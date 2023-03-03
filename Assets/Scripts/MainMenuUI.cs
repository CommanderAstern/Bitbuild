using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private FlowController flowController;
    // Start is called before the first frame update
    public enum ScreenName
    {
        GameScene,
        Loading,
        MainMenuScene,
        ParkScene,
    }
    private void Awake()
    {
        playButton.onClick.AddListener( () => {
            LoadScene(ScreenName.Loading);
            // flowController.SignInClicked();
        });
        quitButton.onClick.AddListener( () => {
            Application.Quit();
        });
    }

    public void LoadScene(ScreenName scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    // Update is called once per frame

}
