using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DeathExplosion : MonoBehaviour
{
    public float radius;
    public float power;
    public Rigidbody rb;
    void Start()
    {
        transform.position = transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && rb.gameObject.CompareTag("explode"))
                
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
