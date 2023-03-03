using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HatController : NetworkBehaviour
{
    public GameObject[] hats; // An array of hat objects to choose from
    [SyncVar(hook = "OnPublicVariableChanged")]
    public int PublicVariable = 0; // The current index of the hat to use
    [SyncVar(hook = "OnCurrentHatIndexChanged")]
    int currentHatIndex = 0; // The current index of the hat to use

    void Start()
    {
        // Deactivate all hats except the current one
        for (int i = 0; i < hats.Length; i++)
        {
            hats[i].SetActive(i == currentHatIndex);
        }
    }

    void OnCurrentHatIndexChanged(int oldValue, int newValue)
    {
        // Deactivate the old hat and activate the new one
        for (int i = 0; i < hats.Length; i++)
        {
            hats[i].SetActive(i == newValue);
        }
        currentHatIndex = newValue;
    }

    void OnPublicVariableChanged(int oldValue, int newValue)
    {
        // Update the current hat index on all clients
        currentHatIndex = newValue;
    }

    [Client]
    void Update()
    {
        // Check if the current hat index has changed
        if (currentHatIndex != PublicVariable)
        {
            // Update the public variable on the server
            CmdChangePublicVariable(currentHatIndex);
        }
    }

    [Command]
    void CmdChangePublicVariable(int newValue)
    {
        // Update the public variable on the server
        PublicVariable = newValue;
    }
}
