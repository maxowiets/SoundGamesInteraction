using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    GlobularMovement globularMovement;
    AudioSource audioSource;
    public AudioClip majorClip;
    public AudioClip minorClip;
    public AudioClip positiveClip;
    public AudioClip negativeClip;

    NPC closeByNPC = null;

    public bool isInteracting = false;

    private void Awake()
    {
        globularMovement = FindObjectOfType<GlobularMovement>();
        audioSource = GetComponent<AudioSource>();
        DisableInteraction();
    }

    private void Update()
    {
        if (closeByNPC != null && !isInteracting)
        {
            InteractWithNPC();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NPC>())
        {
            NPC hitNPC = other.GetComponent<NPC>();
            if (hitNPC != null)
            {
                EnableInteraction(hitNPC);
            }
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
            StartCoroutine(PlayMajor());
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            StartCoroutine(PlayMinor());
        }
    }

    IEnumerator PlayMajor()
    {
        globularMovement.enabled = false;
        isInteracting = true;
        audioSource.clip = majorClip;
        audioSource.Play();
        yield return new WaitForSeconds(majorClip.length);

        if (closeByNPC.wantsMajorSound)
        {
            closeByNPC.PlayMajorSound();
            yield return new WaitForSeconds(closeByNPC.majorClip.length + 1f);

            closeByNPC.PlayPositiveSound();
            yield return new WaitForSeconds(closeByNPC.positiveSound.length);
        }
        else
        {
            closeByNPC.PlayNegativeSound();
            yield return new WaitForSeconds(closeByNPC.negativeSound.length);
        }

        isInteracting = false;
        globularMovement.enabled = true;
    }

    IEnumerator PlayMinor()
    {
        globularMovement.enabled = false;
        isInteracting = true;
        audioSource.clip = minorClip;
        audioSource.Play();
        yield return new WaitForSeconds(minorClip.length);

        if (!closeByNPC.wantsMajorSound)
        {
            closeByNPC.PlayMinorSound();
            yield return new WaitForSeconds(closeByNPC.minorClip.length + 1f);

            closeByNPC.PlayPositiveSound();
            yield return new WaitForSeconds(closeByNPC.positiveSound.length);
        }
        else
        {
            closeByNPC.PlayNegativeSound();
            yield return new WaitForSeconds(closeByNPC.negativeSound.length);
        }

        isInteracting = false;
        globularMovement.enabled = true;
    }
}
