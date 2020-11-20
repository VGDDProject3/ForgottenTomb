using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEnd : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public void OnFadeEnd() {
        AudioSource audio = gameManager.GetComponent<AudioSource>();
        audio.mute = !audio.mute;
        gameManager.StartGame();
    }
}
