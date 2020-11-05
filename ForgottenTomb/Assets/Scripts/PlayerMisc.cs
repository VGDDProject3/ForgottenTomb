using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMisc : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private CheckpointManager checkpointManager;
    #endregion

    #region Public Methods
    public void Die()
    {
        checkpointManager.Respawn(gameObject);
    }
    #endregion
}
