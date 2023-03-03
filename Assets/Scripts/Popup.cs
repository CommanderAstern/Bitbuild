using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Text popupText; // The text component to display the popup message
    public Button quitButton; // The button component to quit the game

    // Create a popup with the specified message
    public void CreatePopup(string message)
    {
        // Set the popup text
        popupText.text = message;

        // Show the popup and disable player input
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Add a listener to the quit button
        quitButton.onClick.AddListener(QuitGame);
    }

    // Quit the game
    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
