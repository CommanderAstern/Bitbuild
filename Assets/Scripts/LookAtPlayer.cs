using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("MainCamera"); // or use the name of your player GameObject
        transform.LookAt(player.transform);
    }
}
