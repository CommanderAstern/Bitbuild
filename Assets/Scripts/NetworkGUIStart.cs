// vis2k: GUILayout instead of spacey += ...; removed Update hotkeys to avoid
// confusion if someone accidentally presses one.
using UnityEngine;

namespace Mirror
{
    /// <summary>Shows NetworkManager controls in a GUI at runtime.</summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/Network Manager HUD")]
    [RequireComponent(typeof(NetworkManager))]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-manager-hud")]
    public class NetworkGUIStart : MonoBehaviour
    {
        NetworkManager manager;

        public int offsetX;
        public int offsetY;
        public GUIStyle buttonStyle;
        [SerializeField] GameObject wallet  ;
        void Awake()
        {
            manager = GetComponent<NetworkManager>();
            // FlowConfig flowConfig = new FlowConfig();
            // flowConfig.NetworkUrl = "https://rest-testnet.onflow.org/v1";

            // flowConfig.Protocol = FlowConfig.NetworkProtocol.HTTP;
            // FlowSDK.Init(flowConfig);
            // FlowSDK.RegisterWalletProvider(ScriptableObject.CreateInstance<DevWalletProvider>());

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

        void OnGUI()
        {
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 20;
            buttonStyle.fixedHeight = 60;
            buttonStyle.fixedWidth = 200;
            GUILayout.BeginArea(new Rect(10 + offsetX, 40 + offsetY, 700, 9999));
            if (!NetworkClient.isConnected && !NetworkServer.active)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();
            }

            // client ready
            if (NetworkClient.isConnected && !NetworkClient.ready)
            {
                if (GUILayout.Button("Client Ready",buttonStyle))
                {
                    NetworkClient.Ready();
                    if (NetworkClient.localPlayer == null)
                    {
                        NetworkClient.AddPlayer();
                    }
                }
            }

            StopButtons();

            GUILayout.EndArea();
        }

        void StartButtons()
        {
            if (!NetworkClient.active)
            {
                
            }
            else
            {
                // Connecting
                GUILayout.Label($"Connecting to {manager.networkAddress}..");
                if (GUILayout.Button("Cancel Connection Attempt",buttonStyle))
                {
                    manager.StopClient();
                    wallet.SetActive(true);
                }
            }
        }

        void StatusLabels()
        {
            // host mode
            // display separately because this always confused people:
            //   Server: ...
            //   Client: ...
            if (NetworkServer.active && NetworkClient.active)
            {
                GUILayout.Label($"<b>Host</b>: running via {Transport.active}");
            }
            // server only
            else if (NetworkServer.active)
            {
                GUILayout.Label($"<b>Server</b>: running via {Transport.active}");
            }
            // client only
            else if (NetworkClient.isConnected)
            {
                GUILayout.Label($"<b>Client</b>: connected to {manager.networkAddress} via {Transport.active}");
            }
        }

        void StopButtons()
        {
            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Stop Host",buttonStyle))
                {
                    manager.StopHost();
                    wallet.SetActive(true);
                }
                if (GUILayout.Button("Stop Client",buttonStyle))
                {
                    manager.StopClient();
                    wallet.SetActive(true);
                }
                GUILayout.EndHorizontal();
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Client",buttonStyle))
                {
                    manager.StopClient();
                    wallet.SetActive(true);
                }
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Stop Server",buttonStyle))
                {
                    manager.StopServer();
                    wallet.SetActive(true);
                }
            }
        }
    }
}
