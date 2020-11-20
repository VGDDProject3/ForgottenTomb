using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    #region Unity_functions
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        }
    }
    #endregion
}
