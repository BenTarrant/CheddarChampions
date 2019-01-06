using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public Button EatButton;
    public static bool buttonPressed;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        EatButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        //Output this to console when Button is clicked
        //Debug.Log("You have clicked the button!");

        print("Pressed");
    }

    }

