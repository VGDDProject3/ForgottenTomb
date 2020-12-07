using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region Private Variables
    private Checkpoint activeCheckpoint;
    public Checkpoint ActiveCheckpoint { get => activeCheckpoint; set => activeCheckpoint = value; }

    private List<Resettable> thingsToReset = new List<Resettable>();
    #endregion

    //private void Start()
    //{
    //    thingsToReset 
    //}

    public void Respawn(GameObject player)
    {
        //player.SetActive(false);
        player.GetComponent<PlayerMovement>().Respawn();
        Collider2D checkpointCollider = ActiveCheckpoint.GetComponent<Collider2D>();
        player.transform.position = checkpointCollider.transform.position - new Vector3(0, checkpointCollider.bounds.extents.y, 0);

        foreach( Resettable thing in thingsToReset)
        {
            thing.Reset();
        }
        
        //player.SetActive(true);

    }

    public void AddToThingsToReset(Resettable thing)
    {
        thingsToReset.Add(thing);
    }
}
