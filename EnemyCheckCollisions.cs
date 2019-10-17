using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckCollisions : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}



    private void OnParticleCollision(GameObject other)
    {
        Destroy(this.gameObject);
        //Destroy(other.gameObject);      

    }







}
