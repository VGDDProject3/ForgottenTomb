using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonFX : MonoBehaviour
{
    [SerializeField]
    private AudioSource fx;

    [SerializeField]
    private AudioClip hoverFx;

    [SerializeField]
    private AudioClip clickFx;

    public void HoverSound() 
    {
        fx.PlayOneShot(hoverFx);
    }

    public void ClickSound() 
    {
        Debug.Log("Plays click sound");
        fx.PlayOneShot(clickFx);
    }
}
