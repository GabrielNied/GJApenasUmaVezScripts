using UnityEngine;
using System.Collections;

public class PartycleSystemAutoDestroy : MonoBehaviour {

    private ParticleSystem ps;


    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(this.gameObject);
    }
}