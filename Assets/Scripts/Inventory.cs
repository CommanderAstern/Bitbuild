using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    // set the current game object inactive
    public void Disable() {
        gameObject.SetActive(false);
    }
}
