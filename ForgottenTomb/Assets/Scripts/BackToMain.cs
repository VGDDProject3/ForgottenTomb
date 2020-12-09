using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMain : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    void Start()
    {
        StartCoroutine("Back");
    }

    IEnumerator Back() {
        yield return new WaitForSeconds(30);
        gameManager.MainMenu();
    }
}
