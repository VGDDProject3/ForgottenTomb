using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    #region Editor Variables

    [SerializeField]
    private float grappleRange;

    [SerializeField]
    private float linearSwingAcceleration;

    [SerializeField]
    private float releaseSpeedBoost;

    [SerializeField]
    private float damper;

    [SerializeField]
    private float springiness;

    [SerializeField]
    private float springinessDecayFactor;

    [SerializeField]
    private float minSpringiness, maxSpringiness;

    [SerializeField]
    private float grappleExtendTime, grappleRetractTime;

    [SerializeField]
    private AnimationCurve extendCurve, retractCurve;

    [SerializeField]
    private GameObject fulcrum;

    #endregion

    #region Private Variables

    private LineRenderer grappleRenderer;

    private bool grappling = false;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        grappleRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !grappling)
        {
            Vector2 mousePos = CreateGrappleTargetVector(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //print("Shot grapple at " + mousePos.ToString());
            ShootGrapple(mousePos);
        }
    }

    #endregion

    #region Grapple Functions

    private bool ShootGrapple(Vector2 target)
    {
        Vector2 startPos = this.transform.position;
        print("Firing grapple raycast from " + startPos.ToString() + " to " + target.ToString() + " (distance: " + Vector2.Distance(startPos, target) + ")");
        Debug.DrawLine(startPos, target, Color.red, 2.0f);
        //grappleRenderer.enabled = true;
        //grappleRenderer.SetPositions(new[] { new Vector3(startPos.x, startPos.y, 0), new Vector3(target.x, target.y, 0) });  // setting the LineRenderer positions

        RaycastHit2D hit = Physics2D.Linecast(startPos, target);//, grappleRange);
        if (hit.collider != null)
        {
            print("Hit something at " + hit.point.ToString());
            Debug.DrawLine(startPos, hit.point, Color.green, 2.0f);

            StartCoroutine(ExtendGrapple(hit.point, hit.collider.gameObject));
            return true;
        }
        else
        {
            //StartCoroutine(ExtendGrapple(hit.point));
            return false;
        }
    }

    private IEnumerator ExtendGrapple(Vector2 target, GameObject hitObject = null)
    {
        grappling = true;

        float grappleTimer = grappleExtendTime;
        grappleRenderer.enabled = true;
        while (grappleTimer > 0)
        {
            grappleTimer -= Time.deltaTime;

            Vector2 grappleTipPoint = Vector2.Lerp(transform.position, target, extendCurve.Evaluate(1 - (grappleTimer / grappleExtendTime)));

            grappleRenderer.SetPositions(new Vector3[] { ((Vector3)transform.position), ((Vector3)grappleTipPoint) });

            yield return new WaitForEndOfFrame();
        }
        if (hitObject != null)
        {
            StartCoroutine(ManageGrapple(target, hitObject));
        }
        else
        {
            StartCoroutine(RetractGrapple(target));
        }
        

    }

    private IEnumerator ManageGrapple(Vector2 hitPoint, GameObject hitObject)
    {
        grappling = true;

        GameObject tempFulcrum = Instantiate(fulcrum, hitPoint, Quaternion.identity);
        tempFulcrum.transform.SetParent(hitObject.transform);
        SpringJoint2D tempSpringJoint = this.gameObject.AddComponent<SpringJoint2D>();
        tempSpringJoint.connectedBody = tempFulcrum.GetComponent<Rigidbody2D>();  // and connecting it to the fulcrum 
        tempSpringJoint.dampingRatio = damper;
        tempSpringJoint.frequency = springiness;
        tempSpringJoint.autoConfigureConnectedAnchor = false;
        tempSpringJoint.autoConfigureDistance = false;
        tempSpringJoint.distance = Vector2.Distance(this.transform.position, tempFulcrum.transform.position);

        grappleRenderer.enabled = true;

        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        while (Input.GetButton("Fire2"))
        {
            rb.velocity += new Vector2(rb.velocity.normalized.x, 0) * linearSwingAcceleration; // adds some extra directional acceleration

            tempSpringJoint.anchor = Vector3.zero;
            tempSpringJoint.connectedAnchor = Vector3.zero;
            tempSpringJoint.frequency *= springinessDecayFactor;
            tempSpringJoint.frequency = Mathf.Clamp(tempSpringJoint.frequency, minSpringiness, maxSpringiness);

            grappleRenderer.SetPositions(new[] { this.gameObject.transform.position, tempFulcrum.transform.position });  // setting the LineRenderer positions

            yield return null; 
        }

        // adding some velocity at the end of the grapple
        rb.velocity += rb.velocity.normalized * releaseSpeedBoost;

        Destroy(tempSpringJoint);
        Destroy(tempFulcrum);

        StartCoroutine(RetractGrapple(hitPoint));
    }

    private IEnumerator RetractGrapple(Vector2 origin)
    {
        float retractTimer = grappleRetractTime;
        while (retractTimer > 0)
        {
            retractTimer -= Time.deltaTime;

            Vector2 grappleTipPoint = Vector2.Lerp(transform.position, origin, retractCurve.Evaluate(retractTimer / grappleRetractTime));

            grappleRenderer.SetPositions(new Vector3[] { ((Vector3)transform.position), ((Vector3)grappleTipPoint) });

            yield return new WaitForEndOfFrame();
        }

        grappleRenderer.enabled = false;
        grappling = false;
    }

    private Vector2 CreateGrappleTargetVector(Vector2 mousePos)
    {
        
        Vector2 startPos = this.transform.position;
        return startPos + new Vector2(mousePos.x - startPos.x, mousePos.y - startPos.y).normalized * grappleRange;
        
    }

    #endregion
}
