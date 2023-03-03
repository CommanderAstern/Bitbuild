using UnityEngine;
using TMPro;
using Mirror;
using DapperLabs.Flow.Sdk;

public class PlayerAccountInit : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar]
    SdkAccount authorizedAccount;

    [SerializeField]
    private TMP_Text nameText;

    public override void OnStartLocalPlayer()
    {
        authorizedAccount = FlowSDK.GetWalletProvider().GetAuthenticatedAccount();
        playerName = authorizedAccount.Address;
        nameText.text = playerName;
        // Call the CmdSetPlayerName function to set the name on the server
        CmdSetPlayerName(playerName);
        CmdSetPlayerAddress(authorizedAccount);
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        // Set the player's name on the server
        playerName = name;
    }

    [Command]
    public void CmdSetPlayerAddress(SdkAccount sdkAccount)
    {
        // Set the player's name on the server
        authorizedAccount = sdkAccount;
    }
    private void OnNameChanged(string oldValue, string newValue)
    {
        // Update the name text on all clients
        nameText.text = newValue;
    }
}
