using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    private GameObject enemy;
    public GameObject enemyToSpawn;
    public float timer;
    public float timerTemp;
	
	// Update is called once per frame
	void Update () {

        if (enemy == null)
        {
            if (timerTemp <= 0)
            {
                enemy = Instantiate(enemyToSpawn, transform.position, transform.localRotation) as GameObject;
                timerTemp = timer;      
            }
            else
            {
                timerTemp -= Time.deltaTime;
            }
        }
    }
}
