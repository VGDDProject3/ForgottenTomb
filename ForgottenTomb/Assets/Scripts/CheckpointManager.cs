using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region Private Variables
    private Checkpoint activeCheckpoint;
    public Checkpoint ActiveCheckpoint { get => activeCheckpoint; set => activeCheckpoint = value; }
    #endregion

    public void Respawn(GameObject player)
    {
        player.SetActive(false);
        Collider2D checkpointCollider = ActiveCheckpoint.GetComponent<Collider2D>();
        player.transform.position = checkpointCollider.transform.position - new Vector3(0, checkpointCollider.bounds.extents.y, 0);

        player.SetActive(true);

    }
}
