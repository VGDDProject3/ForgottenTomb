using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;
    static GameManager gameManager;

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

    void Start() {
        if (gameManager == null ) {
            gameManager = (GameManager) Object.FindObjectOfType(typeof(GameManager));
        }
    }
    public void ObtainCoin() 
    {
        SoundPlayer.PlaySound(pickupSound, transform.position, pickupSoundVolume, pickupSoundPitch);
        playerMovement.Coins = playerMovement.Coins + 1;
        gameManager.AddCoin();
    }

    void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {
            ObtainCoin();
            if (collectionEffect != null)
            {
                Instantiate(collectionEffect, this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
     }
}
