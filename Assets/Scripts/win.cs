using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class win : MonoBehaviour {

    bool menu = true;
    bool help = false;
    GUIStyle style;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        style = new GUIStyle();
        style.wordWrap = true;
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        Destroy(g);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.backgroundColor = Color.white;
        int w = Screen.width / 12;
        int h = Screen.height / 12;
        if (menu)
        {
            GUI.Box(new Rect(w * 6 - (w / 2), h * 6 - (h * 5), 200, 50), "You have battled your way \nthrough dreams and reality.");
            //Start game
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h * 4), 200, 50), "Play again?"))
            {
                GameObject player = Resources.Load("Prefabs/Player/Player") as GameObject;
                Instantiate(player);
                SceneManager.LoadScene("dream");
            }
            //skip tut
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h * 3), 200, 50), "Start again but skip\n practice level"))
            {
                GameObject player = Resources.Load("Prefabs/Player/Player") as GameObject;
                Instantiate(player);
                SceneManager.LoadScene("forest");
            }
            //Help
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h * 2), 200, 50), "Controls"))
            {
                menu = false;
                help = true;

            }
            //exit
            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6 - (h * 1), 200, 50), "Exit Game"))
            {
                Application.Quit();
            }

            if (GUI.Button(new Rect(w * 6 - (w / 2), h * 6, 200, 50), "Test Level"))
            {
                GameObject player = Resources.Load("Prefabs/Player/Player") as GameObject;
                Instantiate(player);
                SceneManager.LoadScene("testScene");
            }

        }
        else if (help)
        {

            GUI.Box(new Rect(w * 6 - (w / 2), h * 6 - (h * 4), w, h), "Controls");
            GUI.backgroundColor = Color.blue;
            GUI.Box(new Rect(w * 6 - (w), h * 6 - (h * 3), w * 2, h), "Use WASD or the arrow \nkeys to move around and the mouse to aim.");
            GUI.Box(new Rect(w * 6 - (w), h * 6 - (h * 2), w * 2, h), "Clicking swings your \nsword, \"F\" fires a fire spell.");
            GUI.Box(new Rect(w * 6 - (w), h * 6 - (h * 1), w * 2, h), "Use tab to open inventory, \n\"U\" to open equipment and \n\"Escape\" to open the \nmain menu.");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu = true;
                help = false;
            }
        }

    }
}
