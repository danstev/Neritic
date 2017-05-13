using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI ()
    {
        GUI.Box(new Rect(70 + 30, 30, 50, 50), "hello"); //draw buttons here

        //Start game
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Start Game"))
        {
            GameObject player = Resources.Load("Prefabs/Player/Player") as GameObject;
            Instantiate(player);
            SceneManager.LoadScene("dream");
        }
        //Help
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 25, 100, 50), "Tutorial"))
        {
            //nothing atm lol
        }
        //exit
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 75, 100, 50), "Exit Game"))
        {
            Application.Quit();
        }
        
    }
}
