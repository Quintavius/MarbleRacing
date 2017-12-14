using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AISoundManager : MonoBehaviour {
    public AudioMixer playerMixer;
    MarbleBrain marble;

    AudioSource rollSound;
    AudioSource startSound;
    AudioSource dropSound;
    AudioSource clickSound;

    public AudioClip[] dropSounds;

    //Variables
    bool stopped;

    void Start()
    {
        marble = GetComponent<MarbleBrain>();

        AudioSource[] audios = GetComponents<AudioSource>();
        rollSound = audios[0];
        startSound = audios[1];
        dropSound = audios[2];
        clickSound = audios[3];
    }

    void Update()
    {
        var rollVolumeMod = marble.mag / (marble.maximumSpeed * 1.5f);
        //Rolling SFX
        if (marble.grounded)
        {
            rollSound.volume = rollVolumeMod;
            rollSound.pitch = Mathf.Lerp(0, 1f, rollVolumeMod);
        }
        else
        {
            rollSound.volume = 0;
        }

        //Starting SFX
        startSound.volume = Mathf.Clamp01(rollVolumeMod + 0.2f);
        startSound.pitch = Mathf.Lerp(0, 1f, rollVolumeMod);
        if (marble.mag == 0)
        {
            stopped = true;
        }
        else
        {
            if (stopped)
            {
                startSound.Play();
                stopped = false;
            }
        }
    }

    public void DropSound(float impactStrength)
    {
        var rollVolumeMod = marble.mag / (marble.maximumSpeed * 1.5f);
        dropSound.volume = impactStrength;
        dropSound.pitch = Random.Range(0.8f, 1.2f);
        int dropArraySize = dropSounds.Length;
        AudioClip chosenDropSound = dropSounds[Random.Range(0, dropArraySize)];
        dropSound.PlayOneShot(chosenDropSound);
    }

    public void MarbleSound(float impactStrength)
    {
        clickSound.volume = impactStrength;
        clickSound.pitch = Random.Range(0.5f, 1.5f);
        clickSound.Play();
    }

    public void WallSound(float impactStrength)
    {
        var rollVolumeMod = marble.mag / (marble.maximumSpeed * 1.5f);
        dropSound.volume = impactStrength;
        dropSound.pitch = Random.Range(0.8f, 1.2f);
        int dropArraySize = dropSounds.Length;
        AudioClip chosenDropSound = dropSounds[Random.Range(0, dropArraySize)];
        dropSound.PlayOneShot(chosenDropSound);
    }
}
