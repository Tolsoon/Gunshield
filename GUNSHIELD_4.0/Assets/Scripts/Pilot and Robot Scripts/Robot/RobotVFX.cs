using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RobotVFX : MonoBehaviour
{
    public GameObject SpawnVFX;
    public Component[] spawnParticles;
    public loadoutSelection loadout;

    public GameObject invincibleEffect;

    public Gradient grad;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    void Start()
    {
        spawnParticles = SpawnVFX.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        foreach(ParticleSystem ps in spawnParticles)
        {
            ps.startColor = loadout.glowColor;
        }

        

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = loadout.glowColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = loadout.glowColor;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        grad.SetKeys(colorKey, alphaKey);

        invincibleEffect.GetComponent<VisualEffect>().SetGradient("grad1" , grad);

        if (GetComponent<MoveTest3>().invincible)
        {
            invincibleEffect.SetActive(true);

        }
        else
        {
            invincibleEffect.SetActive(false);
        }

    }
}
