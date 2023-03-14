using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class SerializeUnityWebTest : NetworkBehaviour
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    [System.Serializable]
    public class BitbuildNFT
    {
        public string name;
        public string symbol;
        public string description;
        public string image;
        public List<BitbuildNFTAttribute> attributes;
        public BitbuildNFTProperties properties;
    }

    [System.Serializable]
    public class BitbuildNFTAttribute
    {
        public string trait_type;
        public string value;
    }

    [System.Serializable]
    public class BitbuildNFTProperties
    {
        public List<BitbuildNFTFile> files;
        public string category;
    }

    [System.Serializable]
    public class BitbuildNFTFile
    {
        public string uri;
        public string type;
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
    public void fetchNFTofAddress(string address)
    {
        if(isLocalPlayer)
        {
            StartCoroutine(GetRequest("http://3.108.191.161:3000/getOwnedNFT?ownerAddress="+address));
        }
    }
    // Update is called once per frame
    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
 
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(webRequest.error);
            } else {
                // Show results as text
                string jsonString = webRequest.downloadHandler.text;
                jsonString = fixJson(jsonString);
                BitbuildNFT[] nfts = JsonHelper.FromJson<BitbuildNFT>(jsonString);
                Debug.Log(nfts[0].name); // Output: "Metal Pant V3"

                InventoryManager.NFTData nftData = new InventoryManager.NFTData();
                foreach (var nft in nfts)
                {
                    string idString = nft.attributes[0].value;
                    int id = int.Parse(idString);
                    StartCoroutine(GetSprite(nft.image, nft.name, id));
                }


                nftData.name = nfts[0].name;
                InventoryManager.Instance.Add(nftData);
                InventoryManager.Instance.ListItems();
                Debug.Log(nfts[1].attributes[0].value); // Output: "This is the wearable NFT for the Bitbuild game. This is a wearable RHand. Item 22/24"
                Debug.Log(jsonString);
            }
        }
    }

    // return sprite from url
    IEnumerator GetSprite(string uri, string name, int id) {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
 
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(webRequest.error);
            } else {
                // Show results as text
                Texture2D texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                InventoryManager.NFTData nftData = new InventoryManager.NFTData();
                nftData.name = name;
                nftData.sprite = sprite;
                nftData.id = id;
                InventoryManager.Instance.Add(nftData);
                InventoryManager.Instance.ListItems();
                Debug.Log("Sprite loaded "+ nftData.name);
            }
        }
    }
}
