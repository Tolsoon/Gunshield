using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgePerk : MonoBehaviour
{
    public GameObject thisRobot;

    public loadoutSelection loadout;

    
    bool isHeal = false;



    bool perk1 = false;
    bool perk2 = false;
    bool perk3 = false;
    public bool perkSet;

    public bool forgeSelected;

    //forge perk parts arrays
    public GameObject[] forgePerk1;
    public GameObject[] forgePerk2;
    public GameObject[] forgePerk3;
    
    //stuff to set the right color on perk parts
    public GameObject[] allParts;
    bool gotMats = false;
    public List<Material> glowMats = new List<Material>();
    public Color glowColor;

    public void Update()
    {
        if (loadout.selectedPilot == 0)
        {
            forgeSelected = true;
        }
        else
        {
            forgeSelected = false;
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


        if (forgeSelected)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    foreach (GameObject part in forgePerk1)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in forgePerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in forgePerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 1:
                    foreach (GameObject part in forgePerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in forgePerk2)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in forgePerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject part in forgePerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in forgePerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in forgePerk3)
                    {
                        part.SetActive(true);
                    }
                    break;

            }

        }
        else
        {
            foreach (GameObject part in forgePerk1)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in forgePerk2)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in forgePerk3)
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

    


        //forge perks
        if (loadout.inGame == true)
        {
            if (perk1 && !perkSet)
            {
                
                //gain 20 health upon death, backup battery perk

                if (loadout.robotHeads[loadout.selectedPilot].GetComponent<Robot>().health < 25 && loadout.robotHeads[loadout.selectedPilot].GetComponent<Robot>().health > 1)
                {
                    
                    loadout.robotHeads[loadout.selectedPilot].GetComponent<Robot>().health += 25f;
                    perkSet = true;
                }

            }

            if (perk2)
            {
                
                if (thisRobot.GetComponent<MoveTest3>().move.magnitude == 0 && isHeal == false && loadout.robotHeads[loadout.selectedPilot].GetComponent<Robot>().health < loadout.robotHeads[loadout.selectedPilot].GetComponent<Robot>().maxHealth)
                {
                    StartCoroutine(healing());
                    
                }

            }

            if (perk3 && !perkSet)
            {
                if (GetComponent<Robot>().health < 20)
                {
                    thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().fireRate -= thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().fireRate * 0.2f;
                    
                    perkSet = true;
                }

            }


        }
        


        IEnumerator healing()
        {
            isHeal = true;
            GetComponent<Robot>().health += 5f;
            yield return new WaitForSeconds(5f);
            isHeal = false;
        }

    }
}
