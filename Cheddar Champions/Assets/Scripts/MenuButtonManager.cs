
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button m_YourFirstButton, m_YourThirdButton;
    public GameObject NW_Manager;

    void Start()
    {
        //Calls the specified method when you click the respective Button


        m_YourThirdButton.onClick.AddListener(Quit);
    }




    void Quit()
    {
        //Output this to console when quit is clicked
        Debug.Log("You have clicked the quit button!");
        Application.Quit();
    }


}
