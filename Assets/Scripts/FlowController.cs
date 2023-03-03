using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DapperLabs.Flow.Sdk;
using DapperLabs.Flow.Sdk.DataObjects;
using DapperLabs.Flow.Sdk.Cadence;
using DapperLabs.Flow.Sdk.Unity;
using DapperLabs.Flow.Sdk.DevWallet;
using UnityEngine.SceneManagement;

public class FlowController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        FlowConfig flowConfig = new FlowConfig();
        flowConfig.NetworkUrl = "https://rest-testnet.onflow.org/v1";

        flowConfig.Protocol = FlowConfig.NetworkProtocol.HTTP;
        FlowSDK.Init(flowConfig);
        FlowSDK.RegisterWalletProvider(ScriptableObject.CreateInstance<DevWalletProvider>());

    }

    public void SignInClicked()
    {
        FlowSDK.GetWalletProvider().Authenticate("", (string authAccount) =>
        {
            Debug.Log($"Authenticated: {authAccount}");
            SceneManager.LoadScene(2);
        }, () =>
        {
            Debug.Log("Authentication failed, aborting transaction.");
        });
    }
}
