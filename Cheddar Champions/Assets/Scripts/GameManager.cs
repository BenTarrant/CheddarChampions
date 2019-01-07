using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    //public int Score; //Creates a reference for the score
    public GameObject Crumbs; // reference for the cheese eating particle effect Game Object

    [SyncVar]
    public float timeLeft = 60f; // reference for the amount of time that's passed

    public Text Timertext; // reference the UI text
    public Text BestScore; // reference for highscore in UI

    public Text ScoreText; // reference the UI text
    public int score; // interger reference for score
    public int highScore = 0; // interger reference for highscore
    string highScoreKey = "Best Score: "; // reference for string kept for player prefs

    void Start()
    {
        score = 0;
        timeLeft= 60;
        highScore = PlayerPrefs.GetInt(highScoreKey);  //Get the highScore from player prefs if it is there, 0 otherwise.
        BestScore.text = "Best Score: " + (highScore); // set the best time text to read as the highest score
    }

    void Update()
    {
        //score = Mathf.RoundToInt(timePassed); //score int = time passed float but rounded to the nearest int
        //Timertext.text = score.ToString(); // set the timer passed text value to the score int string
        ScoreText.text = "Score: " + score.ToString();

        timeLeft -= Time.deltaTime; //time left minus delta time
        Timertext.text = "Time Remaining: " + Mathf.Round(timeLeft); // display the time left in referenced UI text

    }


    public void UpdateHighScore() // update high score function (called by the player when they enter the teleport)
    {
        if (score > highScore) // if the score int is higher than the currently stored high score int
        {
            PlayerPrefs.SetInt(highScoreKey, score); // set the high score key as the current score
            PlayerPrefs.Save(); // ensure the player prefs are saved to be retrieved on reload
        }

        else
        {
            
        }
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

