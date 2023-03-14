using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class FetchPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ThirdPersonController>()._mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
