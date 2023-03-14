using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject countController = GameObject.FindWithTag("ScoreController");
            countController.GetComponent<CountTracker>().IncrementCount();
            Destroy(gameObject);
        }
    }
}
