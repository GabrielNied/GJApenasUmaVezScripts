using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
     
    public UIManager uiManager;

    [SerializeField]
    private GameObject tiroRevolver, tiroEscopeta, explosao, posicaoTiro;

    public Animator animRef;
    public GameObject gameManager, pauseMenu, arma, pa;

    public bool tiro1 = true, tiro2 = true, tiro3 = true, tiro4 = true;

    public GameObject Particula;

   // public string[] names = { "Imagem0", "Imagem1", "Imagem2", "Imagem3" };



    void Start()
    {
        

        gameManager = GameObject.Find("GameManager");
        pauseMenu = GameObject.Find("Canvas/PauseMenuManager");
        animRef = GetComponent<Animator>();
        
    }	
	
	void Update () {
        if (gameManager.GetComponent<GameManager>().turnPlayer && !pauseMenu.GetComponent<PauseMenu>().despausou)
        {

            
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    if (uiManager.attacks[0].name == "Imagem0")
            //    {
                    

            //    }
            //    else
            //    {
            //        animRef.SetBool("Atirou", true);
            //    }
                         
                
            //}

            if (Input.GetKeyDown(KeyCode.Alpha1) && tiro1)
            {
                pa.SetActive(true);
                arma.SetActive(false);
                animRef.SetBool("pazada", true);
                tiro1 = false;
                uiManager.attacks[5].color = Color.black;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && tiro2)
            {
                uiManager.attacks[0].name = "Imagem1";
                animRef.SetBool("Atirou", true);
                tiro2 = false;
                uiManager.attacks[2].color = Color.black;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && tiro3)
            {
                uiManager.attacks[0].name = "Imagem2";
                animRef.SetBool("Atirou", true);
                tiro3 = false;
                uiManager.attacks[3].color = Color.black;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && tiro4)
            {
                uiManager.attacks[0].name = "Imagem3";
                animRef.SetBool("Atirou", true);
                tiro4 = false;
                uiManager.attacks[1].color = Color.black;

            }
        }
    }

    public void AtirarBullets()
    {  
        if (uiManager.attacks[0].name == "Imagem3")
        {
            if(pa == isActiveAndEnabled)
            {
                pa.SetActive(false);
                
            }
            arma.SetActive(true);
            GameObject Tiro1 = Instantiate(tiroRevolver) as GameObject;
            
            Tiro1.transform.position = posicaoTiro.transform.position;
            Tiro1.transform.rotation = transform.rotation;
            //Destroy(Fire, 3); //destroy particle system

            //Instantiate(tiroRevolver, transform.position, Quaternion.identity);
            gameManager.GetComponent<GameManager>().turnEnemy1 = true;
            gameManager.GetComponent<GameManager>().turnPlayer = false;
        }
        else if(uiManager.attacks[0].name == "Imagem1")
        {
            if(pa.gameObject == isActiveAndEnabled)
            {
                pa.SetActive(false);

            }
            arma.SetActive(true);
            GameObject Tiro2 = Instantiate(tiroEscopeta) as GameObject;
            Tiro2.transform.position = posicaoTiro.transform.position;
            Tiro2.transform.rotation = transform.rotation;

             //Instantiate(tiroEscopeta, transform.position, Quaternion.identity);
            gameManager.GetComponent<GameManager>().turnEnemy1 = true;
            gameManager.GetComponent<GameManager>().turnPlayer = false;

        }else if (uiManager.attacks[0].name == "Imagem2")
        {
            if(pa == isActiveAndEnabled)
            {
                pa.SetActive(false);

            }
            arma.SetActive(true);
            GameObject Tiro3 = Instantiate(explosao) as GameObject;
            Tiro3.transform.position = posicaoTiro.transform.position;
            Tiro3.transform.rotation = transform.rotation;

            //Instantiate(explosao, transform.position, Quaternion.identity);
            gameManager.GetComponent<GameManager>().turnEnemy1 = true;
            gameManager.GetComponent<GameManager>().turnPlayer = false;
        }
    }

    public void FalsoTiro()
    {
        animRef.SetBool("Atirou", false);
    }

    public void PaFalse()
    {
        animRef.SetBool("pazada", false);
    }

    public void AtivarParticula()
    {
        Particula.SetActive(true);
    }
}
