using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour, Resettable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<CheckpointManager>().AddToThingsToReset(this);
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void Reset()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
