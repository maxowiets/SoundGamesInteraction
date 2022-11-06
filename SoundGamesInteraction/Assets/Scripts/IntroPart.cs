using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPart : MonoBehaviour
{
    public AudioSource narratorSource;
    public AudioSource ambientSource;
    public GameObject xyloNPC;
    GameObject currentXyloNPC;
    public GlobularMovement globularMovement;
    public PlayerInteraction playerInteraction;

    bool playerDidGood = false;

    public List<AudioClip> narratorClips = new List<AudioClip>();
    int currentNarratorClip = 0;

    public AudioClip ambientSoundClip;

    private void Awake()
    {
        globularMovement.enabled = false;
        playerInteraction.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(IntroScene());
    }

    IEnumerator IntroScene()
    {
        //START INTRO SCENE
        narratorSource.clip = narratorClips[currentNarratorClip];                           //1
        narratorSource.Play();
        yield return new WaitForSeconds(narratorClips[currentNarratorClip].length);        

        //START AMBIENT SOUND
        ambientSource.clip = ambientSoundClip;
        ambientSource.Play();
        narratorSource.clip = narratorClips[currentNarratorClip++];                         //2
        narratorSource.Play();
        yield return new WaitForSeconds(narratorClips[currentNarratorClip].length);

        //SPAWN XYLOPHONE NPC
        currentXyloNPC = Instantiate(xyloNPC);
        Debug.Log(currentXyloNPC);
        yield return new WaitForSeconds(3f);

        narratorSource.clip = narratorSource.clip = narratorClips[currentNarratorClip++];   //3
        narratorSource.Play();
        yield return new WaitForSeconds(narratorClips[currentNarratorClip].length + 4f);

        narratorSource.clip = narratorSource.clip = narratorClips[currentNarratorClip++];   //4
        narratorSource.Play();
        yield return new WaitForSeconds(narratorClips[currentNarratorClip].length);

        //ENABLE PLAYER MOVEMENT
        globularMovement.enabled = true;

        while (!playerDidGood)
        {
            //SPAWN NEW NPC IF OTHER WAS WRONG
            if (currentXyloNPC == null)
            {
                currentXyloNPC = Instantiate(xyloNPC);
                Debug.Log(currentXyloNPC);

            }
            //WAIT FOR PLAYER TO GET CLOSE
            while (!currentXyloNPC.GetComponentInChildren<NPC>().playerIsClose)
            {
                Debug.Log("test");
                yield return 0;
            }
            globularMovement.enabled = false;
            yield return new WaitForSeconds(1f);

            //NPC NOTICED YOU
            narratorSource.clip = narratorSource.clip = narratorClips[currentNarratorClip++];//5
            narratorSource.Play();
            yield return new WaitForSeconds(narratorClips[currentNarratorClip].length);

            //ENABLE PLAYER INTERACTION WITH NPC
            playerInteraction.enabled = true;
            while (!playerInteraction.isInteracting)
            {
                yield return 0;
            }
            while (playerInteraction.isInteracting)
            {
                if (!playerDidGood)
                {
                    if (playerInteraction.GetComponent<AudioSource>().clip == playerInteraction.majorClip)
                    {
                        playerDidGood = true;
                    }
                }
                yield return 0;
            }
            globularMovement.enabled = false;
            //PLAYER MADE THE WRONG CHOICE
            if (!playerDidGood)
            {
                narratorSource.clip = narratorSource.clip = narratorClips[currentNarratorClip++];   //6
                narratorSource.Play();
                currentNarratorClip -= 2;
                yield return new WaitForSeconds(narratorClips[currentNarratorClip].length);
            }
        }

        //PLAYER MADE THE RIGHT CHOICE
        narratorSource.clip = narratorSource.clip = narratorClips[currentNarratorClip++];           //7
        narratorSource.Play();
        yield return new WaitForSeconds(narratorClips[currentNarratorClip].length);

        //LOAD IN NEW PART OF GAME
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
