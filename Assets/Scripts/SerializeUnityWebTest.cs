using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SerializeUnityWebTest : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("localhost:3000/getOwnedNFT?ownerAddress=9Rc1PtEDtzAhXeGYJZJZxJBvF7YF85L7vDc2BTpsxNCQ"));
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
                Debug.Log(nfts[1].description); // Output: "This is the wearable NFT for the Bitbuild game. This is a wearable RHand. Item 22/24"
                Debug.Log(jsonString);
            }
        }
    }
}
