using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroEscopeta : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
