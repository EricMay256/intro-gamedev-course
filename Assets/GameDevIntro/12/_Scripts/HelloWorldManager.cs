using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        //Can't serialize static fields
        [SerializeField]
        static GameObject enemyPrefab; 
        //Hacky workaround to work alongside static function call
        [SerializeField]
        GameObject prefab_selector;
        
        void OnGUI()
        {
            //Exit if network manager doesn't exist - such as exiting scene
            if(!NetworkManager.Singleton) return;

            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && 
              !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        void Start()
        {
            enemyPrefab = prefab_selector;
        }

        static void SpawnNetworkedEnemy()
        {
                var p = Instantiate(enemyPrefab);
                var networkObject = p.GetComponent<NetworkObject>();
                networkObject.Spawn();
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Host")) {
                NetworkManager.Singleton.StartHost();
                SpawnNetworkedEnemy();
            } 
            if (GUILayout.Button("Client")) {
                NetworkManager.Singleton.StartClient();
                //SpawnNetworkedEnemy();
            }
            if (GUILayout.Button("Server")) {
                NetworkManager.Singleton.StartServer();
                SpawnNetworkedEnemy();
            }
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient )
                {
                    foreach (ulong uid in NetworkManager.Singleton.ConnectedClientsIds)
                        NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<HelloWorldPlayer>().MoveToRandomPosition();
                }
                else
                {
                    var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                    var player = playerObject!.GetComponent<HelloWorldPlayer>();
                    player!.MoveToRandomPosition();
                }
            }
        }
    }
}