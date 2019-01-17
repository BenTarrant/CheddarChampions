using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



public class MovePlayerCharacter : FakePhysics
{


    private Text mDebugText;

    #region Utility
    Text FindUITextByName(string vName)
    {
        GameObject tTextGO = GameObject.Find(vName);
        if (tTextGO != null)
        {
            return tTextGO.GetComponent<Text>();
        }
        Debug.Log("FindUITextByName() Unable to find:" + vName);
        return null;
    }
    #endregion

    public override void OnStartClient()
    {
        base.OnStartClient();

        OnUpdatePlayerName(mPlayerName);    //Hooks not called on init
        OnHealthChange(mHealth);

        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

	public override void OnStartServer()
	{
		base.OnStartServer();
        StartCoroutine(UpdatePlayer());
        mHealth = 100;
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
	}


	//This only runs when its the local player
	public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        mDebugText = FindUITextByName("DebugText");

        mController = GetComponent<CharacterController>();
        gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;        //Turn local player blue
        Camera.main.transform.SetParent(transform, false); //Parent Camera to Local Player
        CmdSetPlayerName(System.Environment.UserName + " NetID:" + GetComponent<NetworkIdentity>().netId.ToString());
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
        OnScoreChange(mScore);  //Only neeeded for local player

    }


    #region PlayerName
    private Text mUIPlayerName;

    [SerializeField]
    private Text PlayerNameText;      //Link in IDE

    [SyncVar(hook = "OnUpdatePlayerName")]
    protected string mPlayerName = "No set";
    void OnUpdatePlayerName(string vNewName)
    {
        Debug.Assert(PlayerNameText != null, "NameText Not linked");
        mPlayerName = vNewName;
        PlayerNameText.text = mPlayerName;

        if(isLocalPlayer) { //If we are local player also change in UI
            if (mUIPlayerName == null)
            {       //Link UI first time its used
                mUIPlayerName = FindUITextByName("PlayerName");
            }
            Debug.Assert(mUIPlayerName != null, "UI PlayerName missing");
            mUIPlayerName.text = mPlayerName;
        }
    }

    [Command]
    void CmdSetPlayerName(string vNewName)
    {
        mPlayerName = vNewName;
        Debug.LogFormat("SetNewName {0}", vNewName);
    }
    #endregion

    #region FireDamage

    [SerializeField]
    private Transform BulletStart;

    [SerializeField]
    private GameObject BulletPrefab;

    [Command]
    void CmdFire()
    {
        GameObject tGO = Instantiate(BulletPrefab, BulletStart.position, Quaternion.identity);
        tGO.GetComponent<Rigidbody>().velocity = transform.forward * 5.0f;
        tGO.GetComponent<CheeseProjectile>().Shooter = netId;     //Who did the shooting
        NetworkServer.Spawn(tGO);       //This should sync the Shooter ID
        Destroy(tGO, 2.0f);
    }

    [SyncVar(hook = "OnHealthChange")]
    int mHealth;

    [SyncVar(hook = "OnScoreChange")]
    int mScore;

    [SerializeField]
    GameObject FloatingTextPrefab;

    protected override void GotHit(GameObject vGO)
    {
        int tHealthDamage = -7;
        base.GotHit(vGO);
        CheeseProjectile tBullet = vGO.GetComponent<CheeseProjectile>();
        if(tBullet!=null) {
            GameObject tShooter = NetworkServer.FindLocalObject(tBullet.Shooter);
            if(tShooter!=null) {
                MovePlayerCharacter tShooterPlayer = tShooter.GetComponent<MovePlayerCharacter>();
                tShooterPlayer.mScore += 10;
                GameObject tFloatGO = Instantiate(FloatingTextPrefab);
                tFloatGO.transform.position = transform.position;
                tFloatGO.GetComponent<FloatingText>().mString=tShooterPlayer.mPlayerName + " " + tHealthDamage;
                Quaternion tAngle = Quaternion.Euler(0, 0, Random.Range(-45.0f, 45.0f));
                tFloatGO.GetComponent<FloatingText>().mForce = tAngle * Vector3.up * 10.0f;
                Destroy(tFloatGO, 3.0f);
                NetworkServer.Spawn(tFloatGO);
            }
            mHealth = Mathf.Clamp(mHealth + tHealthDamage,0,100);
        }
        Destroy(vGO);
    }

    public Text mUIScoreText;   //LazyLinked at run time
    void    OnScoreChange(int vNewScore) {
        mScore = vNewScore;
        if(isLocalPlayer) {
            if (mUIScoreText == null) {     //Find UI score text, first time its used
                mUIScoreText = FindUITextByName("PlayerScore");
            }
            Debug.Assert(mUIScoreText != null, "UI PlayerScore"); 
            mUIScoreText.text = mScore.ToString();
        }
    }

    public  RectTransform HealthBar;   //Link in IDE
    void    OnHealthChange(int vNewHealth) {
        mHealth = vNewHealth;
        Debug.Assert(HealthBar != null, "HealthBar not set");
        HealthBar.localScale = new Vector2((float)mHealth / 100.0f,HealthBar.localScale.y);
    }

    #endregion

    #region PlayerControl
    float mFireThrottle = 0.0f;
    //Update Character movement based on control input
    protected override void UpdatePhysicsInput()
    {
        float tSpeed = Input.GetAxis("Vertical") * MoveSpeed;
        transform.rotation *= Quaternion.Euler(0, GetRotationControl() * RotateSpeed, 0);
        mVelocity += transform.rotation * Vector3.forward * MoveSpeed * tSpeed;
        mJumpInput = mController.isGrounded && Input.GetButton("Jump");

        if (mFireThrottle < 0.0f)
        {
            mFireThrottle = 0.15f;
            if (Input.GetAxis("Fire1") > 0.1f)
            {
                CmdFire();
            }
        }
        else
        {
            mFireThrottle -= Time.deltaTime;
        }
    }

    //Get rotation either from mouse or XBox controller, not a great hack!
    float GetRotationControl()
    {
        //Really poor way of doing this, but Unity has no easy way to check if an axis has been set up in Input
        //It just crashes the script, this catches the crash and sues the mosue if there is no Xbox controller set up
        try
        {
            float tControl = Input.GetAxis("Horizontal1");      //If XBox controller has input use this
            if (Mathf.Abs(tControl) > Mathf.Epsilon) return tControl;
            else return Input.GetAxis("Horizontal");      //If not use Mouse X
        }
        catch
        {
            return Input.GetAxis("Horizontal");        //If the check for the XBox axis caused an excpetion catch it and use Mouse
        }
    }

    #endregion

    #region PlayerStatus
    private Text mUIPlayerCount;    //Lazy linked at runtime

    IEnumerator    UpdatePlayer() {     //Should only run on server
        Debug.Assert(isServer, "UpdatePlayer() running on client");
        while (true)
        {
            RpcUpdatePlayerCount(NetworkManager.singleton.numPlayers);
            yield return new    WaitForSeconds(1.0f);
        }
    }

    [ClientRpc] //Runs on Client, called by server
    void    RpcUpdatePlayerCount(int vPlayerCount) {
        if(isLocalPlayer) {     //Only update UI if we are local player
            if (mUIPlayerCount == null)
            { //Find it on first use
                mUIPlayerCount = FindUITextByName("PlayerCount");
            }
            Debug.Assert(mUIPlayerCount != null, "UI PlayerCount mising");
            mUIPlayerCount.text = "Players:" + vPlayerCount.ToString();
        }
    }

    #endregion

}
