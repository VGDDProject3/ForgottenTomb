using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    static int Coins = 1;

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

    public int NumCoins() {
        return Coins;
    }
    public void AddCoin() {
        Coins += 1;
    }
    public void ResetCoins() {
        Coins = 0;
    }
    
    #endregion
}
