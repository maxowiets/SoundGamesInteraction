using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip idleSound;
    public AudioClip positiveSound;
    public AudioClip negativeSound;

    public float interactionRange;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = idleSound;
        GetComponent<SphereCollider>().radius = interactionRange;
    }

    public void PlayPositiveSound()
    {
        audioSource.Stop();
        audioSource.clip = positiveSound;
        audioSource.Play();
    }

    public void PlayNegativeSound()
    {
        audioSource.Stop();
        audioSource.clip = negativeSound;
        audioSource.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
