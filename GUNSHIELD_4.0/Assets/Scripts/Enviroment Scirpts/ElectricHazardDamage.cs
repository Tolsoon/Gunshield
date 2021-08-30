using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricHazardDamage : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("robot"))
        {
            
            other.GetComponentInChildren<Robot>().health -= 10 * Time.deltaTime;
        }
    }
}
