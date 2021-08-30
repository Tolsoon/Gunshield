using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    public float speed;
    public float life;
    public float damage;
    public float damageMultiplier = 1.2f;
    public float energyCost;

    public List<AudioClip> sounds;

    public GameObject whoShot;

    public gun Gun;

    public loadoutSelection loadout;

    AudioSource audioSource;

    //boolean for recon perk to do 20% more damage against opponets with 50% health or less
    public bool execute;

    //boolean for brains perk where getting a kill boosts his stats
    public bool reaper;

    //boolean for yuri perk where she gets speed boost after htting enemy
    public bool speedAfterHit;
    

    
    
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        whoShot = loadout.robotHeads[loadout.selectedPilot].GetComponentInParent<MoveTest3>().gameObject;
    }

    void FixedUpdate()
    {
        
       
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, Time.deltaTime * speed))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rat = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rat, 0);


            if (hit.transform.gameObject.CompareTag("wall"))
            {
                audioSource.clip = sounds[0];
                audioSource.Play();
                life -= 1;
                damage *= damageMultiplier;
            }

            if (hit.transform.gameObject.CompareTag("shield"))
            {
                audioSource.clip = sounds[1];
                audioSource.Play();
                life -= 1;
                damage *= damageMultiplier;

                if (hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<SnailPerk>() != null )
                {
                    //snail regen perk
                    if(hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<SnailPerk>().regen == true)
                    {
                        
                        float healthRestore = energyCost / 2f;
                        hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().health += healthRestore;

                        if (hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().health > hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().maxHealth)
                        {
                            hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().health = hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().maxHealth;
                        }
                    }                  

                    
                }
                if (hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<IdolPerk>() != null)
                {
                    //idol regen perk
                    if (hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<IdolPerk>().regen == true)
                    {
                        Debug.Log("recognize idol perk activte");
                        float healthRestore = energyCost / 2f;
                        hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().health += healthRestore;

                        if (hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().health > hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().maxHealth)
                        {
                            hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().health = hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().maxHealth;
                        }
                    }
                }




                    float energyRestore = energyCost / 2f;
                float difference = hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().maxEnergyCapacity - hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().energyCapacity;

                if (difference >= energyRestore)
                {
                    hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().energyCapacity += energyRestore;
                }
                else if(difference < energyRestore)
                {                  
                    
                    float leftover = energyRestore - difference;
                    
                    hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().energyCapacity += difference;
                    hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().overchargeCapacity += leftover;
                }

                whoShot = hit.transform.GetComponentInParent<MoveTest3>().gameObject;
                GetComponent<TrailRenderer>().material.color = whoShot.GetComponent<MoveTest3>().loadout.glowColor;
                GetComponent<TrailRenderer>().material.SetColor("_EmissionColor", whoShot.GetComponent<MoveTest3>().loadout.glowColor);


            }

            if (hit.transform.gameObject.CompareTag("robot"))
            {
                //for recons perk
                if (execute && hit.transform.gameObject.GetComponentInChildren<Robot>().health <= (hit.transform.gameObject.GetComponentInChildren<Robot>().maxHealth / 2))
                {
                    hit.transform.gameObject.GetComponentInChildren<Robot>().health -= (damage *.2f);
                }

                if (speedAfterHit)
                {
                    whoShot.GetComponentInChildren<YuriPerk>().speed();
                }

                if (hit.transform.gameObject.GetComponent<MoveTest3>().invincible == false)
                {
                    hit.transform.gameObject.GetComponentInChildren<Robot>().health -= damage;
                    hit.transform.gameObject.GetComponent<MoveTest3>().audioSource.clip = hit.transform.gameObject.GetComponent<MoveTest3>().sfx[2];
                    hit.transform.gameObject.GetComponent<MoveTest3>().audioSource.Play();
                }

                


                if (hit.transform.gameObject.GetComponentInChildren<Robot>().health <= 0  && hit.transform != whoShot.transform)
                {
                    if (hit.transform.gameObject.GetComponent<MoveTest3>().alive)
                    {
                        whoShot.GetComponentInChildren<PlayerScore>().addScore();
                        hit.transform.gameObject.GetComponent<MoveTest3>().alive = false;
                    }                  

                    //if playing as brains and you ahve perk1 selected
                    if (reaper)
                    {                        
                        loadout.robotHeads[loadout.selectedPilot].GetComponent<Robot>().moveSpeed += 0.5f;                        
                        loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().damage += (loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().damage * 0.1f);
                        loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().damage += (loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().damage * 0.1f);
                        
                    }

                }
                Destroy(gameObject);
            }


        }

        if(life < 0)
        {
            Destroy(gameObject);
        }
        
      

    }

    public void setGunUp()
    {
        Gun = loadout.guns[loadout.selectedGun].gameObject.GetComponent<gun>();

        speed = Gun.bulletSpeed;
        life = Gun.bounce;
        damage = Gun.damage;
        energyCost = Gun.energyCost;
    }
}
