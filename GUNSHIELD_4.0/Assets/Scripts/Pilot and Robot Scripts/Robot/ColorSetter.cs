using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{    
    public Color glowColor;

    public Light glowLight;

    public GameObject[] glowParts;

    public List<Material> glowMats = new List<Material>();

    public GameObject shield;

    public loadoutSelection loadout;

    bool gotMats = false;
    
    void Start()
    {
        
    }

    public void Update()
    {
        glowColor = loadout.glowColor;

        

        if(gotMats == false)
        {
            foreach (GameObject part in glowParts)
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
        
        
            foreach (Material matt in glowMats)
            {

                matt.color = glowColor;
                matt.SetColor("_EmissionColor", glowColor);
                glowLight.color = glowColor;
                shield.GetComponent<MeshRenderer>().material.color = glowColor;
                shield.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", glowColor);
            }
        
        

       
    }

    
}
