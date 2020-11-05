using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private GameObject player;
    #endregion

    #region Private Variables
    private Checkpoint activeCheckpoint;
    public Checkpoint ActiveCheckpoint { get => activeCheckpoint; set => activeCheckpoint = value; }
    #endregion

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.SetActive(false);
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        Collider2D checkpointCollider = ActiveCheckpoint.GetComponent<Collider2D>();
        player.transform.position = checkpointCollider.transform.position - new Vector3(0, checkpointCollider.bounds.extents.y, 0);

        player.SetActive(true);

    }
}
