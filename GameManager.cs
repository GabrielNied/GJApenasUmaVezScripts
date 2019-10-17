using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool turnPlayer = true, turnEnemy1 = false, turnEnemy2 = false, turnEnemy3 = false;
    public GameObject[] enemys, enemys2, enemys3;

    public GameObject player, pauseMenu;

    public float cooldown = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pauseMenu = GameObject.Find("Canvas/PauseMenuManager");
    }

    void FixedUpdate()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= 0.5f && !pauseMenu.GetComponent<PauseMenu>().despausou)
        {
            Turnos();
        }
    }

    public void Turnos()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        enemys2 = GameObject.FindGameObjectsWithTag("Enemy2");
        enemys3 = GameObject.FindGameObjectsWithTag("Enemy3");
        if (!turnPlayer)
        {
            if (enemys.Length >= 1)
            {
                if (turnEnemy1)
                {
                    foreach (GameObject enemy in enemys)
                    {
                        enemy.GetComponent<EnemyController>().ComecaTurno();
                    }
                }
            }
            else
            {
                turnEnemy1 = false;
                turnEnemy2 = true;
            }

            if (enemys2.Length >= 1)
            {
                if (turnEnemy2)
                {
                    foreach (GameObject enemy2 in enemys2)
                    {
                        enemy2.GetComponent<EnemyController2>().ComecoTurno();
                    }
                }
            }
            else
            {
                turnEnemy2 = false;
                turnEnemy3 = true;
            }

            if (enemys3.Length >= 1)
            {
                if (turnEnemy3)
                {
                    foreach (GameObject enemy3 in enemys3)
                    {
                        enemy3.GetComponent<EnemyController3>().ComecoTurno();
                    }
                }
            }
            else
            {
                turnEnemy3 = false;
                turnPlayer = true;
            }
        }

        cooldown = 0f;
    }
    
    public void Morre()
    {
        SceneManager.LoadScene("TelaDerrota");
    }
}
