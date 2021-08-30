using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPerk : MonoBehaviour
{
    public GameObject thisRobot;
    

    public bool queenSelected;

    public loadoutSelection loadout;

    bool perk1 = false;
    bool perk2 = false;
    bool perk3 = false;
    bool perkSet;

    //queen perk parts arrays
    public GameObject[] queenPerk1;
    public GameObject[] queenPerk2;
    public GameObject[] queenPerk3;
    
    //stuff to set the right color on perk parts
    public GameObject[] allParts;
    bool gotMats = false;
    public List<Material> glowMats = new List<Material>();
    public Color glowColor;
    public void Update()
    {
        if (loadout.selectedPilot == 1)
        {
            queenSelected = true;
        }
        else
        {
            queenSelected = false;
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
                GetComponent<Robot>().health += 20;
                GetComponent<Robot>().maxHealth += 20;
                Debug.Log("health perk");
            }
            if (perk2)
            {
                thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().bullet.GetComponent<bulletMove>().damageMultiplier += 0.2f;
                Debug.Log("damage multy");
            }
            if (perk3)
            {
                thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().fireRate -= (thisRobot.GetComponent<MoveTest3>().Gun.GetComponent<gun>().fireRate * .2f);
                Debug.Log("fire rate");
            }

            perkSet = true;

        }

        if (queenSelected)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    foreach (GameObject part in queenPerk1)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in queenPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in queenPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 1:
                    foreach (GameObject part in queenPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in queenPerk2)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in queenPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject part in queenPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in queenPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in queenPerk3)
                    {
                        part.SetActive(true);
                    }
                    break;

            }

        }
        else
        {
            foreach (GameObject part in queenPerk1)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in queenPerk2)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in queenPerk3)
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

