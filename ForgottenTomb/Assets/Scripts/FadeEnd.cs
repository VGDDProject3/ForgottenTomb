using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEnd : MonoBehaviour
{
    [SerializeField]
    private FadeManager fadeManager;

    [SerializeField]
    private int level = 1;

    public void OnFadeEnd() {
        AudioSource audio = fadeManager.GetComponent<AudioSource>();
        audio.mute = !audio.mute;
        if (level == 1) {
            fadeManager.StartGame();
        } else {
            fadeManager.LevelTwo();
        }
    }
}
