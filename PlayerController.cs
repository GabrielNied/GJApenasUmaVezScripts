using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool pertoBaixo = false, pertoCima = false, pertoEsquerda = false, pertoDireita = false;
    public RaycastHit _hit, _hit2;

    public Animator animPlay;

    public GameObject gameManager, pauseMenu;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GameManager");
        pauseMenu = GameObject.Find("Canvas/PauseMenuManager");
        animPlay = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameManager.GetComponent<GameManager>().turnPlayer)
        {
            ComecoTurno();
        }
    }

    public void ComecoTurno()
    {
        VerificarPodeAndar();        
        Movimenta();        
        //Ataca();
    }

    public void FimTurno()
    {
        gameManager.GetComponent<GameManager>().turnEnemy1 = true;
        gameManager.GetComponent<GameManager>().turnPlayer = false;
        gameManager.GetComponent<GameManager>().cooldown = 0f;
    }

    public void Movimenta()
    {
        if (gameManager.GetComponent<GameManager>().turnPlayer && !pauseMenu.GetComponent<PauseMenu>().despausou)        {

            if (Input.GetKeyDown(KeyCode.Space))
            {               
                FimTurno();
            }

            if (Input.GetKey(KeyCode.A) && !pertoEsquerda) //Left
            {
                animPlay.SetTrigger("Andou");
                agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);
                FimTurno();
            }
            else if (Input.GetKey(KeyCode.D) && !pertoDireita) //Right
            {
                animPlay.SetTrigger("Andou");
                agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
                FimTurno();
            }
            else if (Input.GetKey(KeyCode.W) && !pertoCima) //Up
            {
                animPlay.SetTrigger("Andou");
                agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                FimTurno();
            }
            else if (Input.GetKey(KeyCode.S) && !pertoBaixo) //Down
            {
                animPlay.SetTrigger("Andou");
                agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                FimTurno();
            }
        }
    }
    
    public void VerificarPodeAndar()
    {
        Vector3 _position = Vector3.forward;
        Debug.DrawRay(this.transform.position, _position, Color.red);
        if (Physics.Raycast(this.transform.position, Vector3.forward, out _hit, 2f))
        {
            pertoCima = true;
        }
        else
        {
            pertoCima = false;
        }

        if (Physics.Raycast(this.transform.position, Vector3.back, out _hit, 2f))
        {
            pertoBaixo = true;
        }
        else
        {
            pertoBaixo = false;
        }

        if (Physics.Raycast(this.transform.position, Vector3.left, out _hit, 2f))
        {
            pertoEsquerda = true;
        }
        else
        {
            pertoEsquerda = false;
        }

        if (Physics.Raycast(this.transform.position, Vector3.right, out _hit, 2f))
        {
            pertoDireita = true;
        }
        else
        {
            pertoDireita = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
     if(col.transform.tag == "Tumba1")
        {
            SceneManager.LoadScene("Tumba");
        }
     
        if(col.transform.tag == "Tumba2")
        {
            SceneManager.LoadScene("Inferno");
        }
        if (col.transform.tag == "Explosao")
        {
            gameManager.GetComponent<GameManager>().Morre();
        }
    }

    private void OnParticleCollision(GameObject other)
    {  
        if(other.transform.tag == "Cuspe")
        {
            gameManager.GetComponent<GameManager>().Morre();
        }
    }

}