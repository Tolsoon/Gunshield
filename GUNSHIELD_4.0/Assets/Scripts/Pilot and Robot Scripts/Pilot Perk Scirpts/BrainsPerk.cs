using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainsPerk : MonoBehaviour
{
    public GameObject thisRobot;


    public bool brainsSelected;

    public loadoutSelection loadout;

    bool perk1 = false;
    bool perk2 = false;
    bool perk3 = false;
    bool perkSet;

    public bool phoenix = false;
    public bool learner = false;

    //brains perk parts arrays
    public GameObject[] brainsPerk1;
    public GameObject[] brainsPerk2;
    public GameObject[] brainsPerk3;

    //stuff to set the right color on perk parts
    public GameObject[] allParts;
    bool gotMats = false;
    public List<Material> glowMats = new List<Material>();
    public Color glowColor;
    public void Update()
    {
        if (loadout.selectedPilot == 3)
        {
            brainsSelected = true;
        }
        else
        {
            brainsSelected = false;
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
                loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().reaper = true;
                loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().reaper = true;

            }
            if (perk2)
            {                
                

                if (phoenix)
                {
                    gameObject.GetComponent<Robot>().maxHealth += 10f;
                    gameObject.GetComponent<Robot>().maxEnergyCapacity += 10f;
                    phoenix = false;
                    
                }
            }
            if (perk3)
            {                
                
                if (learner)
                {
                    gameObject.GetComponent<Robot>().maxHealth += 5f;
                    gameObject.GetComponent<Robot>().moveSpeed += 0.25f;
                    gameObject.GetComponent<Robot>().maxEnergyCapacity += 5f;
                    loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().damage += (loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().damage * 0.05f); 
                    loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().damage += (loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().damage * 0.05f);
                    learner = false;

                    gameObject.GetComponent<Robot>().health = gameObject.GetComponent<Robot>().maxHealth;
                    gameObject.GetComponent<Robot>().energyCapacity = gameObject.GetComponent<Robot>().maxEnergyCapacity;
                    gameObject.GetComponent<Robot>().maxOverchargeCapacity = gameObject.GetComponent<Robot>().maxEnergyCapacity * 0.2f; ;
                    
                }
            }

           

        }

        if (brainsSelected)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    foreach (GameObject part in brainsPerk1)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in brainsPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in brainsPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 1:
                    foreach (GameObject part in brainsPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in brainsPerk2)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in brainsPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject part in brainsPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in brainsPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in brainsPerk3)
                    {
                        part.SetActive(true);
                    }
                    break;

            }

        }
        else
        {
            foreach (GameObject part in brainsPerk1)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in brainsPerk2)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in brainsPerk3)
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
