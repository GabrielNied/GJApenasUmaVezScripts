using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {


    [SerializeField]
    private GameObject[] enemys = new GameObject[5];
    [SerializeField]
    private GameObject[] startPos = new GameObject[3];
    [SerializeField]
    private GameObject[] EnemySpawnPos = new GameObject[3];
    [SerializeField]
    private float[] timeSpawn = new float[3];

    public bool spawn1Start = false;

    public Vector3 playerPosition;
    

	void Start () {

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        
	
	}
	
	
	void Update () {
       //SpawnEnemys();
	}

    public void SpawnEnemys()
    {
        if(spawn1Start)
        {
            StartCoroutine(Spawn1());
            spawn1Start = false;

        }
        else if (playerPosition == startPos[1].transform.position)
        {
            StartCoroutine(Spawn2());
        }
        else if (playerPosition == startPos[2].transform.position)
        {
            StartCoroutine(Spawn3());
        }

    }


    //----------------------------Spawns---------------------------------------------
    public IEnumerator Spawn1()
    {
        while (true)
        {
            int i = Random.Range(0, 1);
            Instantiate(enemys[i], EnemySpawnPos[0].transform.position, Quaternion.identity);

            if (!GameObject.FindGameObjectWithTag("Player"))
            {
                StopCoroutine(Spawn1());
                break;
            }
            yield return new WaitForSeconds(timeSpawn[0]);
        }
    }

    public IEnumerator Spawn2()
    {
        while (true)
        {
            int i = Random.Range(0, 1);
            Instantiate(enemys[i], EnemySpawnPos[1].transform.position, Quaternion.identity);

            if (!GameObject.FindGameObjectWithTag("Player"))
            {
                StopCoroutine(Spawn2());
                break;
            }
            yield return new WaitForSeconds(timeSpawn[1]);
        }
    }

    public IEnumerator Spawn3()
    {
        while (true)
        {
            int i = Random.Range(0, 1);
            Instantiate(enemys[i], EnemySpawnPos[2].transform.position, Quaternion.identity);

            if (!GameObject.FindGameObjectWithTag("Player"))
            {
                StopCoroutine(Spawn3());
                break;
            }
            yield return new WaitForSeconds(timeSpawn[2]);
        }
    }


}
