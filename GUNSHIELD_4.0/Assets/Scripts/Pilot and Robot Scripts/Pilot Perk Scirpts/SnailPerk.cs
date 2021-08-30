using System.CodeDom;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SnailPerk : MonoBehaviour
{
    public GameObject thisRobot;


    public bool snailSelected;

    public loadoutSelection loadout;

    bool perk1 = false;
    bool perk2 = false;
    bool perk3 = false;
    bool perkSet;

    public bool regen;

    //snail perk parts arrays
    public GameObject[] snailPerk1;
    public GameObject[] snailPerk2;
    public GameObject[] snailPerk3;


    //stuff to set the right color on perk parts
    public GameObject[] allParts;
    bool gotMats = false;
    public List<Material> glowMats = new List<Material>();
    public Color glowColor;


    
    float originalDamage;
    bool slumped;
    bool setLastPos;
    public void Update()
    {
        if (loadout.selectedPilot == 5)
        {
            snailSelected = true;
        }
        else
        {
            snailSelected = false;
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
                loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().speed *= 0.5f;                

                loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().life *= 2f;
                
                perkSet = true;
            }
            if (perk2)
            {
                regen = true;
                perkSet = true;
            }          


        }

        if (loadout.inGame)
        {
            if (perk3)
            {
                if (!setLastPos)
                {
                    
                    originalDamage = loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().damage;
                    setLastPos = true;
                }
                   
                if (thisRobot.GetComponentInChildren<MoveTest3>().move.x > 0 || thisRobot.GetComponentInChildren<MoveTest3>().move.x < 0
                    || thisRobot.GetComponentInChildren<MoveTest3>().move.y > 0 || thisRobot.GetComponentInChildren<MoveTest3>().move.y < 0)
                {
                    slumped = false;
                }
                else
                {
                    slumped = true;
                }
                
               

                if (slumped && !perkSet)
                {
                    
                    loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().damage *= 1.3f;
                    loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().damage *= 1.3f;
                    perkSet = true;
                }
                else if(!slumped)
                {
                    
                    loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().damage = originalDamage;
                    loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().damage = originalDamage;
                    perkSet = false;
                }

            }
        }

        if (snailSelected)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    foreach (GameObject part in snailPerk1)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in snailPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in snailPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 1:
                    foreach (GameObject part in snailPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in snailPerk2)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in snailPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject part in snailPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in snailPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in snailPerk3)
                    {
                        part.SetActive(true);
                    }
                    break;

            }

        }
        else
        {
            foreach (GameObject part in snailPerk1)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in snailPerk2)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in snailPerk3)
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
}
