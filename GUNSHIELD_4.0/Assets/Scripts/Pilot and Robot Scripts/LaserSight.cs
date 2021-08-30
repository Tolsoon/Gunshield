using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    public LineRenderer lr;

    private void Start()
    {
        
        lr = GetComponent<LineRenderer>();
       
    }

    private void Update()
    {
       

        
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f))
        {
            
            if (hit.collider.gameObject.CompareTag("wall") || hit.collider.gameObject.CompareTag("robot"))
            {

                lr.SetPosition(1, transform.InverseTransformPoint(hit.point));
            }
        }
        else
        {
            
            lr.SetPosition(1, new Vector3 (0,0,5000));
        }
    }
}
