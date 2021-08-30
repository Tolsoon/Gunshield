using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InformationScreen : MonoBehaviour
{
    public void PilotLore()
    {
        SceneManager.LoadScene("PilotLore");
    }

    public void GunShow()
    {
        SceneManager.LoadScene("WeaponShow");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void ControlsScreen()
    {
        SceneManager.LoadScene("ControlsScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
