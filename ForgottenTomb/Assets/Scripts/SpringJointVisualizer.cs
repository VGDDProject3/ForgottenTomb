using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJointVisualizer : MonoBehaviour
{

    private LineRenderer renderer;
    private SpringJoint2D springJoint;
    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        springJoint = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.SetPositions(new Vector3[] { transform.position, springJoint.connectedBody.transform.position});
    }
}
