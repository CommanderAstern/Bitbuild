using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("MainCamera"); // or use the name of your player GameObject
        // change y axis rotation by 180 degrees of player transofrm loot at
        transform.LookAt(player.transform);
        transform.Rotate(0, 180, 0);
        
    }
}
