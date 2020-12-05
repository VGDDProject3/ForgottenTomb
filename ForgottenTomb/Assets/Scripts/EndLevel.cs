using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private int level = 1;

    #region level 2 to 3 variables
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    private PlayerMovement player;
    #endregion

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (level == 1) {
                gameManager.WinGame();
            } else {
                player = (PlayerMovement) Object.FindObjectOfType(typeof(PlayerMovement));
                player.CanMove = false;
                StartCoroutine("LoadLevel2");
            }
            
        }
    }

    IEnumerator LoadLevel2() {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 2;
        yield return new WaitForSeconds(3);
        player.CanMove = true;
        gameManager.WinGame2();
    }
}
