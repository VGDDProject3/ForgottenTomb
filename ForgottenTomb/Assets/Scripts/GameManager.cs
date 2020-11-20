using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private GameObject fade;

    [SerializeField]
    private Animator fadeAnim;

    #region Unity_functions
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transitions
    public void StartGame()
    {
        SceneManager.LoadScene("Celine-Lvl1");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Win");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleMenu");
    }
    #endregion

    #region Other_functions
    public void ExitGame() {
        Application.Quit();
        Debug.Log("The game will close in build");
    }

    public void FadeIn() {
        Debug.Log("Makes fade active");
        fade.gameObject.SetActive(!fade.gameObject.activeSelf);
        fadeAnim.SetTrigger("fadeStart");
    }
    #endregion
}
