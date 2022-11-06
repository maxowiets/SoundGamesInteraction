using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;

    NewNPC currentInteractableNPC;

    AudioSource audioSource;
    public AudioClip walkingSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walkingSound;
        audioSource.loop = true;
    }

    void Update()
    {
        if (currentInteractableNPC != null)
        {
            if (audioSource.isPlaying) audioSource.Stop();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentInteractableNPC?.PlayMajorDuet();
                currentInteractableNPC = null;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                currentInteractableNPC?.PlayMinorDuet();
                currentInteractableNPC = null;
            }
        }
    }

    public void ReachedNPC(NewNPC npc)
    {
        currentInteractableNPC = npc;
        currentInteractableNPC.PlayerGotClose();
    }

    public void StartWalking()
    {
        if (!audioSource.isPlaying) audioSource.Play();
    }
}
