using UnityEngine;
using TMPro;
using Mirror;
using Solana.Unity.SDK;
public class PlayerAccountInit : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar]
    string authorizedAccount;

    [SerializeField]
    private TMP_Text nameText;

    public override void OnStartLocalPlayer()
    {
        playerName = Web3.Instance.Wallet.Account.PublicKey.ToString();
        Debug.Log(Web3.Instance.Wallet.Account.PublicKey.ToString());
        Debug.Log(Web3.Instance.Wallet.Account.PrivateKey.ToString());
        nameText.text = playerName;
        // Call the CmdSetPlayerName function to set the name on the server
        CmdSetPlayerName(playerName);
        CmdSetPlayerAddress("authorizedAccount");
        gameObject.GetComponent<SerializeUnityWebTest>().fetchNFTofAddress(playerName);
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        // Set the player's name on the server
        playerName = name;
    }

    [Command]
    public void CmdSetPlayerAddress(string sdkAccount)
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
