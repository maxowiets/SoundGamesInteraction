using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip idleSound;
    public bool wantsMajorSound = false;
    public AudioClip majorClip;
    public AudioClip minorClip;
    public AudioClip positiveSound;
    public AudioClip negativeSound;
    public AudioClip huhSound;

    public float interactionRange;
    public bool playerIsClose;

    bool playerInteracted = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<SphereCollider>().radius = interactionRange;

        PlaySound(idleSound, true);
    }

    public void PlayMajorSound()
    {
        PlaySound(majorClip, false);
        playerInteracted = true;
    }

    public void PlayMinorSound()
    {
        PlaySound(minorClip, false);
        playerInteracted = true;
    }

    public void PlayPositiveSound()
    {
        PlaySound(positiveSound, false);
        Destroy(transform.parent.gameObject, positiveSound.length);
    }

    public void PlayNegativeSound()
    {
        PlaySound(negativeSound, false);
        Destroy(transform.parent.gameObject, negativeSound.length);
    }

    public void PlayerComesClose()
    {
        PlaySound(huhSound, false);
        playerIsClose = true;
    }

    public void PlayerMovesAway()
    {
        if (!playerInteracted) 
        { 
            PlaySound(idleSound, true);
            playerIsClose = false;
        }
    }

    void PlaySound(AudioClip clip, bool loop)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
