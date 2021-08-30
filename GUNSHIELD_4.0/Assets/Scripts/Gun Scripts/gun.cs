using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    GameManager gameManager;
    public loadoutSelection loadout;
    //gun stats
    public float fireRate;       
    public float energyCost;
    public float damage;
    public float bounce;
    public float bulletSpeed;

    public string gunType;
    public string gunName;

    //bullets
    public GameObject bullet;
    public GameObject bullet0;
    public GameObject[] bullets;

    public GameObject gunModel;

    //glowing mats of gun
    public List<Material> glowMats = new List<Material>();

    public int playerIndex;

    bool matchSetUp = false;
    bool gotMats = false;
    private void Start()
    {
        //find and set game manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerIndex = loadout.PlayerIndex;
        
    }

    private void Update()
    {
        if(loadout.inGame && matchSetUp == false)
        {
            setMatchUp();
        }

        if (loadout.inGame)
        {
            bullet.GetComponent<bulletMove>().whoShot = gameManager.players[playerIndex];
        }

        if(loadout.inGame == false && gotMats == false)
        {
            //set gun glow
            foreach (Material matt in gunModel.GetComponent<MeshRenderer>().materials)
            {
                if (matt.name == "Glow (Instance)")
                {
                    glowMats.Add(matt);
                }
            }

            gotMats = true;
            
        }

        foreach (Material matt in glowMats)
        {

            matt.color = gameManager.players[playerIndex].GetComponentInChildren<loadoutSelection>().glowColor;
            matt.SetColor("_EmissionColor", gameManager.players[playerIndex].GetComponentInChildren<loadoutSelection>().glowColor);
        }

        

    }

    public void setMatchUp()
    {

        //set bullet

        bullet0 = Instantiate(bullets[0], new Vector3(-1000, -1000, -1000), Quaternion.identity);
        bullet = bullet0;
        GetComponent<MeshRenderer>().material.color = gameManager.players[playerIndex].GetComponentInChildren<loadoutSelection>().glowColor;
        bullet.GetComponent<TrailRenderer>().material.color = gameManager.players[playerIndex].GetComponentInChildren<loadoutSelection>().glowColor;
        bullet.GetComponent<TrailRenderer>().material.SetColor("_EmissionColor", gameManager.players[playerIndex].GetComponentInChildren<loadoutSelection>().glowColor);

        bullet.GetComponent<bulletMove>().whoShot = gameObject.transform.parent.gameObject;
        bullet.GetComponent<bulletMove>().loadout = loadout;
        bullet0.GetComponent<bulletMove>().loadout = loadout;

        bullet.GetComponent<bulletMove>().setGunUp();
        bullet0.GetComponent<bulletMove>().setGunUp();

        matchSetUp = true;
    }

}
