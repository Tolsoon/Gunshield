using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    // Start is called before the first frame update

    public float health;
    public float moveSpeed;
    public float energyCapacity;
    public float maxEnergyCapacity;
    public float maxOverchargeCapacity;
    public float overchargeCapacity = 0f;
    public float maxHealth;
    public Transform gunSpawn;

    public string shieldSize;

    public bool overcharged = false;

    public bool decharging = false;

    public GameManager gameManager;
    public loadoutSelection loadout;
    public GameObject brokenRobot;
    bool brokenRoboSpawned = false;
    public void Start()
    {
        maxEnergyCapacity = energyCapacity;
        maxOverchargeCapacity = energyCapacity * 0.2f;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    [System.Obsolete]
    public void Update()
    {
        if(health > 0)
        {
            brokenRoboSpawned = false;
        }

        if(overchargeCapacity >= maxOverchargeCapacity && decharging == false)
        {
            
            decharging = true;
            overcharged = true;
            
        }
        else
        {
            overcharged = false;
            
        }

        if(health <= 0 && brokenRoboSpawned == false)
        {
            brokenRobot.GetComponent<BrokenRobotDissapear>().explosion.GetComponent<ParticleSystem>().startColor = loadout.glowColor ;
           
            
            Vector3 currentPos = transform.position;
            Instantiate(brokenRobot, currentPos, Quaternion.identity);
            GetComponentInParent<MoveTest3>().death();
            brokenRoboSpawned = true;
        }

        if(energyCapacity < 0f)
        {
            energyCapacity = 0f;
        }
        if(overchargeCapacity < 0f)
        {
            overchargeCapacity = 0f;
        }
    }


}
