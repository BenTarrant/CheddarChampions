using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public Button EatButton;
    public Button SqueakButton;
    public MousePlayerController PC_Script;
    //public EatCheese CheeseScript;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        EatButton.onClick.AddListener(EatClick);
        //SqueakButton.onClick.AddListener(SqueakClick);

    }

    void Update()
    {

    }

    public void EatClick()
    {
        PC_Script.CmdFireCheese();
        print("eat button pressed");
    }

    public void SqueakClick()
    {
        print("Squeaked");
    }


    }

