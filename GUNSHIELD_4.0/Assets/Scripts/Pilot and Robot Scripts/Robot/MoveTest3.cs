using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class MoveTest3 : MonoBehaviour
{
    //image for energy bar
    public Image energyBar;
    //Animators
    public Animator roboUpper;
    public Animator roboLower;

    //SFX lsit
    public AudioClip[] sfx;
    public AudioSource audioSource;

    public GameManager gameManager;
    //movement variables
    public Vector2 move;    
    public Vector3 dirMove;
    public Vector2 look;

    //loadout
    public loadoutSelection loadout;

    //canas for the player ui
    public Canvas playerUI;

    public bool alive = false;
    bool deathAccounted = false;

    public Transform firespot;
    public Transform robotArm;
    
    public GameObject Gun;
    public GameObject Shield;

    public bool shieldBashOnCooldown;
    public bool hasShieldBash;

    public bool swapOnCooldown;
    public bool usingGun;
    public bool hasSwapped;

    public bool shotOnCooldown;
    public bool hasShot;

    public GameObject shield;
    public float shieldBashCost = 15f;

    public GameObject pauseMenu;
    public bool paused;
    bool pauseAllowed;

    public bool regen;

    public bool invincible;

    public float timeInRound;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(SetPauseAllow()); 

        foreach (Transform child in transform)
            if (child.CompareTag("shield"))
            {
                Shield = child.gameObject;

            }

        shieldBashOnCooldown = false;
        usingGun = true;
        swapOnCooldown = false;
        hasSwapped = false;


        shotOnCooldown = false;
        hasShot = false;

        Shield.SetActive(false);

        

    }
    public void OnMoveUpdated(InputAction.CallbackContext context)
    {
        if (!gameManager.gamePaused)
        {
            if (alive)
            {
                move = context.ReadValue<Vector2>();

            }
        }
        
        
    }

    public void OnRotateUpdated(InputAction.CallbackContext context)
    {
        if (!gameManager.gamePaused)
        {
            if (alive)
            {
                look = context.ReadValue<Vector2>();
            }
        }
        
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!gameManager.gamePaused)
        {
            if (alive)
            {
                if (usingGun)
                {
                    if (shotOnCooldown == false && gameObject.GetComponentInChildren<Robot>().energyCapacity >= Gun.GetComponent<gun>().energyCost)
                    {
                        if (context.phase == InputActionPhase.Performed && context.ReadValueAsButton())
                        {
                            hasShot = true;
                        }
                    }
                }
            }

            if (alive)
            {
                if (!usingGun)
                {
                    if (shieldBashOnCooldown == false)
                    {
                        if (context.phase == InputActionPhase.Performed && context.ReadValueAsButton())
                        {
                            hasShieldBash = true;
                        }
                    }
                }
            }
        }
        


    }

    
    public void OnSwap(InputAction.CallbackContext context)
    {
        if (!gameManager.gamePaused)
        {
            if (alive)
            {
                if (swapOnCooldown == false)
                {
                    if (context.phase == InputActionPhase.Performed && context.ReadValueAsButton())
                    {
                        hasSwapped = true;

                    }
                }
            }
        }
        
       
                
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        
        if(context.phase == InputActionPhase.Performed && context.ReadValueAsButton())
        {
            if (pauseAllowed)
            {
                if (paused == false)
                {
                    pauseMenu.GetComponent<Canvas>().enabled = true;
                    gameManager.gamePaused = true;
                    paused = true;
                }
                else
                {

                    pauseMenu.GetComponent<Canvas>().enabled = false;
                    gameManager.gamePaused = false;
                    paused = false;
                }
            }
            
        }
        
        
    }

    //movement
    private void FixedUpdate()
    {
        if (loadout.inGame && loadout.matchSetUp == false)
        {
            setMatchUp();
        }

        
        Gun = loadout.guns[loadout.selectedGun];
       
            
            
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        if (look.magnitude > 1)
        {
            look.Normalize();
        }

        dirMove = new Vector3(move.x, 0, move.y);

        transform.position += dirMove * Time.deltaTime * GetComponentInChildren<Robot>().moveSpeed;
        if(dirMove.magnitude > 0)
        {
            roboLower.SetBool("Run Forwards", true);
        }
        else
        {
            roboLower.SetBool("Run Forwards", false);
        }

        float angle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg;
        if (look.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        }
        
    }

    private void Update()
    {
        timeInRound += Time.deltaTime;

        if(GetComponentInChildren<Robot>().overcharged)
        {
            StartCoroutine(overCharged());
           
        }

        //shooting
        if (usingGun)
        {
            if (hasShot && gameObject.GetComponentInChildren<Robot>().energyCapacity >= Gun.GetComponent<gun>().energyCost)
            {
                if(Gun.GetComponent<gun>().gunType == "heavy")
                {
                    roboUpper.SetBool("HeavyFire", true);
                    StartCoroutine(heavyShotAnimationStop());
                }

                if (Gun.GetComponent<gun>().gunType == "light")
                {
                    roboUpper.SetBool("LightFire", true);
                    StartCoroutine(lightShotAnimationStop());
                }

                hasShot = false;
                StartCoroutine(shotCooldown());
                if(Gun.GetComponent<gun>().gunName == "Shotgun A")
                {
                    
                    GameObject bulletClone = Instantiate(Gun.GetComponent<gun>().bullet, firespot.position, firespot.rotation * Quaternion.AngleAxis(0, new Vector3(0, 10, 0)));                    
                    GameObject bulletClone1 = Instantiate(Gun.GetComponent<gun>().bullet, firespot.position, firespot.rotation * Quaternion.AngleAxis(-5, new Vector3(0,10,0)));
                    GameObject bulletClone2 = Instantiate(Gun.GetComponent<gun>().bullet, firespot.position, firespot.rotation * Quaternion.AngleAxis(10, new Vector3(0, 10, 0)));
                    GameObject bulletClone3 = Instantiate(Gun.GetComponent<gun>().bullet, firespot.position, firespot.rotation * Quaternion.AngleAxis(5, new Vector3(0, 10, 0)));
                    GameObject bulletClone4 = Instantiate(Gun.GetComponent<gun>().bullet, firespot.position, firespot.rotation * Quaternion.AngleAxis(-10, new Vector3(0, 10, 0)));

                }
                else
                {
                    GameObject bulletClone = Instantiate(Gun.GetComponent<gun>().bullet, firespot.position, firespot.rotation);
                }
                
                Gun.GetComponent<AudioSource>().Play();

                //new shooting with overcharge meter
                //check if theres any overcharge
                if(GetComponentInChildren<Robot>().overchargeCapacity > 0)
                {
                    //if more overcharge than cost, use overcahrge to shoot, otherwise subtract all overcharge and the leftover from the energy capacity
                    if(GetComponentInChildren<Robot>().overchargeCapacity >= Gun.GetComponent<gun>().energyCost)
                    {
                        GetComponentInChildren<Robot>().overchargeCapacity -= Gun.GetComponent<gun>().energyCost;
                    }
                    else
                    {
                        float leftover = Gun.GetComponent<gun>().energyCost - GetComponentInChildren<Robot>().overchargeCapacity;
                        GetComponentInChildren<Robot>().overchargeCapacity -= Gun.GetComponent<gun>().energyCost;                        
                        GetComponentInChildren<Robot>().energyCapacity -= leftover;
                    }
                    
                }
                //if no overcharge use the regualr energy
                else
                {
                    GetComponentInChildren<Robot>().energyCapacity -= Gun.GetComponent<gun>().energyCost;
                }
                
                

            }
                       
        }

        if (!usingGun)
        {
            if (hasShieldBash && GetComponentInChildren<Robot>().energyCapacity >= shieldBashCost)
            {
                if (GetComponentInChildren<Robot>().overchargeCapacity > 0)
                {
                    //if more overcharge than cost, use overcahrge to shoot, otherwise subtract all overcharge and the leftover from the energy capacity
                    if (GetComponentInChildren<Robot>().overchargeCapacity >= shieldBashCost)
                    {
                        GetComponentInChildren<Robot>().overchargeCapacity -= shieldBashCost;
                    }
                    else
                    {
                        float leftover = shieldBashCost - GetComponentInChildren<Robot>().overchargeCapacity;
                        GetComponentInChildren<Robot>().overchargeCapacity -= shieldBashCost;
                        GetComponentInChildren<Robot>().energyCapacity -= leftover;
                    }

                }
                //if no overcharge use the regualr energy
                else
                {
                    GetComponentInChildren<Robot>().energyCapacity -= shieldBashCost;
                }

                hasShieldBash = false;                
                StartCoroutine(shieldBashCooldown());
            }
        }


        //swapping
        if (hasSwapped)
        {
            audioSource.clip = sfx[0];
            audioSource.Play();
            hasSwapped = false;
            
            StartCoroutine(swapCooldown());
        }
        
        if (!usingGun)
        {            
            if (GetComponentInChildren<Robot>().decharging == false)
            {
                if (GetComponentInChildren<Robot>().energyCapacity < GetComponentInChildren<Robot>().maxEnergyCapacity)
                {
                    if(timeInRound < 20f)
                    {
                        GetComponentInChildren<Robot>().energyCapacity += (6f * Time.deltaTime);
                    }
                    else if(20f < timeInRound && timeInRound < 40f)
                    {
                        GetComponentInChildren<Robot>().energyCapacity += (12f * Time.deltaTime);
                    }
                    else if (timeInRound > 40f)
                    {
                        GetComponentInChildren<Robot>().energyCapacity += (24f * Time.deltaTime);
                    }


                }
                else if (GetComponentInChildren<Robot>().energyCapacity >= GetComponentInChildren<Robot>().maxEnergyCapacity)
                {
                    GetComponentInChildren<Robot>().overchargeCapacity += (6f * Time.deltaTime);
                }
            }           
             
            
        }
        
        if(gameObject.GetComponentInChildren<Robot>().health <= 0 && deathAccounted == false)
        {
            audioSource.clip = sfx[1];
            audioSource.Play();
            alive = false;
            gameManager.GetComponent<GameManager>().playersAlive -= 1;
            deathAccounted = true;


            //if you are playing as brains and have perk 2 selected activate it
            if (loadout.selectedPilot == 3 && loadout.selectedPerk == 1)
            {
                loadout.robotHeads[loadout.selectedPilot].GetComponent<BrainsPerk>().phoenix = true;
                
            }

        }

               
    }

    IEnumerator heavyShotAnimationStop()
    {
        yield return new WaitForSeconds(0.5f);
        roboUpper.SetBool("HeavyFire", false);
    }

    IEnumerator lightShotAnimationStop()
    {
        yield return new WaitForSeconds(0.25f);
        roboUpper.SetBool("LightFire", false);
    }

    IEnumerator shieldBashAnimationStop()
    {
        yield return new WaitForSeconds(0.25f);
        roboUpper.SetBool("ShieldBash", false);
    }
    IEnumerator shotCooldown()
    {
        shotOnCooldown = true;
        yield return new WaitForSeconds(Gun.GetComponent<gun>().fireRate);
        
        shotOnCooldown = false;
    }

    IEnumerator shieldBashCooldown()
    {
        shieldBashOnCooldown = true;
        audioSource.clip = sfx[3];
        audioSource.Play();
        roboUpper.SetBool("ShieldBash", true);
        StartCoroutine(shieldBashAnimationStop());
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 3f);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.transform != gameObject.transform)
            {
                if (hitCollider.gameObject.CompareTag("robot"))
                {
                    Vector3 dir = hitCollider.gameObject.transform.position - gameObject.transform.position;

                    hitCollider.GetComponent<Rigidbody>().AddForce(dir * 200);
                }
            }
           
            
        }
        yield return new WaitForSeconds(1f);
        shieldBashOnCooldown = false;
    }


    IEnumerator overCharged()
    {
        swapOnCooldown = true;
        Gun.SetActive(false);
        Shield.SetActive(false);
        GetComponentInChildren<Robot>().energyCapacity = GetComponentInChildren<Robot>().maxEnergyCapacity;
        GetComponentInChildren<Robot>().overchargeCapacity = 0f;
        yield return new WaitForSeconds(3f);
        GetComponentInChildren<Robot>().decharging = false;

        if (usingGun)
        {
            Gun.SetActive(false);
            Shield.SetActive(true);
            usingGun = false;
            roboUpper.SetBool("ShieldOut", true);
        }
        else
        {
            Gun.SetActive(true);
            Shield.SetActive(false);
            usingGun = true;
            roboUpper.SetBool("ShieldOut", false);
        }

        swapOnCooldown = false;

    }

    
    IEnumerator swapCooldown()
    {
        swapOnCooldown = true;
        

        if(usingGun)
        {
            Gun.SetActive(false);
            Shield.SetActive(true);
            usingGun = false;
            roboUpper.SetBool("ShieldOut", true);
        }
        else
        {
            Gun.SetActive(true);
            Shield.SetActive(false);
            usingGun = true;
            roboUpper.SetBool("ShieldOut", false);
        }
        
        yield return new WaitForSeconds(0.5f);
        swapOnCooldown = false;
        hasSwapped = false;
    }

    public void setMatchUp()
    {
        timeInRound = 0;
        alive = true;
        StartCoroutine(InvincibleStart());
        foreach (Transform child in transform)
        {
            if (child.CompareTag("gun"))
            {
                
                Gun.transform.SetParent(robotArm);
                Gun.transform.localPosition = new Vector3(0.55f, -2.52f, -1.38f);
                Gun.transform.localRotation = Quaternion.Euler(-50.919f, -0.626f, -83.462f);
                
            }
        }


        string shieldSize = GetComponentInChildren<Robot>().shieldSize;

        switch (shieldSize)
        {
            case "small":
                shield.transform.localScale = new Vector3(10f, 14f, 1.2f);
                break;
            case "medium":
                shield.transform.localScale = new Vector3(10f, 16f, 1.2f);
                break;
            case "large":
                shield.transform.localScale = new Vector3(10f, 18f, 1.2f);
                break;
        }

        GetComponent<Rigidbody>().useGravity = true;
        playerUI.enabled = true;
        transform.position = gameManager.spawns[gameManager.randomSpawns[loadout.PlayerIndex]].transform.position;        
        transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        loadout.matchSetUp = true;
    }

    public void newRound()
    {
        timeInRound = 0;
        //if you are playing as brains and have perk 2 selected activate it
        if (loadout.selectedPilot == 3 && loadout.selectedPerk == 2)
        {
            loadout.robotHeads[loadout.selectedPilot].GetComponent<BrainsPerk>().learner = true;
            
        }

        StartCoroutine(InvincibleStart());

        GetComponentInChildren<Robot>().health = GetComponentInChildren<Robot>().maxHealth;
        GetComponentInChildren<Robot>().energyCapacity = GetComponentInChildren<Robot>().maxEnergyCapacity;
        transform.position = gameManager.spawns[gameManager.randomSpawns[loadout.PlayerIndex]].transform.position;
        alive = true;

        if(GetComponentInChildren<IdolPerk>() != null)
        {
            GetComponentInChildren<IdolPerk>().shields = false;
            GetComponentInChildren<IdolPerk>().activatedPerk1 = false;
        }

        if(GetComponentInChildren<MiragePerk>() != null)
        {
            GetComponentInChildren<MiragePerk>().fakeRobotSpawned = false;
        }

        if (GetComponentInChildren<ForgePerk>() != null)
        {
            GetComponentInChildren<ForgePerk>().perkSet = false;
        }


        deathAccounted = false;

    }

    public void death()
    {       

        transform.position = new Vector3(1000, 1000, 1000);
    }

    

    IEnumerator SetPauseAllow()
    {
        yield return new WaitForSeconds(2f);
        pauseAllowed = true;
    }

    IEnumerator InvincibleStart()
    {
        invincible = true;
        yield return new WaitForSeconds(2f);
        invincible = false;
    }

}
