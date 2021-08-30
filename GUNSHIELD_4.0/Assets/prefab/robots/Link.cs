using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public GameObject pelvisObject;

    void Start()
    {

    }

    void Update()
    {
        

        Debug.Log("UpperBody Start: " + transform.position);
        
        //get the pelvis's tranform
        Transform pelvis = pelvisObject.transform;
        
        //set the UpperBody's position to the pelvis's
        transform.position = pelvis.position;

        Debug.Log("Pelvis: " + pelvis.position);
        Debug.Log("UpperBody Finish: " + transform.position);

        //Debug.Log("UpperBody Start: " + transform.eulerAngles);

        //store the pelvis's rotation
        Vector3 pelvisRotation = new Vector3(pelvis.eulerAngles.x, pelvis.eulerAngles.y, pelvis.eulerAngles.z);

        //set the UpperBody's rotation to the pelvis's
        transform.eulerAngles = pelvisRotation;

        //Debug.Log("Pelvis: " + pelvisRotation);
        //Debug.Log("UpperBody Finish: " + transform.eulerAngles);
    }
}
