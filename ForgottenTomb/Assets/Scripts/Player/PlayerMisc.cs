using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMisc : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private CheckpointManager checkpointManager;

    [SerializeField]
    private PlayerSoundSources playerSoundSources;
    #endregion

    #region Public Methods
    public void Die()
    {
        checkpointManager.Respawn(gameObject);
        playerSoundSources.PlayDeathSound();
    }
    #endregion
}
