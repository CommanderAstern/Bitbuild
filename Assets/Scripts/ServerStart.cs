using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ServerStart : MonoBehaviour
{
    NetworkManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetworkManager>();
        string[] args = System.Environment.GetCommandLineArgs();
        for(int i = 0; i < args.Length; i++)
        {
            if(args[i] == "-server")
            {
                manager.StartServer();
            }
            else if(args[i] == "-client")
            {
                manager.StartClient();
            }
            else if(args[i] == "-host")
            {
                manager.StartHost();
            }
        }
    }
}
