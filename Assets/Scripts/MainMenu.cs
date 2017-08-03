using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{

    bool menu = true;
    bool help = false;
    GUIStyle style;

    public NetworkManager m;

	void Start () {
        Cursor.lockState = CursorLockMode.None;
        style = new GUIStyle();
        style.wordWrap = true;
    }

    void instPlayer()
    {
        GameObject player = Resources.Load("Prefabs/Player/Player") as GameObject;
        Instantiate(player);
    }

    void OnGUI ()
    {
        int w = Screen.width / 12;
        int h = Screen.height / 12;
        if (menu)
        {
            GUI.Box(new Rect(w * 6 - (w / 2), h * 6 - (h * 5), 200, 50), "NERITIC");
            //Start game
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h * 4), 200, 50), "Start Game"))
            {
                //instPlayer();
                m.StartHost();
                SceneManager.LoadScene("dream");
            }
            //skip tut
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h * 3), 200, 50), "Start Game but skip\n practice level"))
            {
                //instPlayer();
                m.StartHost();
                SceneManager.LoadScene("home");
            }
            //Help
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h * 2), 200, 50), "Controls"))
            {
                menu = false;
                help = true;
                
            }
            //exit
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h), 200, 50), "Exit Game"))
            {
                Application.Quit();
            }

            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6, 200, 50), "Test Level"))
            {
                m.StartHost();
                SceneManager.LoadScene("testScene");
            }
        }
        else if(help)
        {
            
            GUI.Box(new Rect(w * 6 - (w /2), h * 6 - (h * 4), w, h), "Controls");
            GUI.backgroundColor = Color.blue;
            GUI.Box(new Rect(w * 6 - (w ), h * 6 - (h * 3), w * 2, h), "Use WASD or the arrow \nkeys to move around and the mouse to aim.");
            GUI.Box(new Rect(w * 6 - (w ), h * 6 - (h * 2), w * 2, h), "Clicking swings your \nsword, \"F\" fires a fire spell.");
            GUI.Box(new Rect(w * 6 - (w ), h * 6 - (h * 1), w * 2, h), "Use tab to open inventory, \n\"U\" to open equipment and \n\"Escape\" to open the \nmain menu.");

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu = true;
                help = false;
            }
        }
        
    }
}
