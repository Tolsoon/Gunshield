using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadoutSelection : MonoBehaviour
{
    
    GameManager gameManager;

    public int PlayerIndex;
    public Text playerIndexText;
    public bool spawned = false;


    [SerializeField]
    public Color glowColor;
    public float red;
    public float green;
    public float blue ;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    [SerializeField]
    public GameObject[] pilots;
    public GameObject[] robotHeads;
    public int selectedPilot = 0;

    [SerializeField]
    public GameObject[] guns;    
    public int selectedGun = 0;

    [SerializeField]
    public GameObject[] forgePerks;
    public GameObject[] queenPerks;
    public GameObject[] reconPerks;
    public GameObject[] brainsPerks;
    public GameObject[] yuriPerks;
    public GameObject[] snailPerks;
    public GameObject[] idolPerks;
    public GameObject[] miragePerks;
    public int selectedPerk = 0;

    public bool inGame;
    public bool matchSetUp = false;

    public bool ready;
    public Text readyText;

    public Sprite[] pilotPortraits;

    public GameObject[] uiScreens;
    bool onPilot = true;

    //buttons movements to swpa in loadout screen
    public GameObject perkLeft;
    public GameObject blueSlide;
    public GameObject swap;
    public GameObject readyButton;
    public GameObject readyTextGO;


    private void Awake()
    {     

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        playerIndexText.text = "P " + (PlayerIndex + 1).ToString();

    }


    public void NextCharacter()
    {
        pilots[selectedPilot].SetActive(false);
        robotHeads[selectedPilot].SetActive(false);

        selectedPilot = (selectedPilot + 1) % pilots.Length;
        
        pilots[selectedPilot].SetActive(true);
        robotHeads[selectedPilot].SetActive(true);


    }

    public void PreviousCharacter()
    {
        pilots[selectedPilot].SetActive(false);
        robotHeads[selectedPilot].SetActive(false);
        selectedPilot--;
        if (selectedPilot < 0)
        {
            selectedPilot += pilots.Length;
        }
        pilots[selectedPilot].SetActive(true);
        robotHeads[selectedPilot].SetActive(true);

    }

    public void NextGun()
    {
        guns[selectedGun].SetActive(false);
        selectedGun = (selectedGun + 1) % guns.Length;
        guns[selectedGun].SetActive(true);
    }

    public void PreviousGun()
    {
        guns[selectedGun].SetActive(false);
        selectedGun--;
        if (selectedGun < 0)
        {
            selectedGun += guns.Length;
        }
        guns[selectedGun].SetActive(true);
    }

    public void NextPerk()
    {
        forgePerks[selectedPerk].SetActive(false);
        queenPerks[selectedPerk].SetActive(false);
        reconPerks[selectedPerk].SetActive(false);
        brainsPerks[selectedPerk].SetActive(false);
        yuriPerks[selectedPerk].SetActive(false);
        snailPerks[selectedPerk].SetActive(false);
        idolPerks[selectedPerk].SetActive(false);
        miragePerks[selectedPerk].SetActive(false);

        selectedPerk = (selectedPerk + 1) % 3;

        forgePerks[selectedPerk].SetActive(true);
        queenPerks[selectedPerk].SetActive(true);
        reconPerks[selectedPerk].SetActive(true);
        brainsPerks[selectedPerk].SetActive(true);
        yuriPerks[selectedPerk].SetActive(true);
        snailPerks[selectedPerk].SetActive(true);
        idolPerks[selectedPerk].SetActive(true);
        miragePerks[selectedPerk].SetActive(true);

    }

    public void PreviousPerk()
    {
        forgePerks[selectedPerk].SetActive(false);
        queenPerks[selectedPerk].SetActive(false);
        reconPerks[selectedPerk].SetActive(false);
        brainsPerks[selectedPerk].SetActive(false);
        yuriPerks[selectedPerk].SetActive(false);
        snailPerks[selectedPerk].SetActive(false);
        idolPerks[selectedPerk].SetActive(false);
        miragePerks[selectedPerk].SetActive(false);

        selectedPerk--;
        if (selectedPerk < 0)
        {
            selectedPerk += 3;
        }

        forgePerks[selectedPerk].SetActive(true);
        queenPerks[selectedPerk].SetActive(true);
        reconPerks[selectedPerk].SetActive(true);
        brainsPerks[selectedPerk].SetActive(true);
        yuriPerks[selectedPerk].SetActive(true);
        snailPerks[selectedPerk].SetActive(true);
        idolPerks[selectedPerk].SetActive(true);
        miragePerks[selectedPerk].SetActive(true);
    }

    public void setRed()
    {
        red = redSlider.value;
        glowColor = new Color(red, green, blue);
    }

    public void setGreen()
    {
        green = greenSlider.value;
        glowColor = new Color(red, green, blue);
    }
    public void setBlue()
    {
        blue = blueSlider.value;
        glowColor = new Color(red, green, blue);
    }

    public void readyUp()
    {
        if (!ready)
        {
            ready = true;
            readyText.text = "READY!";
            readyTextGO.SetActive(true);
            gameManager.playersReady += 1;
        }
        else
        {
            ready = false;
            readyText.text = "READY?";
            readyTextGO.SetActive(false);
            gameManager.playersReady -= 1;
        }
    }

    public void swapScreens()
    {
        if (onPilot)
        {
            Navigation NewNav = new Navigation();
            NewNav.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNav.selectOnUp = blueSlide.GetComponent<Slider>();
            NewNav.selectOnDown = readyButton.GetComponent<Button>();

            //Assign the new navigation to your desired button or ui Object
            swap.GetComponent<Button>().navigation = NewNav;


            uiScreens[0].SetActive(false);
            uiScreens[1].SetActive(true);            
            onPilot = false;
        }
        else if(!onPilot)
        {
            Navigation NewNav = new Navigation();
            NewNav.mode = Navigation.Mode.Explicit;

            //Set what you want to be selected on down, up, left or right;
            NewNav.selectOnUp = perkLeft.GetComponent<Button>();
            NewNav.selectOnDown = readyButton.GetComponent<Button>();


            //Assign the new navigation to your desired button or ui Object
            swap.GetComponent<Button>().navigation = NewNav;

            uiScreens[0].SetActive(true);
            uiScreens[1].SetActive(false);
            onPilot = true;
        }
    }
}
