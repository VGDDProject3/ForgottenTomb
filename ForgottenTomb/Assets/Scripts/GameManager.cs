﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

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
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
