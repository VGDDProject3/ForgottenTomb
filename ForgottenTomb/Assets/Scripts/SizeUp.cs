using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUp : MonoBehaviour
{
    [SerializeField]
    private AudioSource fx;

    [SerializeField]
    private AudioClip sizeUpFx;

    public void SizeUpSound() 
    {
        fx.PlayOneShot(sizeUpFx);
    }
}
