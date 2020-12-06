using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private GameObject collectionEffect;

    [SerializeField]
    private AudioClip pickupSound;

    [SerializeField]
    [Range(0.0f, 1f)]
    private float pickupSoundVolume;

    [SerializeField]
    [Range(0.0f, 3f)]
    private float pickupSoundPitch;
    #endregion
    public void ObtainKey() 
    {
        SoundPlayer.PlaySound(pickupSound, transform.position, pickupSoundVolume, pickupSoundPitch);
        playerMovement.Keys = playerMovement.Keys + 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ObtainKey();
            if (collectionEffect != null)
            {
                Instantiate(collectionEffect, this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
