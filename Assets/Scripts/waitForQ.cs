using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class waitForQ : MonoBehaviour
{
    void Update()
    {
        // check if Q is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // if active
            if (transform.GetChild(2).GetChild(2).gameObject.activeSelf)
            {
                // disable
                transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
            }
            // if not active
            else
            {
                // enable
                transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
            }
        }
    }
}
