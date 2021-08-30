using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotStats : MonoBehaviour
{
    public Slider health;
    public Slider moveSpeed;
    public Slider energyCapacity;

    public GameObject robot;

    public loadoutSelection loadout;

    
    public void Update()
    {
        robot = loadout.robotHeads[loadout.selectedPilot];


        health.value = robot.GetComponentInChildren<Robot>().health;
        moveSpeed.value = robot.GetComponentInChildren<Robot>().moveSpeed;
        energyCapacity.value = robot.GetComponentInChildren<Robot>().energyCapacity;
        
    }
    
}
