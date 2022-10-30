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

    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = idleSound;
        audioSource.loop = true;
        audioSource.Play();
        GetComponent<SphereCollider>().radius = interactionRange;
    }

    public void PlayMajorSound()
    {
        Debug.Log("PlayerMajorS9ound");
        PlaySound(majorClip, false);

        playerMovement.enabled = false;

        playerInteracted = true;
        GetComponent<SphereCollider>().radius = 0;
        GetComponent<SphereCollider>().isTrigger = false;

        Invoke("PlayPositiveSound", 18);
    }

    public void PlayMinorSound()
    {
        PlaySound(minorClip, false);

        playerMovement.enabled = false;

        playerInteracted = true;
        GetComponent<SphereCollider>().radius = 0;
        GetComponent<SphereCollider>().isTrigger = false;

        Invoke("PlayNegativeSound", 22);
    }

    void PlayPositiveSound()
    {
        PlaySound(positiveSound, false);
        playerMovement.enabled = true;
    }

    void PlayNegativeSound()
    {
        PlaySound(negativeSound, false);
        playerMovement.enabled = true;
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
