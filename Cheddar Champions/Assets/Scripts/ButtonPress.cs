using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonPress : NetworkBehaviour
{
    public Button EatButton;
    public Button SqueakButton;
    public MousePlayerController PC_Script;

    public Camera main;
    public Camera win;
    //public EatCheese CheeseScript;

    public GameObject myPlayer;
    private MousePlayerController myController;

    void Start()
    {
        main.enabled = true;
        win.enabled = false;

        EatButton.onClick.AddListener(EatClick);
        //SqueakButton.onClick.AddListener(SqueakClick);
    }

    void Update()
    {
        if (myPlayer == null)
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player");

            if (myPlayer == null)
            {
                print("Player not found");
            }
        }
    }

    public void EatClick()
    {
        print("Does my Player have authority: " + myPlayer.GetComponent<NetworkIdentity>().hasAuthority);

        myController = myPlayer.GetComponent<MousePlayerController>();

        myController.FireCheese();
        print("eat button pressed");

        //main.enabled = false;
        //win.enabled = true;
    }

    public void SqueakClick()
    {
        print("Squeaked");
    }
}

