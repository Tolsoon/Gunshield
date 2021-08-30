using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PilotLoreScript : MonoBehaviour
{
    public GameObject[] loreScreens;
    public int selectedScreen;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("InformationBase");
    }

    public void NextLore()
    {
        loreScreens[selectedScreen].SetActive(false);
        
        selectedScreen = (selectedScreen + 1) % loreScreens.Length;

        loreScreens[selectedScreen].SetActive(true);
        
    }

    public void PreviousLore()
    {
        loreScreens[selectedScreen].SetActive(false);
        selectedScreen--;

        if (selectedScreen < 0)
        {
            selectedScreen += loreScreens.Length;
        }

        loreScreens[selectedScreen].SetActive(true);
    }
    
}
