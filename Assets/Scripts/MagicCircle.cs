using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicCircle : MonoBehaviour
{
    public float detectionRadius = 2f;
    public KeyCode teleportButton = KeyCode.E;
    public GameObject popupUI;
    public GameObject Player;

    // public TMP_Text popupText;
    // public string nextLevelName;

    void Update()
    {
        if (Input.GetKeyDown(teleportButton) && IsPlayerInRange())
        {
            DontDestroyOnLoad(Player);
            DontDestroyOnLoad(GameObject.FindWithTag("FollowCamera"));
            SceneManager.LoadScene(1);
        }

        // if(IsPlayerInRange()){
        //     Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        //     int flag = 0;
        //     foreach (Collider collider in colliders)
        //     {
        //         if (collider.CompareTag("Player"))
        //         {
        //             flag = 1;
        //             Debug.Log(collider.gameObject.transform.GetChild(5).GetChild(0).gameObject.name);
        //             collider.gameObject.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
        //         }
        //     }
        //     if (flag == 0)
        //     {
        //         GameObject.Find("Player").transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
        //     }
        // }
    }

    bool IsPlayerInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Player = collider.gameObject;
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

