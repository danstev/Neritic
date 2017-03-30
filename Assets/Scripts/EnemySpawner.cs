using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    private GameObject enemy;
    public GameObject enemyToSpawn;
    public float timer;
    private float timerTemp;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (enemy == null)
        {
            if (timerTemp <= 0)
            {
                enemy = Instantiate(enemyToSpawn, transform) as GameObject;      
            }
        }
        else
        {
            timerTemp -= Time.deltaTime;
        }
    }
}
