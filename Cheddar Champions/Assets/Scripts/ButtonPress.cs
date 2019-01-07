using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public Button EatButton;
    public MousePlayerController PC_Script;
    public bool InZone;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        EatButton.onClick.AddListener(TaskOnClick);
        
    }

     void Update()
    {
        
    }

    public void TaskOnClick()
    {
       PC_Script.Consume();
  
    }

    }

