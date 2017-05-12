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
    }
}
