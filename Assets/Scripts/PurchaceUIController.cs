using Solana.Unity.Programs;
using Solana.Unity.Programs.Models;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Messages;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Wallet;
using Solana.Unity.SDK;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class PurchaceUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button quitButton;
    public Button createButton;
    public Button buy1;
    public Button buy2;
    public Button buy3;
    public Button buy4;
    public Button buy5;

    public Button select1;
    public Button select2;
    public Button select3;
    public Button select4;
    public Button select5;
    public Button quit2;

    public VisualElement buyGUI;
    public VisualElement root;
    public string address;

    private static readonly IRpcClient rpcClient = ClientFactory.GetClient(Cluster.DevNet);
    public void Start()
    {
        //get gameObject by tag only one
        // root = GameObject.FindWithTag("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        // root = transform.GetChild(1).gameObject.GetComponent<UIDocument>().rootVisualElement;
        root = GetComponentInChildren<UIDocument>().rootVisualElement;
        quitButton = root.Q<Button>("quit");
        createButton = root.Q<Button>("createAccount");

        buy1 = root.Q<Button>("buy1");
        buy2 = root.Q<Button>("buy2");
        buy3 = root.Q<Button>("buy3");
        buy4 = root.Q<Button>("buy4");
        buy5 = root.Q<Button>("buy5");

        select1 = root.Q<Button>("select1");
        select2 = root.Q<Button>("select2");
        select3 = root.Q<Button>("select3");
        select4 = root.Q<Button>("select4");
        select5 = root.Q<Button>("select5");

        quit2 = root.Q<Button>("quit2");

        buyGUI = root.Q<VisualElement>("buyGUI");
        address = GetComponent<PlayerAccountInit>().playerName;
        quitButton.clicked += QuitButtonPressed;
        createButton.clicked += CreateButtonPressed;

        buy1.clicked += Buy1ButtonPressed;
        buy2.clicked += Buy2ButtonPressed;
        buy3.clicked += Buy3ButtonPressed;
        buy4.clicked += Buy4ButtonPressed;
        buy5.clicked += Buy5ButtonPressed;

        select1.clicked += Select1Pressed;
        select2.clicked += Select2Pressed;
        select3.clicked += Select3Pressed;
        select4.clicked += Select4Pressed;
        select5.clicked += Select5Pressed;

        quit2.clicked += QuitButton2Pressed;

    }
    public void UIStart()
    {
        buyGUI.style.display = DisplayStyle.Flex;
        root.Q<VisualElement>("authorizedContainer").style.display = DisplayStyle.None;
        root.Q<VisualElement>("createContainer").style.display = DisplayStyle.None;
        for (int i = 1; i < 6; i++)
        {
            root.Q<VisualElement>("buy" + i).style.display = DisplayStyle.None;
        }
        getInfo();
    }
    public void HatStart()
    {
        root.Q<VisualElement>("charChangeGUI").style.display = DisplayStyle.Flex;
        getOwned();
    }

    void QuitButtonPressed()
    {
        buyGUI.style.display = DisplayStyle.None;
    }
    void QuitButton2Pressed()
    {
        root.Q<VisualElement>("charChangeGUI").style.display = DisplayStyle.None;
    }

    void Select1Pressed()
    {
      HatController hatobj = GetComponent<HatController>();
      hatobj.PublicVariable = 1;
    }
    void Select2Pressed()
    {
      HatController hatobj = GetComponent<HatController>();
      hatobj.PublicVariable = 2;
    }
    void Select3Pressed()
    {
      HatController hatobj = GetComponent<HatController>();
      hatobj.PublicVariable = 3;
    }
    void Select4Pressed()
    {
      HatController hatobj = GetComponent<HatController>();
      hatobj.PublicVariable = 4;
    }
    void Select5Pressed()
    {
      HatController hatobj = GetComponent<HatController>();
      hatobj.PublicVariable = 5;
    }







    public void getOwned()
    {

        int[] responseVal = {1,0,0,0,1};
        // Iterate the array of UInt64 values
        int i = 1;
        foreach (int value in responseVal)
        {
            if (value == 1)
            {
                root.Q<VisualElement>("select"+i.ToString()).style.display = DisplayStyle.Flex;
            }
            else
            {
                root.Q<VisualElement>("select"+i.ToString()).style.display = DisplayStyle.None;
            }
            i++;
        }
    }
    private void getInfo()
    {
        bool response = true;

        if(response == false)
        {
            root.Q<VisualElement>("createContainer").style.display = DisplayStyle.Flex;
            root.Q<VisualElement>("authorizedContainer").style.display = DisplayStyle.None;
        }
        else
        {
            root.Q<VisualElement>("authorizedContainer").style.display = DisplayStyle.Flex;
            root.Q<VisualElement>("createContainer").style.display = DisplayStyle.None;
            getInitialInventory();
        }
            
    }

    private void CreateButtonPressed()
    {


        root.Q<VisualElement>("createContainer").style.display = DisplayStyle.Flex;
        getInfo();
    }

    private void getInitialInventory()
    {
        int[] responseVal = {1,0,0,0,1};
        // Iterate the array of UInt64 values
        int i = 1;
        foreach (int value in responseVal)
        {
            if (value == 1)
            {
                root.Q<VisualElement>("buy"+i.ToString()).style.display = DisplayStyle.Flex;
            }
            else
            {
                root.Q<VisualElement>("own"+i.ToString()).style.display = DisplayStyle.Flex;
            }
            i++;
        }
    }

    private async void Buy1ButtonPressed()
    {
      Account user = Web3.Instance.Wallet.Account;

      RequestResult<ResponseValue<BlockHash>> blockHash = await rpcClient.GetRecentBlockHashAsync();
      ulong minBalanceForExemptionAcc = (await rpcClient.GetMinimumBalanceForRentExemptionAsync(TokenProgram.TokenAccountDataSize)).Result;
      Debug.Log("buy1");
    }

    private void Buy2ButtonPressed()
    {
      Debug.Log("buy2");
    }
    private void Buy3ButtonPressed()
    {
      Debug.Log("buy3");
    }    
    private void Buy4ButtonPressed()
    {
      Debug.Log("buy4");
    }    
    private void Buy5ButtonPressed()
    {
      Debug.Log("buy5");
    }
}
