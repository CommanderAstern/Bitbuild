using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class costumeController : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHatActiveIDChanged))]
    public int hatActiveID = -1;
    [SyncVar(hook = nameof(OnTorsoActiveIDChanged))]
    public int torsoActiveID = -1;
    [SyncVar(hook = nameof(OnHipsActiveIDChanged))]
    public int hipsActiveID = -1;
    [SyncVar(hook = nameof(OnLegActiveIDChanged))]
    public int legActiveID = -1;
    [SyncVar(hook = nameof(OnWeaponActiveIDChanged))]
    public int weaponActiveID = -1;
    public GameObject[] hats;
    public GameObject[] torsos;
    public GameObject[] hips;
    public GameObject[] legs;
    public GameObject[] weapons;


    public override void OnStartServer()
    {
        base.OnStartServer();

        // Set hatActiveID based on server value
        RpcSetHatActiveID(hatActiveID);
        RpcSetTorsoActiveID(torsoActiveID);
        RpcSetHipsActiveID(hipsActiveID);
        RpcSetLegActiveID(legActiveID);
        RpcSetWeaponActiveID(weaponActiveID);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        // Set hatActiveID based on server value
        RpcSetHatActiveID(hatActiveID);
        RpcSetTorsoActiveID(torsoActiveID);
        RpcSetHipsActiveID(hipsActiveID);
        RpcSetLegActiveID(legActiveID);
        RpcSetWeaponActiveID(weaponActiveID);


        CmdSetHatActive(0, true);
        CmdSetTorsoActive(0, true);
        CmdSetHipsActive(0, true);
        CmdSetLegActive(0, true);
        CmdSetWeaponActive(0, true);
        // Update hats for all players on the server
        CmdUpdateHats();
        CmdUpdateTorsos();
        CmdUpdateHips();
        CmdUpdateLegs();
        CmdUpdateWeapons();
    }
    [Command]
    void CmdUpdateHats()
    {
        // Get all players on the server
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Update hats for all players
        foreach (GameObject player in players)
        {
            costumeController controller = player.GetComponent<costumeController>();
            if (controller != null)
            {
                controller.RpcSetHatActiveID(controller.hatActiveID);
            }
        }
    }

    [Command]
    void CmdUpdateTorsos()
    {
        // Get all players on the server
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Update hats for all players
        foreach (GameObject player in players)
        {
            costumeController controller = player.GetComponent<costumeController>();
            if (controller != null)
            {
                controller.RpcSetTorsoActiveID(controller.torsoActiveID);
            }
        }
    }

    [Command]
    void CmdUpdateHips()
    {
        // Get all players on the server
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Update hats for all players
        foreach (GameObject player in players)
        {
            costumeController controller = player.GetComponent<costumeController>();
            if (controller != null)
            {
                controller.RpcSetHipsActiveID(controller.hipsActiveID);
            }
        }
    }

    [Command]
    void CmdUpdateLegs()
    {
        // Get all players on the server
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Update hats for all players
        foreach (GameObject player in players)
        {
            costumeController controller = player.GetComponent<costumeController>();
            if (controller != null)
            {
                controller.RpcSetLegActiveID(controller.legActiveID);
            }
        }
    }

    [Command]
    void CmdUpdateWeapons()
    {
        // Get all players on the server
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Update hats for all players
        foreach (GameObject player in players)
        {
            costumeController controller = player.GetComponent<costumeController>();
            if (controller != null)
            {
                controller.RpcSetWeaponActiveID(controller.weaponActiveID);
            }
        }
    }

    void OnHatActiveIDChanged(int oldID, int newID)
    {
        for (int i = 0; i < hats.Length; i++)
        {
            hats[i].SetActive(i == newID);
        }
    }

    // // Other player script code...

    public void SetHatActive(int hatIndex, bool active)
    {
        if (isLocalPlayer)
        {
            CmdSetHatActive(hatIndex, active);
        }
    }

    [Command]
    void CmdSetHatActive(int hatIndex, bool active)
    {
        if (active)
        {
            hatActiveID = hatIndex;
        }
        else if (hatActiveID == hatIndex)
        {
            hatActiveID = -1;
        }
    }

    void OnTorsoActiveIDChanged(int oldID, int newID)
    {
        for (int i = 0; i < torsos.Length; i++)
        {
            torsos[i].SetActive(i == newID);
        }
    }

    public void SetTorsoActive(int torsoIndex, bool active)
    {
        if (isLocalPlayer)
        {
            CmdSetTorsoActive(torsoIndex, active);
        }
    }

    [Command]
    void CmdSetTorsoActive(int torsoIndex, bool active)
    {
        if (active)
        {
            torsoActiveID = torsoIndex;
        }
        else if (torsoActiveID == torsoIndex)
        {
            torsoActiveID = -1;
        }
    }

    void OnHipsActiveIDChanged(int oldID, int newID)
    {
        for (int i = 0; i < hips.Length; i++)
        {
            hips[i].SetActive(i == newID);
        }
    }

    public void SetHipsActive(int hipsIndex, bool active)
    {
        if (isLocalPlayer)
        {
            CmdSetHipsActive(hipsIndex, active);
        }
    }

    [Command]
    void CmdSetHipsActive(int hipsIndex, bool active)
    {
        if (active)
        {
            hipsActiveID = hipsIndex;
        }
        else if (hipsActiveID == hipsIndex)
        {
            hipsActiveID = -1;
        }
    }

    void OnLegActiveIDChanged(int oldID, int newID)
    {
        for (int i = 0; i < legs.Length; i++)
        {
            legs[i].SetActive(i == newID);
        }
    }

    public void SetLegActive(int legIndex, bool active)
    {
        if (isLocalPlayer)
        {
            CmdSetLegActive(legIndex, active);
        }
    }

    [Command]
    void CmdSetLegActive(int legIndex, bool active)
    {
        if (active)
        {
            legActiveID = legIndex;
        }
        else if (legActiveID == legIndex)
        {
            legActiveID = -1;
        }
    }

    void OnWeaponActiveIDChanged(int oldID, int newID)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == newID);
        }
    }

    public void SetWeaponActive(int weaponIndex, bool active)
    {
        if (isLocalPlayer)
        {
            CmdSetWeaponActive(weaponIndex, active);
        }
    }

    [Command]
    void CmdSetWeaponActive(int weaponIndex, bool active)
    {
        if (active)
        {
            weaponActiveID = weaponIndex;
        }
        else if (weaponActiveID == weaponIndex)
        {
            weaponActiveID = -1;
        }
    }

    void Start()
    {
        // Get all children 
        GameObject hatObject  = gameObject.transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).gameObject;
        GameObject torsoObject = gameObject.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(2).gameObject;
        GameObject hipObject = gameObject.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(6).gameObject;
        GameObject legObject = gameObject.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(7).gameObject;
        GameObject weaponObject = gameObject.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(8).gameObject;

        hats = new GameObject[hatObject.transform.childCount];
        torsos = new GameObject[torsoObject.transform.childCount];
        hips = new GameObject[hipObject.transform.childCount];
        legs = new GameObject[legObject.transform.childCount];
        weapons = new GameObject[weaponObject.transform.childCount];

        for (int i = 0; i < hatObject.transform.childCount; i++)
        {
            hats[i] = hatObject.transform.GetChild(i).gameObject;
            Debug.Log(hats[i].name);
        }

        for (int i = 0; i < torsoObject.transform.childCount; i++)
        {
            torsos[i] = torsoObject.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < hipObject.transform.childCount; i++)
        {
            hips[i] = hipObject.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < legObject.transform.childCount; i++)
        {
            legs[i] = legObject.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < weaponObject.transform.childCount; i++)
        {
            weapons[i] = weaponObject.transform.GetChild(i).gameObject;
        }
        // Set hatActiveID based on server value
        if (isServer)
        {
            RpcSetHatActiveID(hatActiveID);
            RpcSetTorsoActiveID(torsoActiveID);
            RpcSetHipsActiveID(hipsActiveID);
            RpcSetLegActiveID(legActiveID);
            RpcSetWeaponActiveID(weaponActiveID);
        }
    }

    [ClientRpc]
    void RpcSetHatActiveID(int activeID)
    {
        hatActiveID = activeID;
        OnHatActiveIDChanged(-1, activeID);
    }

    [ClientRpc]
    void RpcSetTorsoActiveID(int activeID)
    {
        torsoActiveID = activeID;
        OnTorsoActiveIDChanged(-1, activeID);
    }

    [ClientRpc]
    void RpcSetHipsActiveID(int activeID)
    {
        hipsActiveID = activeID;
        OnHipsActiveIDChanged(-1, activeID);
    }

    [ClientRpc]
    void RpcSetLegActiveID(int activeID)
    {
        legActiveID = activeID;
        OnLegActiveIDChanged(-1, activeID);
    }

    [ClientRpc]
    void RpcSetWeaponActiveID(int activeID)
    {
        weaponActiveID = activeID;
        OnWeaponActiveIDChanged(-1, activeID);
    }


    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.Z))
        {
            int newHatActiveID = (hatActiveID + 1) % hats.Length;
            int newTorsoActiveID = (torsoActiveID + 1) % torsos.Length;
            int newHipsActiveID = (hipsActiveID + 1) % hips.Length;
            int newLegActiveID = (legActiveID + 1) % legs.Length;
            int newWeaponActiveID = (weaponActiveID + 1) % weapons.Length;
            CmdSetHatActive(newHatActiveID, true);
            CmdSetTorsoActive(newHatActiveID, true);
            CmdSetHipsActive(newHatActiveID, true);
            CmdSetLegActive(newHatActiveID, true);
            CmdSetWeaponActive(newHatActiveID, true);

        }
    }

}
