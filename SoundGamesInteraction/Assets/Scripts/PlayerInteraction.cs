using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip majorClip;
    public AudioClip minorClip;
    public AudioClip positiveClip;
    public AudioClip negativeClip;

    public AudioClip InteractionSound;

    NPC closeByNPC = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DisableInteraction();
    }

    private void Update()
    {
        if (closeByNPC != null)
        {
            InteractWithNPC();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NPC hitNPC = other.GetComponent<NPC>();
        if (hitNPC != null)
        {
            EnableInteraction(hitNPC);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DisableInteraction();
    }

    void EnableInteraction(NPC npc)
    {
        closeByNPC = npc;
        npc.PlayerComesClose();
    }

    void DisableInteraction()
    {
        closeByNPC?.PlayerMovesAway();
        closeByNPC = null;
    }

    void InteractWithNPC()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            closeByNPC.PlayMajorSound();
            audioSource.Stop();
            audioSource.PlayOneShot(InteractionSound);
            //audioSource.clip = majorClip;
            //audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            closeByNPC.PlayMinorSound();
            audioSource.Stop();
            audioSource.PlayOneShot(InteractionSound);
            //audioSource.clip = minorClip;
            //audioSource.Play();
        }
    }
}
