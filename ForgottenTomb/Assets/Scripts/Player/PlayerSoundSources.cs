﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundSources : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private List<AudioClip> walkSounds;

    [SerializeField]
    private float walkPitchVariation;

    [SerializeField]
    private List<AudioClip> jumpSounds;

    [SerializeField]
    private List<AudioClip> grappleClinkSounds;
    #endregion


    #region Private Variables
    private AudioSource walkAudioSource;

    private AudioSource jumpAudioSource;

    private AudioSource airJumpAudioSource;

    private AudioSource wallJumpAudioSource;

    private AudioSource grappleClinkAudioSource;

    private AudioSource dashAudioSource;

    private AudioSource deathAudioSource;

    private float walkOriginalPitch;
    private float airJumpOriginalPitch;
    #endregion

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        walkAudioSource = audioSources[0];
        jumpAudioSource = audioSources[1];
        airJumpAudioSource = audioSources[2];
        wallJumpAudioSource = audioSources[3];
        grappleClinkAudioSource = audioSources[4];
        dashAudioSource = audioSources[5];
        deathAudioSource = audioSources[6];

        walkOriginalPitch = walkAudioSource.pitch;
        airJumpOriginalPitch = airJumpAudioSource.pitch;
    }

    public void PlayWalkSound()
    {
        walkAudioSource.clip = RandomClip(walkSounds);
        walkAudioSource.pitch = Random.Range(walkOriginalPitch - walkPitchVariation, walkOriginalPitch + walkPitchVariation);
        walkAudioSource.Play();
    }

    public void PlayJumpSound()
    {
        jumpAudioSource.clip = RandomClip(jumpSounds);
        jumpAudioSource.Play();
    }


    public void PlayAirJumpSound(float pitchAddition)
    {
        airJumpAudioSource.clip = RandomClip(jumpSounds);
        airJumpAudioSource.pitch = airJumpOriginalPitch + pitchAddition;
        airJumpAudioSource.Play();
        SetPitch(airJumpAudioSource, airJumpOriginalPitch, airJumpAudioSource.clip.length /  airJumpAudioSource.pitch);
    }

    public void PlayWallJumpSound()
    {
        wallJumpAudioSource.clip = RandomClip(jumpSounds);
        wallJumpAudioSource.Play();
    }

    public void PlayGrappleClinkSound()
    {
        grappleClinkAudioSource.clip = RandomClip(grappleClinkSounds);
        grappleClinkAudioSource.Play();
    }

    public void PlayDashSound(float pitchAddition)
    {
        
    }

    public void PlayDeathSound()
    {
        
    }

    private IEnumerator SetPitch(AudioSource audioSource, float pitch, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        audioSource.pitch = pitch;
    }

    private AudioClip RandomClip(List<AudioClip> clips)
    {
        return clips[Random.Range(0, clips.Count)];
    }

}
