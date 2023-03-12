using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    // a struyct with sprite and image
    public struct NFTData
    {
        public Sprite sprite;
        public string name;
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
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemData = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();
            itemData.text = item.name;
            itemImage.sprite = item.sprite;
        }
    }
}
