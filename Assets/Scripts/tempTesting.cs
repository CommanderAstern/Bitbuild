using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DapperLabs.Flow.Sdk;
using DapperLabs.Flow.Sdk.DevWallet;
using TMPro;
public class tempTesting : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] public bool isTesting;
    void Start()
    {
        // Initialize FlowSDK. You must pass in a FlowConfig object which specifies which protocol
        // and Url to connect to (only HTTP is currently supported). 
        // By default, connects to the emulator on localhost. 

        // FlowConfig flowConfig = new FlowConfig();

        // // flowConfig.NetworkUrl = FlowControl.Data.EmulatorSettings.emulatorEndpoint ?? "http://localhost:8888/v1";             // local emulator
        // flowConfig.NetworkUrl = "https://rest-testnet.onflow.org/v1";  // testnet
        // //flowConfig.NetworkUrl = "https://rest-mainnet.onflow.org/v1";  // mainnet

        // flowConfig.Protocol = FlowConfig.NetworkProtocol.HTTP;
        // FlowSDK.Init(flowConfig);
        // FlowSDK.RegisterWalletProvider(ScriptableObject.CreateInstance<DevWalletProvider>());
        if(isTesting)
        {
        SdkAccount authorizedAccount = FlowSDK.GetWalletProvider().GetAuthenticatedAccount();
        Debug.Log(authorizedAccount.Address);
        textMeshPro.text = authorizedAccount.Address;
        }
        else
        {
            textMeshPro.text = "0x0";
        }
    }
    void Update()
    {

    }
}
