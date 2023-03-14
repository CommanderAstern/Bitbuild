using UnityEngine;

public class DupeCoin : MonoBehaviour
{
    public int numberOfObjects = 100;
    public GameObject gameObjectCoin;
    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject newObject = Instantiate(gameObjectCoin);
            newObject.transform.position = new Vector3(Random.Range(-100f, 100f), -3.21f, Random.Range(-100, 100f));
        }
    }
}