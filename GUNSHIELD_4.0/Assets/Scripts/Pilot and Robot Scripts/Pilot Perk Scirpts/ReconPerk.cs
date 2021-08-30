using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReconPerk : MonoBehaviour
{
    public GameObject thisRobot;

    public loadoutSelection loadout;

    bool perk1 = false;
    bool perk2 = false;
    bool perk3 = false;
    bool perkSet;

    public bool reconSelected;

    //recon perk parts arrays
    public GameObject[] reconPerk1;
    public GameObject[] reconPerk2;
    public GameObject[] reconPerk3;

    //stuff to set the right color on perk parts
    public GameObject[] allParts;
    bool gotMats = false;
    public List<Material> glowMats = new List<Material>();
    public Color glowColor;

    public GameObject laserSight;
    public void Update()
    {
        if (loadout.selectedPilot == 2)
        {
            reconSelected = true;
        }
        else
        {
            reconSelected = false;
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
                    laserSight.SetActive(false);
                    break;
                case 2:
                    perk1 = false; perk2 = false; perk3 = true;
                    laserSight.SetActive(false);
                    break;

            }

        }


        if (reconSelected)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    foreach (GameObject part in reconPerk1)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in reconPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in reconPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 1:
                    foreach (GameObject part in reconPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in reconPerk2)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in reconPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject part in reconPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in reconPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in reconPerk3)
                    {
                        part.SetActive(true);
                    }
                    break;

            }

        }
        else
        {
            foreach (GameObject part in reconPerk1)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in reconPerk2)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in reconPerk3)
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




        //recon perks
        if (loadout.inGame == true && !perkSet)
        {
            if (perk1)
            {

                //laser sighjt
                laserSight.SetActive(true);
                laserSight.GetComponent<LineRenderer>().material.color = loadout.glowColor;
                laserSight.GetComponent<LineRenderer>().material.SetColor("_EmissionColor",loadout.glowColor);
                perkSet = true;

            }

            if (perk2)
            {
                //execture
                loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet.GetComponent<bulletMove>().execute = true;
                loadout.guns[loadout.selectedGun].GetComponent<gun>().bullet0.GetComponent<bulletMove>().execute = true;
            }

            if (perk3)
            {
                //increased movement speed after shield bashing
                if (GetComponentInParent<MoveTest3>().shieldBashOnCooldown)
                {
                    Debug.Log("doing this");
                    StartCoroutine(SpeedUp());
                }

            }


        }

        

    }

    IEnumerator SpeedUp()
    {
        perkSet = true;
        float orginalSpeed = GetComponent<Robot>().moveSpeed;
        GetComponent<Robot>().moveSpeed = GetComponent<Robot>().moveSpeed + (GetComponent<Robot>().moveSpeed * 0.3f);
        yield return new WaitForSeconds(5f);
        GetComponent<Robot>().moveSpeed = orginalSpeed;
        perkSet = false;
    }
}
