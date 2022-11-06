using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip idleSound;
    public AudioClip majorClip;
    public AudioClip minorClip;
    public AudioClip positiveSound;
    public AudioClip negativeSound;
    public AudioClip huhSound;

    public float interactionRange;

    bool playerInteracted = false;

    GlobularMovement globularMovement;

    private void Awake()
    {
        globularMovement = FindObjectOfType<GlobularMovement>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = idleSound;
        audioSource.loop = true;
        audioSource.Play();
        GetComponent<SphereCollider>().radius = interactionRange;
    }

    public void PlayMajorSound()
    {
        PlaySound(majorClip, false);

        globularMovement.enabled = false;

        playerInteracted = true;

        Destroy(GetComponent<SphereCollider>());

        Invoke("PlayPositiveSound", majorClip.length + 1);
    }

    public void PlayMinorSound()
    {
        PlaySound(minorClip, false);

        globularMovement.enabled = false;

        playerInteracted = true;

        Destroy(GetComponent<SphereCollider>());

        Invoke("PlayNegativeSound", minorClip.length);
    }

    void PlayPositiveSound()
    {
        PlaySound(positiveSound, false);
        globularMovement.enabled = true;
    }

    void PlayNegativeSound()
    {
        PlaySound(negativeSound, false);
        globularMovement.enabled = true;
    }

    public void PlayerComesClose()
    {
        PlaySound(huhSound, false);
    }

    public void PlayerMovesAway()
    {
        if (!playerInteracted) PlaySound(idleSound, true);
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
