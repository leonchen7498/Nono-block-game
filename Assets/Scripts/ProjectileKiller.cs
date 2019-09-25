using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKiller : MonoBehaviour
{
    new public ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("what the frick");
        if (collision.gameObject.tag == "projectile")
        {
            Destroy(collision.gameObject);
            Vector3 particlePosition = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
            ParticleSystem placeParticle = Instantiate(particleSystem, particlePosition, Quaternion.identity);
            placeParticle.Play();
        }
    }
}
