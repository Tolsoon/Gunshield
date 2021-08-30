using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiControl : MonoBehaviour
{
    public GameObject gun;
    public GameObject robotHead;
    public Slider energySlider;
    public Slider healthSlider;
    public Slider overchargeSlider;

    public Image pilotPortrait;
    public Image perkIcon;
    public Image uiOutline;

    public Image floorEnergy;
    public Image floorOvercharge;
    public Image floorHealth;

    public int playerIndex;
    public Text playerIndexText;

    public GameObject UI;
    public GameObject[] UIspawns;

    public Image aimer;

    public loadoutSelection loadout;

    public void Start()
    {
        playerIndex = loadout.PlayerIndex;
    }

    public void Update()
    {
        


        robotHead = loadout.robotHeads[loadout.selectedPilot];
        pilotPortrait.sprite = loadout.pilotPortraits[loadout.selectedPilot];

        aimer.color = loadout.glowColor;

        switch (loadout.selectedPilot)
        {
            case 0:
                switch (loadout.selectedPerk)
                {
                    case 0:
                        perkIcon.sprite = loadout.forgePerks[0].GetComponent<Image>().sprite;
                        break;
                    case 1:
                        perkIcon.sprite = loadout.forgePerks[1].GetComponent<Image>().sprite;
                        break;
                    case 2:
                        perkIcon.sprite = loadout.forgePerks[2].GetComponent<Image>().sprite;
                        break;
                }
                break;
            case 1:
                switch (loadout.selectedPerk)
                {
                    case 0:
                        perkIcon.sprite = loadout.queenPerks[0].GetComponent<Image>().sprite;
                        break;
                    case 1:
                        perkIcon.sprite = loadout.queenPerks[1].GetComponent<Image>().sprite;
                        break;
                    case 2:
                        perkIcon.sprite = loadout.queenPerks[2].GetComponent<Image>().sprite;
                        break;
                }
                break;
            case 2:
                switch (loadout.selectedPerk)
                {
                    case 0:
                        perkIcon.sprite = loadout.reconPerks[0].GetComponent<Image>().sprite;
                        break;
                    case 1:
                        perkIcon.sprite = loadout.reconPerks[1].GetComponent<Image>().sprite;
                        break;
                    case 2:
                        perkIcon.sprite = loadout.reconPerks[2].GetComponent<Image>().sprite;
                        break;
                }
                break;
            case 3:
                switch (loadout.selectedPerk)
                {
                    case 0:
                        perkIcon.sprite = loadout.brainsPerks[0].GetComponent<Image>().sprite;
                        break;
                    case 1:
                        perkIcon.sprite = loadout.brainsPerks[1].GetComponent<Image>().sprite;
                        break;
                    case 2:
                        perkIcon.sprite = loadout.brainsPerks[2].GetComponent<Image>().sprite;
                        break;
                }
                break;
            case 4:
                switch (loadout.selectedPerk)
                {
                    case 0:
                        perkIcon.sprite = loadout.yuriPerks[0].GetComponent<Image>().sprite;
                        break;
                    case 1:
                        perkIcon.sprite = loadout.yuriPerks[1].GetComponent<Image>().sprite;
                        break;
                    case 2:
                        perkIcon.sprite = loadout.yuriPerks[2].GetComponent<Image>().sprite;
                        break;
                }
                break;
            case 5:
                switch (loadout.selectedPerk)
                {
                    case 0:
                        perkIcon.sprite = loadout.snailPerks[0].GetComponent<Image>().sprite;
                        break;
                    case 1:
                        perkIcon.sprite = loadout.snailPerks[1].GetComponent<Image>().sprite;
                        break;
                    case 2:
                        perkIcon.sprite = loadout.snailPerks[2].GetComponent<Image>().sprite;
                        break;
                }
                break;
            case 6:
                switch (loadout.selectedPerk)
                {
                    case 0:
                        perkIcon.sprite = loadout.idolPerks[0].GetComponent<Image>().sprite;
                        break;
                    case 1:
                        perkIcon.sprite = loadout.idolPerks[1].GetComponent<Image>().sprite;
                        break;
                    case 2:
                        perkIcon.sprite = loadout.idolPerks[2].GetComponent<Image>().sprite;
                        break;
                }
                break;
            case 7:
                switch (loadout.selectedPerk)
                {
                    case 0:
                        perkIcon.sprite = loadout.miragePerks[0].GetComponent<Image>().sprite;
                        break;
                    case 1:
                        perkIcon.sprite = loadout.miragePerks[1].GetComponent<Image>().sprite;
                        break;
                    case 2:
                        perkIcon.sprite = loadout.miragePerks[2].GetComponent<Image>().sprite;
                        break;
                }
                break;




        }

        //changing energy on floor to red if you dont have enough energy to shoot
        if(robotHead.GetComponent<Robot>().energyCapacity < robotHead.GetComponentInParent<MoveTest3>().Gun.GetComponent<gun>().energyCost)
        {
            floorEnergy.color = new Color32(255, 0, 0, 200);

            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = new Color32(255, 0, 0,255);
            colorBlock.colorMultiplier = 1;
            
            energySlider.colors = colorBlock;
        }
        else
        {
            floorEnergy.color = new Color32(0, 255, 70, 200);
            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = new Color32(71, 255, 0,255);
            colorBlock.colorMultiplier = 1;
            energySlider.colors = colorBlock;

        }
        //setting floor ui values
        floorEnergy.fillAmount = robotHead.GetComponent<Robot>().energyCapacity / robotHead.GetComponent<Robot>().maxEnergyCapacity;
        floorOvercharge.fillAmount = robotHead.GetComponent<Robot>().overchargeCapacity / robotHead.GetComponent<Robot>().maxOverchargeCapacity;
        floorHealth.fillAmount = robotHead.GetComponent<Robot>().health / robotHead.GetComponent<Robot>().maxHealth;

        energySlider.minValue = 0;
        energySlider.maxValue = robotHead.GetComponent<Robot>().maxEnergyCapacity;
        energySlider.wholeNumbers = true;

        overchargeSlider.minValue = 0;
        overchargeSlider.maxValue = 0.2f * robotHead.GetComponent<Robot>().maxEnergyCapacity;
        overchargeSlider.wholeNumbers = true;


        healthSlider.minValue = 0;
        healthSlider.maxValue = robotHead.GetComponent<Robot>().maxHealth;
        healthSlider.wholeNumbers = true;


        playerIndexText.text = "P "+(playerIndex + 1).ToString();

        gun = GetComponentInChildren<MoveTest3>().Gun;


        energySlider.value = robotHead.GetComponent<Robot>().energyCapacity;
        healthSlider.value = robotHead.GetComponent<Robot>().health;
        overchargeSlider.value = robotHead.GetComponent<Robot>().overchargeCapacity;
        uiOutline.color = loadout.glowColor;


        if (playerIndex == 0)
        {
            UI.transform.position = UIspawns[0].transform.position;
        }
        else if (playerIndex == 1)
        {
            UI.transform.position = UIspawns[1].transform.position;
        }
        else if (playerIndex == 2)
        {
            UI.transform.position = UIspawns[2].transform.position;
        }
        else if (playerIndex == 3)
        {
            UI.transform.position = UIspawns[3].transform.position;
        }

    }
    public void onValueChanged(float value)
    {
        Debug.Log(value);
    }
}
