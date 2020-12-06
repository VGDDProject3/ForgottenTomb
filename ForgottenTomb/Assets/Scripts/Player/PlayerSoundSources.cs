using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundSources : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private List<AudioClip> walkSounds;

    [SerializeField]
    private List<AudioClip> jumpSounds;
    #endregion


    #region Private Variables
    private AudioSource walkAudioSource;

    private AudioSource jumpAudioSource;

    private AudioSource airJumpAudioSource;

    private float airJumpOriginalPitch;
    #endregion

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        walkAudioSource = audioSources[0];
        jumpAudioSource = audioSources[1];
        airJumpAudioSource = audioSources[2];

        airJumpOriginalPitch = airJumpAudioSource.pitch;
    }

    public void PlayWalkSound()
    {

    }

    public void PlayJumpSound()
    {
        jumpAudioSource.clip = jumpSounds[Random.Range(0, jumpSounds.Count)];
        jumpAudioSource.Play();
    }


    public void PlayAirJumpSound(int pitchAddition)
    {
        airJumpAudioSource.clip = jumpSounds[Random.Range(0, jumpSounds.Count)];
        airJumpAudioSource.pitch = airJumpOriginalPitch + pitchAddition;
        airJumpAudioSource.Play();
        SetPitch(airJumpAudioSource, airJumpOriginalPitch, airJumpAudioSource.clip.length /  airJumpAudioSource.pitch);
    }

    IEnumerator SetPitch(AudioSource audioSource, float pitch, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        audioSource.pitch = pitch;
    }

}
