using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileUpdate : MonoBehaviour {
    [SerializeField] TMP_Text inputNameField;
    [SerializeField] TMP_Text inputBioField;
    
    public void UpdateProfile() {
        Debug.Log(inputBioField.text);
        Debug.Log(inputNameField.Text);
    }
}
