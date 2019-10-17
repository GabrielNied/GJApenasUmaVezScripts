using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController2 : MonoBehaviour
{
    public GameObject gameManager, target, textMesh, explosao, marcaExplosao, MarcaExplosao;

    public int tempoExplosao = 5;

    public bool atacou = false, pertoBaixo = false, pertoCima = false, pertoEsquerda = false, pertoDireita = false, DiagonalEsquerdaBaixo = false, DiagonalDireitaBaixo = false, diagonalEsquerda = false, diagonalDireita = false;

    public RaycastHit _hit;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GameManager");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void ComecoTurno()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist <=10)
        {
            checkColl();
            checkDiagonal();
            Movimenta();
            Ataca();
        }
        else
        {
            FimTurno();
        }
    }

    public void FimTurno()
    {
        gameManager.GetComponent<GameManager>().turnEnemy2 = false;
        gameManager.GetComponent<GameManager>().turnEnemy3 = true;

        if (atacou)
        {
            Destroy(this.gameObject);
        }   
    }

    public void Movimenta()
    {    

        float distX = transform.position.x - target.transform.position.x;
        float distZ = transform.position.z - target.transform.position.z;

        if (distX > 1.5f || distX < -1.5f)
        {
            MovimentaHorizontal();
        }
        else if (distZ > 1.5f || distZ < -1.5f)
        {
            MovimentaVertical();
        }
    }

    public void Ataca()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist <= 8)
        {
            if (MarcaExplosao == null)
            {
                MarcaExplosao = Instantiate(marcaExplosao, transform.position, Quaternion.identity) as GameObject;
                MarcaExplosao.transform.SetParent(this.transform);
            }
            textMesh.GetComponent<TextMesh>().text = "" + tempoExplosao.ToString();
            tempoExplosao -= 1;
        }

        if (tempoExplosao <= -1)
        {            
            Instantiate(explosao, transform.position, Quaternion.identity);
            atacou = true;
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
            pertoCima = true;
        }
        else
        {
            pertoCima = false;
        }
        if (Physics.Raycast(this.transform.position, Vector3.back, out _hit, 1f))
        {
            pertoBaixo = true;
        }
        else
        {
            pertoBaixo = false;
        }

        if (Physics.Raycast(this.transform.position, Vector3.left, out _hit, 1f))
        {
            pertoEsquerda = true;
        }
        else
        {
            pertoEsquerda = false;
        }

        if (Physics.Raycast(this.transform.position, Vector3.right, out _hit, 1f))
        {
            pertoDireita = true;
        }
        else
        {
            pertoDireita = false;
        }
    }

    public void checkDiagonal()
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
        if (other.tag == "DiagonalEsquerdaBaixo")
        {
            DiagonalEsquerdaBaixo = true;
        }
        if (other.tag == "DiagonalDireitaBaixo")
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