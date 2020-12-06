using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    /**
        Useful for objects that destroy themselves (ex: collectibles)
    */
    public static void PlaySound (AudioClip clip, Vector3 position, float volume, float pitch)
     {
        GameObject soundPlayer = new GameObject();
        soundPlayer.transform.position = position;
        soundPlayer.AddComponent<AudioSource>();
        
        AudioSource soundPlayerAudioSource = soundPlayer.GetComponent<AudioSource>();
        soundPlayerAudioSource.pitch = pitch;
        soundPlayerAudioSource.PlayOneShot(clip, volume);
        Destroy (soundPlayer, clip.length / pitch);
     }
}
