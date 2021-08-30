using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CrownRotate : MonoBehaviour
{
    public GameObject crownSpikes;
    public loadoutSelection loadout;

    public Color glowColor;

    

    public GameObject[] glowParts;

    public List<Material> glowMats = new List<Material>();

    

   

    bool gotMats = false;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        crownSpikes.transform.Rotate(new Vector3(0, 0, 25) * Time.deltaTime);       
        
        
        glowColor = loadout.glowColor;



        if (gotMats == false)
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
          
        }
        
    }

}
