using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenRobotDissapear : MonoBehaviour
{

    public ParticleSystem explosion;
    
    void Start()
    {
        StartCoroutine(Dissapear());
        
    }

    IEnumerator Dissapear()
    {
       
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
