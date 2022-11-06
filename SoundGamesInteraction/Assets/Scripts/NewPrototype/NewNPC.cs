using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNPC : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip mainLoopSound;
    public AudioClip huhSound;
    public AudioClip majorDuetSound;
    public AudioClip minorDuetSound;

    public bool wantsMajorSound;
    bool playedRightSound;

    public AudioClip happySound;
    public AudioClip angrySound;

    public bool completedInteraction = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNewSound(mainLoopSound, true);
    }


    public void PlayerGotClose()
    {
        PlayNewSound(huhSound, false);
    }

    public void PlayMajorDuet()
    {
        PlayNewSound(majorDuetSound, false);
        playedRightSound = wantsMajorSound;
        Invoke("HappyOrAngrySound", majorDuetSound.length + 1);
    }

    public void PlayMinorDuet()
    {
        PlayNewSound(minorDuetSound, false);
        playedRightSound = !wantsMajorSound;
        Invoke("HappyOrAngrySound", minorDuetSound.length + 1);
    }

    void HappyOrAngrySound()
    {
        if (playedRightSound)
        {
            PlayNewSound(happySound, false);
            Invoke("InteractionCompleted", happySound.length);
        }
        else
        {
            PlayNewSound(angrySound, false);
            Invoke("InteractionCompleted", angrySound.length);
        }
    }

    void InteractionCompleted()
    {
        completedInteraction = true;
    }

    void PlayNewSound(AudioClip clip, bool loop)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
