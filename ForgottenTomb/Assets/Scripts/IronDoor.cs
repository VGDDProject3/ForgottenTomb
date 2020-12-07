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

    [SerializeField]
    private AudioClip openSound;

    [SerializeField]
    [Range(0.0f, 1f)]
    private float openSoundVolume;

    [SerializeField]
    [Range(0.0f, 3f)]
    private float openSoundPitch;
    #endregion

    #region Private Variables
    private Color fadeColor;
    #endregion
    public void OpenDoor() 
    {
        if (playerMovement.Keys >= requiredKeys) {
            SoundPlayer.PlaySound(openSound, transform.position, openSoundVolume, openSoundPitch);
            playerMovement.Keys -= requiredKeys;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(FadeAway(0.3f));
        }

    }

    private IEnumerator FadeAway(float timeInSeconds)
    {
        float startTime = Time.time;
        fadeColor = GetComponent<SpriteRenderer>().color;
        while (Time.time < startTime + timeInSeconds)
        {
            fadeColor.a = 1 - ((Time.time - startTime) / timeInSeconds);
            print(1 - ((Time.time - startTime) / timeInSeconds));
            GetComponent<SpriteRenderer>().color = fadeColor;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<SpriteRenderer>().enabled = false;
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
