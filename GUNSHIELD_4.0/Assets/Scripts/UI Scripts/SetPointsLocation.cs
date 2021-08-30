using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPointsLocation : MonoBehaviour
{
    public loadoutSelection loadout;
    

    public GameObject[] cubes;
    void Start()
    {
        
        loadout = GetComponentInParent<loadoutSelection>();
       
        if (loadout.PlayerIndex == 0)
        {
            transform.SetParent(cubes[loadout.PlayerIndex].transform);
            transform.position = cubes[loadout.PlayerIndex].transform.position;
        }
        if (loadout.PlayerIndex == 1)
        {
            transform.SetParent(cubes[loadout.PlayerIndex].transform);
            transform.position = cubes[loadout.PlayerIndex].transform.position;
        }
        if (loadout.PlayerIndex == 2)
        {
            transform.SetParent(cubes[loadout.PlayerIndex].transform);
            transform.position = cubes[loadout.PlayerIndex].transform.position;
        }
        if (loadout.PlayerIndex == 3)
        {
            transform.SetParent(cubes[loadout.PlayerIndex].transform);
            transform.position = cubes[loadout.PlayerIndex].transform.position;
        }


    }
}
