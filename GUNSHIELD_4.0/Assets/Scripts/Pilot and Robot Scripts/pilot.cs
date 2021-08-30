using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilot : MonoBehaviour
{
    
    public int perkSelected;

    public GameObject gun;
    public GameObject shield;
    
   
    public void Update()
    {
        
        gun = GetComponentInChildren<MoveTest3>().Gun;
    }


}
