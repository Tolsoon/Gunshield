using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YuriPerk : MonoBehaviour
{
    public GameObject thisRobot;


    public bool yuriSelected;

    public loadoutSelection loadout;

    bool perk1 = false;
    bool perk2 = false;
    bool perk3 = false;
    bool perkSet;

    //yuri perk parts arrays
    public GameObject[] yuriPerk1;
    public GameObject[] yuriPerk2;
    public GameObject[] yuriPerk3;

    float defaultMoveSpeed;

    float lastHealth;
    bool speedOnCooldown;
    bool getBaseSpeed;

    //stuff to set the right color on perk parts
    public GameObject[] allParts;
    bool gotMats = false;
    public List<Material> glowMats = new List<Material>();
    public Color glowColor;
    public void Update()
    {
        if (loadout.selectedPilot == 4)
        {
            yuriSelected = true;
        }
        else
        {
            yuriSelected = false;
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

        if (loadout.inGame == true && !perkSet)
        {
            if (perk1)
            {
                if(gameObject.GetComponent<Robot>().health <= (gameObject.GetComponent<Robot>().maxHealth * 0.5f))
                {
                    gameObject.GetComponentInParent<MoveTest3>().shield.transform.localScale = new Vector3(14f, 20f, 1.2f);
                }
                else
                {
                    gameObject.GetComponentInParent<MoveTest3>().shield.transform.localScale = new Vector3(14f, 14f, 1.2f);
                }

            }
            if (perk2)
            {
                if (!getBaseSpeed)
                {
                    defaultMoveSpeed = gameObject.GetComponent<Robot>().moveSpeed;
                    lastHealth = gameObject.GetComponent<Robot>().maxHealth;                    
                    getBaseSpeed = true;
                }

                float curHealth = gameObject.GetComponent<Robot>().health;

                Debug.Log("current health    ===  " + curHealth);
                Debug.Log("least health  ===   " +lastHealth);

                if (curHealth != lastHealth)
                {
                    Debug.Log("huuhuhuh");
                    gameObject.GetComponent<Robot>().moveSpeed = defaultMoveSpeed;
                }

                

                gameObject.GetComponent<Robot>().moveSpeed += (1f * Time.deltaTime);

                if(gameObject.GetComponent<Robot>().moveSpeed > (defaultMoveSpeed + 1.5f))
                {
                    Debug.Log("resetting speed");
                    gameObject.GetComponent<Robot>().moveSpeed = defaultMoveSpeed + (defaultMoveSpeed * 0.5f);
                }

                lastHealth = curHealth;
            }

            if (perk3)
            {
                loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().speedAfterHit = true;
                loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().speedAfterHit = true;
            }



        }

        if (yuriSelected)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    foreach (GameObject part in yuriPerk1)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in yuriPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in yuriPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 1:
                    foreach (GameObject part in yuriPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in yuriPerk2)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in yuriPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject part in yuriPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in yuriPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in yuriPerk3)
                    {
                        part.SetActive(true);
                    }
                    break;

            }

        }
        else
        {
            foreach (GameObject part in yuriPerk1)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in yuriPerk2)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in yuriPerk3)
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

    }

    public void speed()
    {
        if (!speedOnCooldown)
        {
            StartCoroutine(SpeedAfterHit());
        }
        
    }

    IEnumerator SpeedAfterHit()
    {
        speedOnCooldown = true;
        float oldSpeed = gameObject.GetComponent<Robot>().moveSpeed;
        gameObject.GetComponent<Robot>().moveSpeed = gameObject.GetComponent<Robot>().moveSpeed * 1.5f;

        yield return new WaitForSeconds(2f);

        gameObject.GetComponent<Robot>().moveSpeed = oldSpeed;
        speedOnCooldown = false;
    }
}
