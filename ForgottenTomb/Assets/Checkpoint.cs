using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private CheckpointManager checkpointManager;

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            checkpointManager.ActiveCheckpoint = this;
        }
    }
}
