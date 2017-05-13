using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour {

    private string level;

    public void setLevel(string l)
    {
        level = l;
    }

    void worldUse()
    {
        print(level);
        if(level == "testScene")
        {
            SceneManager.LoadScene("dream", LoadSceneMode.Single);
        }
        else if(level == "dream")
        {
            SceneManager.LoadScene("forest", LoadSceneMode.Single);
        }
        else if (level == "forest")
        {
            SceneManager.LoadScene("dungeon", LoadSceneMode.Single);
        }
        else if (level == "dungeon")
        {
            SceneManager.LoadScene("endLevel", LoadSceneMode.Single);
        }
        else if (level == "endLevel")
        {
            SceneManager.LoadScene("endScreen", LoadSceneMode.Single);
        }
    }
}
