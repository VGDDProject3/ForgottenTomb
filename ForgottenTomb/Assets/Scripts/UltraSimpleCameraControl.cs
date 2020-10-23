using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraSimpleCameraControl : MonoBehaviour
{
    private GameObject playerGO;

    private Camera camera;

    [SerializeField]
    private float cameraOffset;


    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        camera = GetComponent<Camera>();
        camera.orthographicSize = cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(playerGO.transform.position.x, playerGO.transform.position.y, this.transform.position.z);
        
    }
}
