using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class waitForQ : NetworkBehaviour
{
    void Update()
    {
        // check if Q is pressed
        if (Input.GetKeyDown(KeyCode.Q) && isLocalPlayer)
        {
            // if active
            if (transform.GetChild(2).GetChild(2).gameObject.activeSelf)
            {
                // disable
                transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                // enable cursor
            }
            // if not active
            else
            {
                // enable
                transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                // disable cursor
                Cursor.lockState = CursorLockMode.None;

            }
        }

        // check if I is pressed
        if (Input.GetKeyDown(KeyCode.Semicolon) && isLocalPlayer)
        {
            // if active
            if (transform.GetChild(2).GetChild(3).gameObject.activeSelf)
            {
                // disable
                transform.GetChild(2).GetChild(3).gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                // enable cursor
            }
            // if not active
            else
            {
                // enable
                transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
                // disable cursor
                Cursor.lockState = CursorLockMode.None;

            }
        }
        if (Input.GetKeyDown(KeyCode.Quote) && isLocalPlayer)
        {
            // if active
            if (transform.GetChild(2).GetChild(5).gameObject.activeSelf)
            {
                // disable
                transform.GetChild(2).GetChild(5).gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                // enable cursor
            }
            // if not active
            else
            {
                // enable
                transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
                // disable cursor
                Cursor.lockState = CursorLockMode.None;

            }
        }
    }
}
