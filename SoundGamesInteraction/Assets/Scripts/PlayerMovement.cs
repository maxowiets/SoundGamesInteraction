using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip walkingSound;
    public float rotationSpeed;
    public float moveSpeed;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walkingSound;
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) transform.position += transform.right * -moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) transform.position += transform.forward * -moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime));
        if (Input.GetKey(KeyCode.E)) transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime));
    }
}
