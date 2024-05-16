using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public Transform playerPos;
    public void TeleportToSpawnPoint()
    {
        Debug.Log("respawning");
        Debug.Log(transform.position);

        playerPos.transform.position = transform.position;
        
    }
}
