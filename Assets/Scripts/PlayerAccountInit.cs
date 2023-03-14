using UnityEngine;
using TMPro;
using Mirror;
using Solana.Unity.SDK;
using System.Collections;
using UnityEngine.Networking;

public class PlayerAccountInit : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName = "";
    [SyncVar]
    public string playerBio = "";


    [SyncVar]
    string authorizedAccount;

    [SerializeField]
    private TMP_Text nameText;

    [System.Serializable]
    public struct MyJsonObject
    {
        public string name;
        public string bio;
    }
    public override void OnStartLocalPlayer()
    {
        playerName = Web3.Instance.Wallet.Account.PublicKey.ToString();
        Debug.Log(Web3.Instance.Wallet.Account.PublicKey.ToString());
        Debug.Log(Web3.Instance.Wallet.Account.PrivateKey.ToString());
        nameText.text = playerName;
        // Call the CmdSetPlayerName function to set the name on the server
        CmdSetPlayerName(playerName);
        CmdSetPlayerAddress("authorizedAccount");
        startFetchName();
        gameObject.GetComponent<SerializeUnityWebTest>().fetchNFTofAddress(playerName);
    }

    public void startFetchName()
    {
        StartCoroutine(GetRequest("http://3.110.83.239:3000/getPlayerInfo?publicKey="+Web3.Instance.Wallet.Account.PublicKey.ToString()));
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

    [Command]
    public void CmdSetPlayerBio(string bio)
    {
        // Set the player's name on the server
        playerBio = bio;
    }
    private void OnNameChanged(string oldValue, string newValue)
    {
        // Update the name text on all clients
        nameText.text = newValue;
    }

    IEnumerator GetRequest(string uri) 
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) 
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError) 
                {
                    Debug.Log(webRequest.error);
                } 
                else 
                {
                    // Show results as text
                    string jsonString = webRequest.downloadHandler.text;
                    string checkError = jsonString.Substring(2,5);
                    Debug.Log("checkError: " + checkError);
                    if (checkError == "error")
                    {
                        Debug.Log("error");
                    }
                    else
                    {
                        MyJsonObject myObject = JsonUtility.FromJson<MyJsonObject>(jsonString);
                        Debug.Log("id: " + myObject.name);
                        CmdSetPlayerName(myObject.name);
                        CmdSetPlayerBio(myObject.bio);
                    }
                    
                }
        }
    }
}
