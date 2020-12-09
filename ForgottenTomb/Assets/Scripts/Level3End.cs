using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3End : MonoBehaviour
{  
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject eyes;

    [SerializeField]
    private GameObject camPanner;

    [SerializeField]
    private FadeManager fm;

    private float panTime = 1000;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            camPanner.SetActive(true);
            fm.GetComponent<AudioSource>().mute = true;
            StartCoroutine("End");
        }
    }

    IEnumerator End() {
        yield return new WaitForSeconds(5);
        eyes.SetActive(true);
        yield return new WaitForSeconds(3);
        fm.FadeIn();
    }
}
