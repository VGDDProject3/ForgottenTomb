using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fade;

    [SerializeField]
    private Animator fadeAnim;

    public void FadeIn() {
        Debug.Log("Makes fade active");
        fade.gameObject.SetActive(!fade.gameObject.activeSelf);
        fadeAnim.SetTrigger("fadeStart");
    }

    // Load first level
    public void StartGame()
    {
        SceneManager.LoadScene("Celine-Lvl1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }
}
