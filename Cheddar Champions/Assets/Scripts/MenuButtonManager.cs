
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button m_YourFirstButton, m_YourSecondButton, m_YourThirdButton;

    void Start()
    {
        //Calls the specified method when you click the respective Button
        m_YourFirstButton.onClick.AddListener(PlayGame);
        m_YourSecondButton.onClick.AddListener(Tutorial);
        m_YourThirdButton.onClick.AddListener(Quit);
    }

    void PlayGame()
    {
        //Output this to console when play button is clicked
        Debug.Log("You have clicked the play button!");
        //SceneManager.LoadScene(1);
    }

    void Tutorial()
    {
        //Output this to console when tutorial is clicked
        Debug.Log("You have clicked the tutorial button!");
    }

    void Quit()
    {
        //Output this to console when quit is clicked
        Debug.Log("You have clicked the quit button!");
        Application.Quit();
    }


}
