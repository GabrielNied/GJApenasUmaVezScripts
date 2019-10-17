using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController3 : MonoBehaviour
{
    public GameObject gameManager, target, cuspePrefab, Cuspe, posicaoCuspe;

    public GameObject[] Tiros;

    public float carregaAtaque = 1;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void ComecoTurno()
    {
 
        Debug.Log("Ataque: " + carregaAtaque);
        if (carregaAtaque >= 1)
        {
            Ataca();
        }
        else
        {
            FimTurno();
        }
    }

    public void FimTurno()
    {
        carregaAtaque += 1;
        gameManager.GetComponent<GameManager>().turnEnemy3 = false;
        gameManager.GetComponent<GameManager>().turnPlayer = true;
    }

    public void Ataca()
    {
        float distX = transform.position.x - target.transform.position.x;
        float distZ = transform.position.z - target.transform.position.z;
        if (distX > 1.5f || distX < -1.5f)
        {
            AtacaHorizontal();
            carregaAtaque = 0;
        }
        else if (distZ > 1.5f || distZ < -1.5f)
        {
            AtacaVertical();
            carregaAtaque = 0;
        }
    }

    public void AtacaHorizontal()
    {
        float distX = transform.position.x - target.transform.position.x;

        if (distX > 0.1f) //Left
        {
            Debug.Log("L");
            transform.rotation = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);
            Cuspe = Instantiate(cuspePrefab, posicaoCuspe.transform.position, Quaternion.identity) as GameObject;
            Cuspe.transform.rotation = transform.rotation;
            FimTurno();
        }

        if (distX < -0.1f) //Right
        {
            Debug.Log("R");
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
            Cuspe = Instantiate(cuspePrefab, posicaoCuspe.transform.position, Quaternion.identity) as GameObject;
            Cuspe.transform.rotation = transform.rotation;
            FimTurno();
        }
    }

    public void AtacaVertical()
    {
        float distZ = transform.position.z - target.transform.position.z;

        if (distZ > 1f) //Down
        {
            Debug.Log("D");
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            Cuspe = Instantiate(cuspePrefab, posicaoCuspe.transform.position, Quaternion.identity) as GameObject;
            Cuspe.transform.rotation = transform.rotation;
            FimTurno();
        }

        if (distZ < -0.1f) //Up
        {
            Debug.Log("U");
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            Cuspe = Instantiate(cuspePrefab, posicaoCuspe.transform.position, Quaternion.identity) as GameObject;
            Cuspe.transform.rotation = transform.rotation;
            FimTurno();
        }
    }
}
       
