using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject positiveText;
    public GameObject negativeText;

    AudioSource audioSource;
    public AudioClip positiveClip;
    public AudioClip negativeClip;

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
        positiveText.SetActive(true);
        negativeText.SetActive(true);
        Debug.Log(npc.gameObject.name + " is interactable");
    }

    void DisableInteraction()
    {
        Debug.Log("Interaction Disabled");
        closeByNPC?.PlayerMovesAway();
        closeByNPC = null;
        positiveText.SetActive(false);
        negativeText.SetActive(false);
    }

    void InteractWithNPC()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Playing Positive Sound");
            closeByNPC.PlayPositiveSound();
            audioSource.Stop();
            audioSource.clip = positiveClip;
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("Playing Negative Sound");
            closeByNPC.PlayNegativeSound();
            audioSource.Stop();
            audioSource.clip = positiveClip;
            audioSource.Play();
        }
    }
}
