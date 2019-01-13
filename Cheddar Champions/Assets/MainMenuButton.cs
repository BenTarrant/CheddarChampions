using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {

    public Button menuReturn;
    public LobbyCarer tog;

	// Use this for initialization
	void Start () {

        menuReturn.onClick.AddListener(MenuReturn);
        //lobbyManager = gameObject.GetComponent<Canvas>();
    }

    public void MenuReturn()
    {
        print("return to menu");
        //tog.Toggle();
        SceneManager.LoadScene(0);


    }
}
