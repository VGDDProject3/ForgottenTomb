using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronDoor : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private int requiredKeys;

    private Color fadeColor;
    #endregion
    public void OpenDoor() 
    {
        if (playerMovement.Keys >= requiredKeys) {
            playerMovement.Keys -= requiredKeys;
            StartCoroutine(FadeAway(0.5f));
        }

    }

    private IEnumerator FadeAway(float timeInSeconds)
    {
        float startTime = Time.time;
        fadeColor = GetComponent<SpriteRenderer>().color;
        while (Time.time < startTime + timeInSeconds)
        {
            fadeColor.a = (Time.time - startTime) / timeInSeconds;
            GetComponent<SpriteRenderer>().color = fadeColor;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touch door");
            OpenDoor();
        }
     }
}
