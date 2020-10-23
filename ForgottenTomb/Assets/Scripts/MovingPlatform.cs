using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    #region Editor Variables

    [SerializeField]
    private Vector2[] positions;

    [SerializeField]
    private float[] timePerMove;

    [SerializeField]
    private float waitTime;

    [SerializeField]
    [Tooltip("The (optional) animation curves that determine how the platform moves")]
    private AnimationCurve[] curves;

    [SerializeField]
    private bool loop = true;

    #endregion

    #region Runtime Variables

    private int step;

    private float progress;
    private bool waiting = false;

    private Vector2 pointA;
    private Vector2 pointB;

    #endregion

    #region Movement Functions

    private void Start()
    {
        step = 1;
        transform.position = positions[0];
        
        progress = 0;
    }

    private void Update()
    {
        if (waiting)
        {
            return;
        }

        progress = Mathf.Min(1, progress + (1 / timePerMove[step]) * Time.deltaTime);

        //print("moving from position " + ChangeStep(-1) + " to position " + step);

        float currentPos = curves[step].Evaluate(progress);
        pointA = positions[ChangeStep(-1)];
        pointB = positions[step];
        transform.position = Vector2.LerpUnclamped(pointA, pointB, currentPos);

        if (progress >= 1)
        {
            //print("changing current step from " + step + " to " + (step + 1) % (positions.Length - 1));
            waiting = true;
            step = ChangeStep(1);
            progress = 0;

            StartCoroutine(WaitToMove());
            
            
        }

    }

    private int ChangeStep(int change)
    {
        if ((step + change) < 0)
        {
            return positions.Length + change;
        }
        else if ((step + change) > (positions.Length - 1))
        {
            return 0 + change - 1;
        }
        return (step + change);
        
    }

    private IEnumerator WaitToMove()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }

    #endregion

}
