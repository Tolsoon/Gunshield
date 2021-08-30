using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMirage : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Dissapear());
    }

    IEnumerator Dissapear()
    {

        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
