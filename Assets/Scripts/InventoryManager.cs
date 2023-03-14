using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
public class InventoryManager : MonoBehaviour
{
    // a struyct with sprite and image
    public struct NFTData
    {
        public Sprite sprite;
        public string name;
        public int id;
    }
    public static InventoryManager Instance;
    public List<NFTData> items = new List<NFTData>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void Add(NFTData item)
    {
        items.Add(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in items)
        {
            if (item.sprite == null)
            {
                continue;
            }
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemData = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
            // add an onclick event to the item
            obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                int category = item.id / 5;
                costumeController CostumeController = gameObject.GetComponent<costumeController>();
                if (category == 0)
                {
                    CostumeController.SetHatActive(item.id%5+1,true);
                    gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;
                    // gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(1).SetActive(true);
                    Debug.Log("Clicked on Hat");
                }
                else if (category == 1)
                {
                    CostumeController.SetLegActive(item.id%5+1,true);
                    gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;
                    // gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(1).GetChild(1).SetActive(true);
                    Debug.Log("Clicked on Leg");
                }
                else if (category == 2)
                {
                    CostumeController.SetHipsActive(item.id%5+1,true);
                    gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(2).GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;
                    // gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(2).GetChild(1).SetActive(true);
                    Debug.Log("Clicked on Hips");
                }
                else if (category == 3)
                {
                    CostumeController.SetTorsoActive(item.id%5+1,true);
                    gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(3).GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;
                    // gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(3).GetChild(1).SetActive(true);
                    Debug.Log("Clicked on Torso");
                }
                else if (category == 4)
                {
                    CostumeController.SetWeaponActive(item.id%5+1,true);
                    gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(4).GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;
                    // gameObject.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(1).GetChild(4).GetChild(1).SetActive(true);
                    Debug.Log("Clicked on Weapon");
                }
                Debug.Log("Clicked on " + item.id);
            });
            itemData.text = item.name;
            itemImage.sprite = item.sprite;
        }
    }
}
