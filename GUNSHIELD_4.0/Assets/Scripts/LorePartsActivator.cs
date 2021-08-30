using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LorePartsActivator : MonoBehaviour
{
    public PartsActivator partsActivator;

    public float selectedPilot;
    public float selectedPerk;

    public GameObject[] forgeParts1;
    public GameObject[] forgeParts2;
    public GameObject[] forgeParts3;

    public GameObject[] queenParts1;
    public GameObject[] queenParts2;
    public GameObject[] queenParts3;

    public GameObject[] brainsParts1;
    public GameObject[] brainsParts2;
    public GameObject[] brainsParts3;

    public GameObject[] idolParts1;
    public GameObject[] idolParts2;
    public GameObject[] idolParts3;

    public GameObject[] mirageParts1;
    public GameObject[] mirageParts2;
    public GameObject[] mirageParts3;

    public GameObject[] yuriParts1;
    public GameObject[] yuriParts2;
    public GameObject[] yuriParts3;

    public GameObject[] snailParts1;
    public GameObject[] snailParts2;
    public GameObject[] snailParts3;

    public GameObject[] reconParts1;
    public GameObject[] reconParts2;
    public GameObject[] reconParts3;

    public GameObject[] allParts;

    public Text perkText;

    private void Update()
    {
        switch (selectedPilot)
        {
            case 0:
                partsActivator.allPartsInactive();
                foreach (GameObject part in partsActivator.forgeParts)
                {
                    part.SetActive(true);
                }
                break;
            case 1:
                partsActivator.allPartsInactive();
                foreach (GameObject part in partsActivator.queenParts)
                {
                    part.SetActive(true);
                }
                break;
            case 2:
                partsActivator.allPartsInactive();
                foreach (GameObject part in partsActivator.reconParts)
                {
                    part.SetActive(true);
                }
                break;
            case 3:
                partsActivator.allPartsInactive();
                foreach (GameObject part in partsActivator.brainsParts)
                {
                    part.SetActive(true);
                }
                break;
            case 4:
                partsActivator.allPartsInactive();
                foreach (GameObject part in partsActivator.yuriParts)
                {
                    part.SetActive(true);
                }
                break;
            case 5:
                partsActivator.allPartsInactive();
                foreach (GameObject part in partsActivator.snailParts)
                {
                    part.SetActive(true);
                }
                break;
            case 6:
                partsActivator.allPartsInactive();
                foreach (GameObject part in partsActivator.idolParts)
                {
                    part.SetActive(true);
                }
                break;
            case 7:
                partsActivator.allPartsInactive();
                foreach (GameObject part in partsActivator.mirageParts)
                {
                    part.SetActive(true);
                }
                break;
        }

        switch (selectedPerk)
        {
            case 1:
                deactivateAllParts();
                foreach (GameObject part in forgeParts1)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in queenParts1)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in brainsParts1)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in yuriParts1)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in mirageParts1)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in snailParts1)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in reconParts1)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in idolParts1)
                {
                    part.SetActive(true);
                }
                break;
            case 2:
                deactivateAllParts();
                foreach (GameObject part in forgeParts2)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in queenParts2)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in brainsParts2)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in yuriParts2)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in mirageParts2)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in snailParts2)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in reconParts2)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in idolParts2)
                {
                    part.SetActive(true);
                }
                break;
            case 3:
                deactivateAllParts();
                foreach (GameObject part in forgeParts3)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in queenParts3)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in brainsParts3)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in yuriParts3)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in mirageParts3)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in snailParts3)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in reconParts3)
                {
                    part.SetActive(true);
                }
                foreach (GameObject part in idolParts3)
                {
                    part.SetActive(true);
                }
                break;
        }

        switch (selectedPerk)
        {
            case 1:
                perkText.text = "Perk 1 Parts";
                break;
            case 2:
                perkText.text = "Perk 2 Parts";
                break;
            case 3:
                perkText.text = "Perk 3 Parts";
                break;
        }


    }
    
    public void deactivateAllParts()
    {
        foreach(GameObject part in allParts)
        {
            part.SetActive(false);
        }
    }
    public void nextPilot()
    {
        selectedPilot += 1;
        if(selectedPilot > 7)
        {
            selectedPilot = 0;
        }
    }

    public void lastPilot()
    {
        selectedPilot -= 1;
        if(selectedPilot < 0)
        {
            selectedPilot = 7;
        }
    }

    public void nextperk()
    {
        selectedPerk += 1;
        if(selectedPerk > 3)
        {
            selectedPerk = 1;
        }
    }

    public void lastperk()
    {
        selectedPerk -= 1;
        if (selectedPerk < 1)
        {
            selectedPerk = 3;
        }
    }
}
