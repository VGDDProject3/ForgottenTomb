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

    [SerializeField]
    private bool autoFade = false;

    void Start() {
        if (autoFade) {
            StartCoroutine("AutoFade");
        }
    }

    IEnumerator AutoFade() {
        yield return new WaitForSeconds(6);
        FadeIn();
    }

    public void FadeIn() {
        Debug.Log("Makes fade active");
        fade.gameObject.SetActive(!fade.gameObject.activeSelf);
        fadeAnim.SetTrigger("fadeStart");
    }

    #region SceneManager because gameManager fails me
    // Load first level
    public void StartGame()
    {
        SceneManager.LoadScene("Celine-Lvl1");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level3Intro()
    {
        SceneManager.LoadScene("Win2.5");
    }

    public void LevelThree()
    {
        Debug.Log("Loads last level");
        //SceneManager.LoadScene("INSERT NAME OF THIRD LEVEL");
    }
    #endregion
}
