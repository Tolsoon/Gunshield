using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolPerk : MonoBehaviour
{
    public GameObject thisRobot;


    public bool idolSelected;

    public loadoutSelection loadout;

    bool perk1 = false;
    bool perk2 = false;
    bool perk3 = false;
    bool perkSet;

    //idol perk parts arrays
    public GameObject[] idolPerk1;
    public GameObject[] idolPerk2;
    public GameObject[] idolPerk3;

    //fake bullet
    public GameObject FakeBullet;
    public bool activatedPerk1;

    //regen bool for perk2
    public bool regen;

    //shields bool for perk 3
    public bool shields;
    //stuff to set the right color on perk parts
    public GameObject[] allParts;
    bool gotMats = false;
    public List<Material> glowMats = new List<Material>();
    public Color glowColor;
    public void Update()
    {
        if (loadout.selectedPilot == 6)
        {
            idolSelected = true;
        }
        else
        {
            idolSelected = false;
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
            //spawn 5 fake bullets that deal no damage but bouce around the map, last until they hit enemy robot
            if (perk1)
            {
                if(GetComponent<Robot>().health < GetComponent<Robot>().maxHealth * 0.5f && !activatedPerk1)
                {
                    Instantiate(FakeBullet, thisRobot.GetComponent<MoveTest3>().firespot.position, thisRobot.GetComponent<MoveTest3>().firespot.rotation * Quaternion.AngleAxis(50, new Vector3(3, 10, 0)));
                    Instantiate(FakeBullet, thisRobot.GetComponent<MoveTest3>().firespot.position, thisRobot.GetComponent<MoveTest3>().firespot.rotation * Quaternion.AngleAxis(150, new Vector3(1, 10, 0)));
                    Instantiate(FakeBullet, thisRobot.GetComponent<MoveTest3>().firespot.position, thisRobot.GetComponent<MoveTest3>().firespot.rotation * Quaternion.AngleAxis(275, new Vector3(0, 10, 0)));
                    Instantiate(FakeBullet, thisRobot.GetComponent<MoveTest3>().firespot.position, thisRobot.GetComponent<MoveTest3>().firespot.rotation * Quaternion.AngleAxis(200, new Vector3(5, 10, 0)));
                    Instantiate(FakeBullet, thisRobot.GetComponent<MoveTest3>().firespot.position, thisRobot.GetComponent<MoveTest3>().firespot.rotation * Quaternion.AngleAxis(335, new Vector3(5, 10, 0)));
                    activatedPerk1 = true;
                }
            }
            //when dropped below 30% health bullets hitting your shield will now restore health
            if (perk2)
            {
                if(GetComponent<Robot>().health < GetComponent<Robot>().maxHealth * 0.5f)
                {
                    regen = true;
                }
                else
                {
                    regen = false;
                }
            }
            //when dropped below 40% health disable all guns and force shields out
            if (perk3)
            {
                if(GetComponent<Robot>().health < GetComponent<Robot>().maxHealth * 0.4f && !shields)
                {
                    StartCoroutine(ShieldsOut());
                }
                                
            }

            

        }

        if (idolSelected)
        {
            switch (loadout.selectedPerk)
            {
                case 0:
                    foreach (GameObject part in idolPerk1)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in idolPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in idolPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 1:
                    foreach (GameObject part in idolPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in idolPerk2)
                    {
                        part.SetActive(true);
                    }
                    foreach (GameObject part in idolPerk3)
                    {
                        part.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject part in idolPerk1)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in idolPerk2)
                    {
                        part.SetActive(false);
                    }
                    foreach (GameObject part in idolPerk3)
                    {
                        part.SetActive(true);
                    }
                    break;

            }

        }
        else
        {
            foreach (GameObject part in idolPerk1)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in idolPerk2)
            {
                part.SetActive(false);
            }
            foreach (GameObject part in idolPerk3)
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


    IEnumerator ShieldsOut()
    {
        foreach (GameObject player in thisRobot.GetComponent<MoveTest3>().gameManager.players)
        {
            if (player.GetComponentInChildren<MoveTest3>().usingGun)
            {
                player.GetComponentInChildren<MoveTest3>().Gun.SetActive(false);
                player.GetComponentInChildren<MoveTest3>().Shield.SetActive(true);
                player.GetComponentInChildren<MoveTest3>().usingGun = false;
                player.GetComponentInChildren<MoveTest3>().roboUpper.SetBool("ShieldOut", true);
            }
        }

        shields = true;

        yield return new WaitForSeconds(7f);

        foreach (GameObject player in thisRobot.GetComponent<MoveTest3>().gameManager.players)
        {
            player.GetComponentInChildren<MoveTest3>().Gun.SetActive(true);
            player.GetComponentInChildren<MoveTest3>().Shield.SetActive(false);
            player.GetComponentInChildren<MoveTest3>().usingGun = true;
            player.GetComponentInChildren<MoveTest3>().roboUpper.SetBool("ShieldOut", false);
        }
       
    }
}
