using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject gameManager, target, textMesh;

    public bool carregaAtaque = false, carregaAtaque1 = false,
        pertoBaixo = false, pertoCima = false, pertoEsquerda = false, pertoDireita = false, DiagonalEsquerdaBaixo = false, DiagonalDireitaBaixo = false, diagonalEsquerda = false, diagonalDireita = false,
        pertoBaixoPlayer = false, pertoCimaPlayer = false, pertoEsquerdaPlayer = false, pertoDireitaPlayer = false;

    public RaycastHit _hit;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GameManager");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        checkColl();
        Debug.Log("perto? " + pertoBaixoPlayer + pertoCimaPlayer + pertoEsquerdaPlayer + pertoDireitaPlayer);
    }

    public void ComecaTurno()
    {        
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist <= 10)
        {
            Movimenta();
            CheckAtaque();
            if (pertoBaixoPlayer || pertoCimaPlayer || pertoEsquerdaPlayer || pertoDireitaPlayer)
            {
                if (carregaAtaque)
                {
                    textMesh.GetComponent<TextMesh>().text = "!!!";
                }
                else if (carregaAtaque1)
                    textMesh.GetComponent<TextMesh>().text = "!";
            }
            else
            {
                textMesh.GetComponent<TextMesh>().text = "";
            }
        }
        else
        {
            FimTurno();
        }

    }

    public void FimTurno()
    {
        gameManager.GetComponent<GameManager>().turnEnemy1 = false;
        gameManager.GetComponent<GameManager>().turnEnemy2 = true;
        if (pertoBaixoPlayer || pertoCimaPlayer || pertoEsquerdaPlayer || pertoDireitaPlayer)
        {
            if (carregaAtaque)
            {
                textMesh.GetComponent<TextMesh>().text = "!!!";
            }
            else if (carregaAtaque1)
                textMesh.GetComponent<TextMesh>().text = "!";
        }
        else
        {
            textMesh.GetComponent<TextMesh>().text = "";
        }
    }

    public void Movimenta()
    {
        if (!carregaAtaque1)
        {
            float distX = transform.position.x - target.transform.position.x;
            float distZ = transform.position.z - target.transform.position.z;

            if (DiagonalEsquerdaBaixo || DiagonalDireitaBaixo || diagonalEsquerda || diagonalDireita)
            {
                AndaDiagonal();
            }
            else if (distX > 1.5f || distX < -1.5f)
            {
                MovimentaHorizontal();
            }
            else if (distZ > 1.5f || distZ < -1.5f)
            {
                MovimentaVertical();
            }       
        }  
    }

    public void CheckAtaque()
    {
        if (carregaAtaque1)
        {
            Mata();
        }
        else if (pertoBaixoPlayer || pertoCimaPlayer || pertoEsquerdaPlayer || pertoDireitaPlayer)
        {
            float distX = transform.position.x - target.transform.position.x;
            float distZ = transform.position.z - target.transform.position.z;

            if ((distX < 1f || distX > -1f) || (distZ < 1f || distZ > -1f))
            {
                Ataca();               
            }
            else
            {
                FimTurno();
            }
        }
        else
        {
            FimTurno();
        }
    }
    
    public void Ataca()
    {
        carregaAtaque1 = true;
        if (pertoBaixoPlayer)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            Debug.Log("B");
        }
        else if (pertoCimaPlayer)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            Debug.Log("C");
        }
        else if (pertoEsquerdaPlayer)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);
            Debug.Log("E");
        }
        else if (pertoDireitaPlayer)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
            Debug.Log("D");
        }
        FimTurno();
    }

    public void Mata()
    {
        textMesh.GetComponent<TextMesh>().text = "!!!";
        carregaAtaque = true;
        if (pertoBaixoPlayer || pertoCimaPlayer || pertoEsquerdaPlayer || pertoDireitaPlayer)
        {
            float distX = transform.position.x - target.transform.position.x;
            float distZ = transform.position.z - target.transform.position.z;
            if ((distX < 1f || distX > -1f) || (distZ < 1f || distZ > -1f))
            {
                Debug.Log("Morreu");
                //gameManager.GetComponent<GameManager>().Morre();
                FimTurno();
                carregaAtaque = false;
                carregaAtaque1 = false;
            }
            else
            {
                FimTurno();
                carregaAtaque = false;
                carregaAtaque1 = false;
            }
        }
        else
        {
            carregaAtaque = false;
            carregaAtaque1 = false;
            FimTurno();
        }
    }

    public void MovimentaHorizontal()
    {
        float distX = transform.position.x - target.transform.position.x;

        if (!pertoEsquerda)
        {
            if (distX > 1f) //Left
            {
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                                 
                agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);                
                FimTurno();
            }
        }
        if (!pertoDireita)
        {
            if (distX < -0.1f) //Right
            {
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
                FimTurno();
            }
        }
    }

    public void MovimentaVertical()
    {
        float distZ = transform.position.z - target.transform.position.z;

        if (!pertoBaixo)
        {
            if (distZ > 1f) //Down
            {
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                FimTurno();
            }
        }

        if (!pertoCima)
        {
            if (distZ < -0.1f) //Up
            {
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                FimTurno();
            }
        }
    }

    public void checkColl()
    {
        Vector3 _position = Vector3.forward;
        Debug.DrawRay(this.transform.position, _position, Color.red);
        if (Physics.Raycast(this.transform.position, Vector3.forward, out _hit, 1f))
        {
            if (_hit.collider.gameObject.tag == "Player")
            {
                pertoCimaPlayer = true;
            }
            else
            {
                pertoCima = true;
            }
        }
        else
        {
            pertoCimaPlayer = false;
            pertoCima = false;
        }
        if (Physics.Raycast(this.transform.position, Vector3.back, out _hit, 1f))
        {
            if (_hit.collider.gameObject.tag == "Player")
            {
                pertoBaixoPlayer = true;
            }
            else
            {
                pertoBaixo = true;
            }
        }
        else
        {
            pertoBaixoPlayer = false;
            pertoBaixo = false;
        }

        if (Physics.Raycast(this.transform.position, Vector3.left, out _hit, 1f))
        {
            if (_hit.collider.gameObject.tag == "Player")
            {
                pertoEsquerdaPlayer = true;
            }
            else
            {
                pertoEsquerda = true;
            }
        }
        else
        {
            pertoEsquerdaPlayer = false;
            pertoEsquerda = false;
        }

        if (Physics.Raycast(this.transform.position, Vector3.right, out _hit, 1f))
        {
            if (_hit.collider.gameObject.tag == "Player")
            {
                pertoDireitaPlayer = true;
            }
            else
            {
                pertoDireita = true;
            }
        }
        else
        {
            pertoDireitaPlayer = false;
            pertoDireita = false;
        }
    }

    public void AndaDiagonal()
    {
        if (diagonalDireita)
        {
            if (transform.position.x < target.transform.position.x && transform.position.z < target.transform.position.z)
            {
                if (!pertoCima)
                {
                    agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    diagonalDireita = false;
                    FimTurno();
                }
                else if (!pertoDireita)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    diagonalDireita = false;
                    FimTurno();
                }
            }
            else if (transform.position.x < target.transform.position.x && transform.position.z > target.transform.position.z)
            {
                if (!pertoBaixo)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    diagonalDireita = false;
                    FimTurno();
                }
                else if (!pertoDireita)
                {
                    agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    diagonalDireita = false;
                    FimTurno();
                }
            }
            else if (transform.position.x > target.transform.position.x && transform.position.z > target.transform.position.z)
            {
                if (!pertoBaixo)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    diagonalDireita = false;
                    FimTurno();
                }
                else if (!pertoEsquerda)
                {
                    agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    diagonalDireita = false;
                    FimTurno();
                }
            }
            else if (transform.position.x > target.transform.position.x && transform.position.z < target.transform.position.z)
            {
                if (!pertoCima)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    diagonalDireita = false;
                    FimTurno();
                }
                else if (!pertoEsquerda)
                {
                    agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    diagonalDireita = false;
                    FimTurno();
                }
            }
        }

        else if (diagonalEsquerda)
        {
            if (transform.position.x < target.transform.position.x && transform.position.z < target.transform.position.z)
            {
                if (!pertoCima)
                {
                    agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    diagonalEsquerda = false;
                    FimTurno();
                }
                else if (!pertoDireita)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    diagonalEsquerda = false;
                    FimTurno();
                }
            }
            else if (transform.position.x < target.transform.position.x && transform.position.z > target.transform.position.z)
            {
                if (!pertoBaixo)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    diagonalEsquerda = false;
                    FimTurno();
                }
                else if (!pertoDireita)
                {
                    agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    diagonalEsquerda = false;
                    FimTurno();
                }
            }
            else if (transform.position.x > target.transform.position.x && transform.position.z > target.transform.position.z)
            {
                if (!pertoBaixo)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    diagonalEsquerda = false;
                    FimTurno();
                }
                else if (!pertoEsquerda)
                {
                    agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    diagonalEsquerda = false;
                    FimTurno();
                }
            }
            else if (transform.position.x > target.transform.position.x && transform.position.z < target.transform.position.z)
            {
                if (!pertoCima)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    diagonalEsquerda = false;
                    FimTurno();
                }
                else if (!pertoEsquerda)
                {
                    agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    diagonalEsquerda = false;
                    FimTurno();
                }
            }
        }
        else if (DiagonalEsquerdaBaixo)
        {
            if (transform.position.x < target.transform.position.x && transform.position.z < target.transform.position.z)
            {
                if (!pertoCima)
                {
                    agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    DiagonalEsquerdaBaixo = false;
                    FimTurno();
                }
                else if (!pertoDireita)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    DiagonalEsquerdaBaixo = false;
                    FimTurno();
                }
            }
            else if (transform.position.x < target.transform.position.x && transform.position.z > target.transform.position.z)
            {
                if (!pertoBaixo)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    DiagonalEsquerdaBaixo = false;
                    FimTurno();
                }
                else if (!pertoDireita)
                {
                    agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    DiagonalEsquerdaBaixo = false;
                    FimTurno();
                }
            }
            else if (transform.position.x > target.transform.position.x && transform.position.z > target.transform.position.z)
            {
                if (!pertoBaixo)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    DiagonalEsquerdaBaixo = false;
                    FimTurno();
                }
                else if (!pertoEsquerda)
                {
                    agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    DiagonalEsquerdaBaixo = false;
                    FimTurno();
                }
            }
            else if (transform.position.x > target.transform.position.x && transform.position.z < target.transform.position.z)
            {
                if (!pertoCima)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    DiagonalEsquerdaBaixo = false;
                    FimTurno();
                }
                else if (!pertoEsquerda)
                {
                    agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    DiagonalEsquerdaBaixo = false;
                    FimTurno();
                }
            }
        }
        else if (DiagonalDireitaBaixo)
        {
            if (transform.position.x < target.transform.position.x && transform.position.z < target.transform.position.z)
            {
                if (!pertoCima)
                {
                    agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    DiagonalDireitaBaixo = false;
                    FimTurno();
                }
                else if (!pertoDireita)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    DiagonalDireitaBaixo = false;
                    FimTurno();
                }
            }
            else if (transform.position.x < target.transform.position.x && transform.position.z > target.transform.position.z)
            {
                if (!pertoBaixo)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    DiagonalDireitaBaixo = false;
                    FimTurno();
                }
                else if (!pertoDireita)
                {
                    agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    DiagonalDireitaBaixo = false;
                    FimTurno();
                }
            }
            else if (transform.position.x > target.transform.position.x && transform.position.z > target.transform.position.z)
            {
                if (!pertoBaixo)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                    DiagonalDireitaBaixo = false;
                    FimTurno();
                }
                else if (!pertoEsquerda)
                {
                    agent.destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

                    DiagonalDireitaBaixo = false;
                    FimTurno();
                }
            }
            else if (transform.position.x > target.transform.position.x && transform.position.z < target.transform.position.z)
            {
                if (!pertoCima)
                {
                    agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

                    DiagonalDireitaBaixo = false;
                    FimTurno();
                }
                else if (!pertoEsquerda)
                {
                    agent.destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

                    DiagonalDireitaBaixo = false;
                    FimTurno();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DiagonalEsquerdaBaixo")
        {
            DiagonalEsquerdaBaixo = true;
        }
        if(other.tag == "DiagonalDireitaBaixo")
        {
            DiagonalDireitaBaixo = true;
        }

        if (other.tag == "DiagonalEsquerda")
        {
            diagonalEsquerda = true;
        }

        if (other.tag == "DiagonalDireita")
        {
            diagonalDireita = true;
        }
    }
}