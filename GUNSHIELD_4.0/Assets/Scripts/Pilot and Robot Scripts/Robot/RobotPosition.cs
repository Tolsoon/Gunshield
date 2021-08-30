using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPosition : MonoBehaviour
{
    public loadoutSelection loadout;
    public Camera cam;

    public GameObject robotCube;
    void Start()
    {
        robotCube = GameObject.FindGameObjectWithTag("cube");
        loadout = GetComponentInParent<loadoutSelection>();
        cam = FindObjectOfType<Camera>();
        if(loadout.PlayerIndex == 0)
        {
            transform.SetParent(robotCube.GetComponent<RobotCubes>().cubes[loadout.PlayerIndex].transform);
            transform.position = robotCube.GetComponent<RobotCubes>().cubes[loadout.PlayerIndex].transform.position;
        }
        if (loadout.PlayerIndex ==1 )
        {
            transform.SetParent(robotCube.GetComponent<RobotCubes>().cubes[loadout.PlayerIndex].transform);
            transform.position = robotCube.GetComponent<RobotCubes>().cubes[loadout.PlayerIndex].transform.position;
        }
        if (loadout.PlayerIndex == 2)
        {
            transform.SetParent(robotCube.GetComponent<RobotCubes>().cubes[loadout.PlayerIndex].transform);
            transform.position = robotCube.GetComponent<RobotCubes>().cubes[loadout.PlayerIndex].transform.position;
        }
        if (loadout.PlayerIndex == 3)
        {
            transform.SetParent(robotCube.GetComponent<RobotCubes>().cubes[loadout.PlayerIndex].transform);
            transform.position = robotCube.GetComponent<RobotCubes>().cubes[loadout.PlayerIndex].transform.position;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
