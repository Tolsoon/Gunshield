using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsActivator : MonoBehaviour
{
    public loadoutSelection loadout;

    public GameObject[] forgeParts;
    public GameObject[] queenParts;
    public GameObject[] reconParts;
    public GameObject[] brainsParts;
    public GameObject[] yuriParts;
    public GameObject[] snailParts;
    public GameObject[] idolParts;
    public GameObject[] mirageParts;

    public void Update()
    {
       if(loadout!= null)
        {
            switch (loadout.selectedPilot)
            {
                case 0:
                    allPartsInactive();
                    foreach (GameObject part in forgeParts)
                    {
                        part.SetActive(true);
                    }
                    break;
                case 1:
                    allPartsInactive();
                    foreach (GameObject part in queenParts)
                    {
                        part.SetActive(true);
                    }
                    break;
                case 2:
                    allPartsInactive();
                    foreach (GameObject part in reconParts)
                    {
                        part.SetActive(true);
                    }
                    break;
                case 3:
                    allPartsInactive();
                    foreach (GameObject part in brainsParts)
                    {
                        part.SetActive(true);
                    }
                    break;
                case 4:
                    allPartsInactive();
                    foreach (GameObject part in yuriParts)
                    {
                        part.SetActive(true);
                    }
                    break;
                case 5:
                    allPartsInactive();
                    foreach (GameObject part in snailParts)
                    {
                        part.SetActive(true);
                    }
                    break;
                case 6:
                    allPartsInactive();
                    foreach (GameObject part in idolParts)
                    {
                        part.SetActive(true);
                    }
                    break;
                case 7:
                    allPartsInactive();
                    foreach (GameObject part in mirageParts)
                    {
                        part.SetActive(true);
                    }
                    break;


            }
        }
        
    }

    public void allPartsInactive()
    {
        foreach(GameObject part in forgeParts)
        {
            part.SetActive(false);
        }

        foreach (GameObject part in queenParts)
        {
            part.SetActive(false);
        }

        foreach (GameObject part in reconParts)
        {
            part.SetActive(false);
        }
        foreach (GameObject part in brainsParts)
        {
            part.SetActive(false);
        }
        foreach (GameObject part in yuriParts)
        {
            part.SetActive(false);
        }
        foreach (GameObject part in snailParts)
        {
            part.SetActive(false);
        }
        foreach (GameObject part in idolParts)
        {
            part.SetActive(false);
        }
        foreach (GameObject part in mirageParts)
        {
            part.SetActive(false);
        }
    }
}
