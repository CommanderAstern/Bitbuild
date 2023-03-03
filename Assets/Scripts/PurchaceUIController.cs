using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DapperLabs.Flow.Sdk;
using DapperLabs.Flow.Sdk.DataObjects;
using DapperLabs.Flow.Sdk.Cadence;
using DapperLabs.Flow.Sdk.DevWallet;

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







    public async void getOwned()
    {
        string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f
pub fun main(): [Bool] {
  return BasicInventoryV3.getItemsList(_address: "+address+@");
}
        ";
        FlowScriptRequest scriptReq = new FlowScriptRequest
        {
            Script = script
        };

        FlowScriptResponse response = await Scripts.ExecuteAtLatestBlock(scriptReq);

        // The script returns an array of ids, so convert the return value to an array. 
        CadenceArray responseVal = (CadenceArray)response.Value;

        string resultText = $"Account {address} NFTs:\n";

        // Iterate the array of UInt64 values
        int i = 1;
        foreach (CadenceBase value in responseVal.Value)
        {
            CadenceBool nftId = (CadenceBool)value;
            if (nftId.Value == true)
            {
                root.Q<VisualElement>("select"+i.ToString()).style.display = DisplayStyle.Flex;
            }
            else
            {
                root.Q<VisualElement>("select"+i.ToString()).style.display = DisplayStyle.None;
            }
            i++;
        }
        Debug.Log(resultText);

    }
    private async void getInfo()
    {
        //get playername
        string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f
pub fun main(): Bool {
  return BasicInventoryV3.inDatabase(_address: "+address+@");
}
 ";
        FlowScriptRequest scriptReq = new FlowScriptRequest
        {
            Script = script
        };

        FlowScriptResponse response = await Scripts.ExecuteAtLatestBlock(scriptReq);

        CadenceBool responseStr = (CadenceBool)response.Value;
        if(responseStr.Value == false)
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

    private async void CreateButtonPressed()
    {
            string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f
transaction {
  prepare(acct: AuthAccount) {}
  execute {
    log(BasicInventoryV3.initializeAccount(_address:"+address+@"))
  }
}
";

        // Using the same account as proposer, payer and authorizer. 
        FlowTransactionResponse response = await Transactions.Submit(script);
        root.Q<VisualElement>("createContainer").style.display = DisplayStyle.Flex;
        if (response.Error != null)
        {
            Debug.LogError(response.Error.Message);
            return;
        }
        getInfo();
    }

    private async void getInitialInventory()
    {
        string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f
pub fun main(): [Bool] {
  return BasicInventoryV3.getItemsList(_address: "+address+@");
}
        ";
        FlowScriptRequest scriptReq = new FlowScriptRequest
        {
            Script = script
        };

        FlowScriptResponse response = await Scripts.ExecuteAtLatestBlock(scriptReq);

        // The script returns an array of ids, so convert the return value to an array. 
        CadenceArray responseVal = (CadenceArray)response.Value;

        string resultText = $"Account {address} NFTs:\n";

        // Iterate the array of UInt64 values
        int i = 1;
        foreach (CadenceBase value in responseVal.Value)
        {
            CadenceBool nftId = (CadenceBool)value;
            if (nftId.Value == false)
            {
                root.Q<VisualElement>("buy"+i.ToString()).style.display = DisplayStyle.Flex;
            }
            else
            {
                root.Q<VisualElement>("own"+i.ToString()).style.display = DisplayStyle.Flex;
            }
            i++;
        }
        Debug.Log(resultText);

    }

    private async void Buy1ButtonPressed()
    {
        string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f

transaction {

  prepare(acct: AuthAccount) {}

  execute {
    log(BasicInventoryV3.enableOwnership(_address:"+address+@", _itemID: 0))
  }
}

";

        // Using the same account as proposer, payer and authorizer. 
        FlowTransactionResponse response = await Transactions.Submit(script);
        if (response.Error != null)
        {
            Debug.LogError(response.Error.Message);
            return;
        }
    }

    private async void Buy2ButtonPressed()
    {
        string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f

transaction {

  prepare(acct: AuthAccount) {}

  execute {
    log(BasicInventoryV3.enableOwnership(_address:"+address+@", _itemID: 1))
  }
}

";

        // Using the same account as proposer, payer and authorizer. 
        FlowTransactionResponse response = await Transactions.Submit(script);
        if (response.Error != null)
        {
            Debug.LogError(response.Error.Message);
            return;
        }
    }    private async void Buy3ButtonPressed()
    {
        string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f

transaction {

  prepare(acct: AuthAccount) {}

  execute {
    log(BasicInventoryV3.enableOwnership(_address:"+address+@", _itemID: 2))
  }
}

";

        // Using the same account as proposer, payer and authorizer. 
        FlowTransactionResponse response = await Transactions.Submit(script);
        if (response.Error != null)
        {
            Debug.LogError(response.Error.Message);
            return;
        }
    }    private async void Buy4ButtonPressed()
    {
        string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f

transaction {

  prepare(acct: AuthAccount) {}

  execute {
    log(BasicInventoryV3.enableOwnership(_address:"+address+@", _itemID: 3))
  }
}

";

        // Using the same account as proposer, payer and authorizer. 
        FlowTransactionResponse response = await Transactions.Submit(script);
        if (response.Error != null)
        {
            Debug.LogError(response.Error.Message);
            return;
        }
    }    private async void Buy5ButtonPressed()
    {
        string script = @"
import BasicInventoryV3 from 0x91c7fd71eaa35f2f

transaction {

  prepare(acct: AuthAccount) {}

  execute {
    log(BasicInventoryV3.enableOwnership(_address:"+address+@", _itemID: 4))
  }
}

";

        // Using the same account as proposer, payer and authorizer. 
        FlowTransactionResponse response = await Transactions.Submit(script);
        if (response.Error != null)
        {
            Debug.LogError(response.Error.Message);
            return;
        }
    }
}
