using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroBasico : MonoBehaviour {

    [SerializeField]
    private float _speed;
	
	void Start () {
        

    }
	
	
	void Update () {
        MoveBullet();
        

    }

    public void MoveBullet()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
