using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRifle : MonoBehaviour {

    public GameObject playerSemRifleRef, posicaoTiro, tiroFraco, tiroFortinho;
    public Animator animTiroRef;


	void Start () {
        animTiroRef = GetComponent<Animator>();
        playerSemRifleRef = GameObject.FindGameObjectWithTag("Player") as GameObject;
        StartCoroutine(Tiro1());


	}
	
	
	void Update () {
		
	}

    IEnumerator Tiro1()
    {
        yield return new WaitForSeconds(1.07f);
        Instantiate(tiroFraco, posicaoTiro.transform.position, Quaternion.identity);

        playerSemRifleRef.gameObject.SetActive(true);
        Destroy(this.gameObject);

        
    }
}
