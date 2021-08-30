using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiragePerk : MonoBehaviour
{
    public GameObject thisRobot;


    public bool mirageSelected;

    public loadoutSelection loadout;

    bool perk1 = false;
    bool perk2 = false;
    bool perk3 = false;
    bool perkSet;

    //gun and shield for bug fix
    public GameObject gunGO = null;
    public GameObject shield;

    //mirage perk parts arrays
    public GameObject[] miragePerk1;
    public GameObject[] miragePerk2;
    public GameObject[] miragePerk3;

    //for perk 1
    public GameObject fakeRobot;
    public bool fakeRobotSpawned;

    //bool for time spent moving
    public float movingTime;
    public Component[] meshRendere;
    public bool invisible;
    public bool invisibleCooldown = false;

    //bool for increasing gun damage and getting orginal dmagae
    public float orginalDamage;
    public bool increasedDamage;
    public Canvas aimCanvas;

    //stuff to set the right color on perk parts
    public GameObject[] allParts;
    bool gotMats = false;
    public List<Material> glowMats = new List<Material>();
    public Color glowColor;
    public void Update()
    {
        if (loadout.selectedPilot == 7)
        {
            mirageSelected = true;
        }
        else
        {
            mirageSelected = false;
        }

        if (loadout.inGame == false)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    perk1 = true; perk2 = false; perk3 = false;
                    break;
                case 1:
                    perk1 = false; perk2 = true; perk3 = false;
                    break;
                case 2:
                    perk1 = false; perk2 = false; perk3 = true;
                    break;

            }

        }

        if (loadout.inGame == true )
        {
            gunGO = GetComponentInParent<MoveTest3>().Gun;
            //after dropping below 50% health go invisible and leave a fake mirage behind
            if (perk1)
            {

                if(GetComponent<Robot>().health < GetComponent<Robot>().maxHealth * 0.5f && !fakeRobotSpawned)
                {
                    
                    Vector3 currentPos = thisRobot.GetComponentInChildren<MoveTest3>().transform.position;
                    Instantiate(fakeRobot, currentPos, Quaternion.identity);
                    fakeRobotSpawned = true;
                    invisible = true;
                    StartCoroutine(ShortInvisible());
                }
               
            }
            //after moving for 5 seconds become invisible
            if (perk2)
            {
                if (thisRobot.GetComponentInChildren<MoveTest3>().move.x > 0 || thisRobot.GetComponentInChildren<MoveTest3>().move.x < 0
                    || thisRobot.GetComponentInChildren<MoveTest3>().move.y > 0 || thisRobot.GetComponentInChildren<MoveTest3>().move.y < 0)
                {
                    movingTime += Time.deltaTime;
                    
                }
                else
                {
                    movingTime = 0f;
                }

                if (movingTime >= 3f && !invisible && invisibleCooldown == false)
                {
                    StartCoroutine(Invisible());
                    invisible = true;
                    
                }
               
            }

            
            //first shot fired does increased damaage
            if (perk3)
            {
                if (!perkSet)
                {
                    orginalDamage = thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().damage;

                }


                if(GetComponent<Robot>().energyCapacity >= GetComponent<Robot>().maxEnergyCapacity && !increasedDamage)
                {
                   
                    thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().bullet.GetComponent<bulletMove>().damage *= 2f;
                    
                    increasedDamage = true;
                }
                else if(GetComponent<Robot>().energyCapacity < GetComponent<Robot>().maxEnergyCapacity)
                {
                    
                    thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().bullet.GetComponent<bulletMove>().damage = orginalDamage;
                    thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().bullet0.GetComponent<bulletMove>().damage = orginalDamage;

                    increasedDamage = false;
                }
                
                
            }

            perkSet = true;

        }

        if (mirageSelected)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    foreach (GameObject part in miragePerk1)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in miragePerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in miragePerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 1:
                    foreach (GameObject part in miragePerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in miragePerk2)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in miragePerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject part in miragePerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in miragePerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in miragePerk3)
                    {
                        part.SetActive(true);
                    }
                    break;

            }

        }
        else
        {
            foreach (GameObject part in miragePerk1)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in miragePerk2)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in miragePerk3)
            {
                part.SetActive(false);
            }
        }


        //setting color of the perk parts
        //get color
        glowColor = loadout.glowColor;
        //get the materials you need tyo change
        if (gotMats == false)
        {
            foreach (GameObject part in allParts)
            {
                foreach (Material matt in part.GetComponent<MeshRenderer>().materials)
                {
                    if (matt.name == "Glow (Instance)")
                    {

                        glowMats.Add(matt);
                    }
                }
            }

            gotMats = true;
        }

        //change the material color
        foreach (Material matt in glowMats)
        {
            matt.color = glowColor;
            matt.SetColor("_EmissionColor", glowColor);
        }

        if (invisible)
        {
            gunGO.GetComponent<MeshRenderer>().enabled = false;
            shield.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gunGO.GetComponent<MeshRenderer>().enabled = true;
            shield.GetComponent<MeshRenderer>().enabled = true;
        }

    }

    IEnumerator Invisible()
    {
        StartCoroutine(StartInvisibleCooldown());
        aimCanvas.enabled = false;
        meshRendere = thisRobot.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshRendere)
        {
            mesh.enabled = false;
        }

        

        yield return new WaitForSeconds(4f);


        meshRendere = thisRobot.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshRendere)
        {
            mesh.enabled = true;
        }
        

        aimCanvas.enabled = true;
        invisible = false;
    }

    IEnumerator ShortInvisible()
    {
        aimCanvas.enabled = false;
        meshRendere = thisRobot.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshRendere)
        {
            mesh.enabled = false;
        }
       

        yield return new WaitForSeconds(3f);


        meshRendere = thisRobot.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshRendere)
        {
            mesh.enabled = true;
        }
    
        aimCanvas.enabled = true;
        invisible = false;
    }

    IEnumerator StartInvisibleCooldown()
    {
        invisibleCooldown = true;
        yield return new WaitForSeconds(10f);
        invisibleCooldown = false;

    }
}
