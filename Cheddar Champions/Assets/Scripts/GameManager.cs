using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class GameManager : NetworkBehaviour
{

    public GameObject[] Podiums;
    public GameObject[] Players;

    int playerScore;

    public Camera main;
    public Camera win;

    [SyncVar]
    public float timeLeft = 20f; // reference for the amount of time that's passed

    public Text Timertext; // reference the UI text

    void Start()
    {
        timeLeft= 20;
    }

    void Update()
    {
       Players = GameObject.FindGameObjectsWithTag("Player");

        if (Timertext != null)
        {

            timeLeft -= Time.deltaTime; //time left minus delta time
            Timertext.text = "Time: " + Mathf.Round(timeLeft); // display the time left in referenced UI text
        }

        if (timeLeft <= 0)
        {
            EndGame();
        }

    }


    public void EndGame()
    {
        Timertext.enabled = false;
        StartCoroutine(Allocate());
    }

   IEnumerator Allocate()
   {
        print("Starting coroutine");
        foreach (GameObject go in Players)
        {
            playerScore = go.GetComponent<MousePlayerController>().score;
            print("Player Score is: " + playerScore);
        }



        yield return null;
    }


    #region  DON'T DESTROY ON LOAD

    public static GameManager sGM; //Creates a reference for the GM singleton

    private void Awake() //This happens before Start
    {


        if (sGM == null) //Does the Gm already exist? Y/N
        {
            sGM = this; //If NO and this is the first time, create an instance that is stored
            DontDestroyOnLoad(gameObject); //This means that the Gm will persist throughout reloads
        }

        else if (sGM != this) // If another GM is created, we need to destory it to avoid having more than one
        {
            Destroy(gameObject); //remove any subsequent Gm's
        }

    }
    #endregion

}

