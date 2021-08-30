using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour
{
    public GameObject PointsScreen;
    public int playerIndex;
    public loadoutSelection loadout;
    public Text playerIndexText;

    public Image pilotPortrait;
    public Image background;
    
    
    private void Awake()
    {
        playerIndex = loadout.PlayerIndex + 1;
        playerIndexText.text = "Player " + playerIndex.ToString();
        PointsScreen = GameObject.FindGameObjectWithTag("Points Screen");
        gameObject.transform.SetParent(PointsScreen.transform);
        PointsScreen.GetComponent<PointsList>().points.Add(gameObject);
        transform.localScale = new Vector3(.75f, .75f, .75f);
        gameObject.SetActive(false);
    }

    public void Update()
    {
        pilotPortrait.sprite = loadout.pilotPortraits[loadout.selectedPilot];
        
        background.color = loadout.glowColor;
    }


}
