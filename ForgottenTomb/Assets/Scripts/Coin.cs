﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;
    static GameManager gameManager;
    #endregion

    void Start() {
        if (gameManager == null ) {
            gameManager = (GameManager) Object.FindObjectOfType(typeof(GameManager));
        }
    }
    public void ObtainCoin() 
    {
        playerMovement.Coins = playerMovement.Coins + 1;
        gameManager.AddCoin();

    }

    void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touch coin");
            ObtainCoin();
            Destroy(this.gameObject);
        }
     }
}
