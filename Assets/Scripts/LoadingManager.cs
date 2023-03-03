using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
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
        WaitForSeconds wait = new WaitForSeconds(0.3f);
        StartCoroutine(LoadScene(ScreenName.ParkScene, wait));
    }

    IEnumerator LoadScene(ScreenName scene, WaitForSeconds wait)
    {
        yield return wait;
        SceneManager.LoadScene(scene.ToString());
    }
    // Update is called once per frame
}
