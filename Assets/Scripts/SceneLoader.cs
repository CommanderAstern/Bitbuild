using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum ScreenName
    {
        GameScene,
        Loading,
        MainMenuScene,
        ParkScene,
    }
    // Start is called before the first frame update
    public void LoadScene(ScreenName scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

