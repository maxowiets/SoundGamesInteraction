using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class GameplayLoop : MonoBehaviour
{
    public List<NewNPC> npcList = new List<NewNPC>();
    public Player player;

    void Start()
    {
        StartCoroutine(Loop());
    }

    void Update()
    {
        
    }

    IEnumerator Loop()
    {
        for (int i = 0; i < npcList.Count; i++)
        {
            //MOVE PLAYER TOWARDS NEXT NPC
            player.StartWalking();
            while (player.transform.position != npcList[i].transform.position)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, npcList[i].transform.position, player.moveSpeed * Time.deltaTime);
                yield return 0;
            }

            //NPC PLAYS HUH SOUND AND PLAYER CAN INTERACT
            player.ReachedNPC(npcList[i]);
            yield return new WaitForSeconds(1f);

            //WAIT FOR PLAYER TO INTERACT AND FOR THE INTERACTION TO BE FINISHED
            while (!npcList[i].completedInteraction)
            {
                yield return 0;
            }
            yield return 0;
        }
    }
}
