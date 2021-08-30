using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetUpMenu : MonoBehaviour
{

    public loadoutSelection loadout;

    public GameObject rootMenu;
    
    private void Awake()
    {
        
        rootMenu = GameObject.Find("MainLayout");
        
        


    }

    public void Update()
    {
        transform.SetParent(rootMenu.GetComponent<LoadoutPositions>().cubes[loadout.PlayerIndex].transform);

        transform.position = rootMenu.GetComponent<LoadoutPositions>().cubes[loadout.PlayerIndex].transform.position;
    }


}
