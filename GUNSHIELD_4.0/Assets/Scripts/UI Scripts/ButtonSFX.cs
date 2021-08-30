using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioClip audioSelect;
    public AudioClip audioMove;

    public AudioSource audio;

    public void playSelect()
    {
        audio.clip = audioSelect;
        audio.Play();
    }

    public void playMove()
    {
        audio.clip = audioMove;
        audio.Play();
    }
}
