using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class colorPicker : MonoBehaviour
{
    
    public Light glow;
    public float red;
    public float blue;
    public float green;

    public Slider greenSlider;
    public Slider redSlider;
    public Slider blueSlider;

    public GameObject[] glowParts;

    public List<Material> glowMats = new List<Material>();
    void Start()
    {
        foreach(GameObject part in glowParts)
        {
            foreach (Material matt in part.GetComponent<MeshRenderer>().materials)
            {
                
                if (matt.name == "Glow (Instance)")
                {
                    
                    glowMats.Add(matt);
                }
            }
        }     
                
    }

    // Update is called once per frame
    void Update()
    {
        
        
        glow.color = new Color(red, green, blue);

        foreach (Material matt in glowMats)
        {
            matt.color = new Color(red, green, blue);
        }
    }

    public void setGreen()
    {
        green = greenSlider.value;
    }

    public void setBlue()
    {
        blue = blueSlider.value;
    }

    public void setRed()
    {
        red = redSlider.value;
    }
}
