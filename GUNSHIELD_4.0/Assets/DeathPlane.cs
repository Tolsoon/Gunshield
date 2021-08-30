using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("robot"))
        {
            collision.transform.gameObject.GetComponentInChildren<Robot>().health = 0;
        }
    }
}
