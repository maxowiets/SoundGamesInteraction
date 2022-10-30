using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip idleSound;
    public AudioClip positiveSound;
    public AudioClip negativeSound;
    public AudioClip huhSound;

    public float interactionRange;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = idleSound;
        audioSource.loop = true;
        GetComponent<SphereCollider>().radius = interactionRange;
    }

    public void PlayPositiveSound()
    {
        audioSource.Stop();
        audioSource.clip = positiveSound;
        audioSource.loop = false;
        audioSource.Play();

        GetComponent<SphereCollider>().enabled = false;
        this.enabled = false;
    }

    public void PlayNegativeSound()
    {
        audioSource.Stop();
        audioSource.clip = negativeSound;
        audioSource.loop = false;
        audioSource.Play();

        GetComponent<SphereCollider>().enabled = false;
        this.enabled = false;
    }

    public void PlayerComesClose()
    {
        audioSource.Stop();
        audioSource.clip = huhSound;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayerMovesAway()
    {
        audioSource.Stop();
        audioSource.clip = idleSound;
        audioSource.loop = true;
        audioSource.Play();
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
