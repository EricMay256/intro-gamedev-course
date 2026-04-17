using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        [SerializeField]
        GameObject emotePrefab;
        GameObject spawnedEmote;
        Vector3 inputDirection = Vector3.zero;
        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                MoveToRandomPosition();
            }
            else
            {
                GetComponent<PlayerInput>().enabled = false;
            }
        }

        public void MoveToRandomPosition()
        {
            SubmitPositionRequestServerRpc();
        }

        [Rpc(SendTo.Server)]
        void SubmitPositionRequestServerRpc(RpcParams rpcParams = default)
        {
            var randomPosition = GetRandomPosition();
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }

        [Rpc(SendTo.Server)]
        void SubmitInputRequestServerRpc(Vector3 inputDir, RpcParams rpcParams = default)
        {
            inputDirection = inputDir;
            //The server's representation of the client's object is storing current input
        }

        [Rpc(SendTo.Everyone)]
        void SubmitEmoteActionEveryoneRpc(Vector3 position, RpcParams rpcParams = default)
        {   
            //If the local emote has not disappeared, do not spawn a new one
            //Discourages bad actors from spamming emotes
            //if(!spawnedEmote)
            {
                spawnedEmote = Instantiate(emotePrefab, transform.position, Quaternion.identity);
            }
        }

        static Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }

        void Update()
        {
            transform.position = Position.Value;
        }

        void FixedUpdate()
        {
            if(IsServer)
            {
            Position.Value = transform.position + inputDirection*Time.fixedDeltaTime;
            }
            transform.position = Position.Value;
        }

        public void OnMove(InputValue direction)
        {
            if(IsOwner)
            {
                inputDirection = direction.Get<Vector2>();
                SubmitInputRequestServerRpc(inputDirection);
            }
        }

        public void OnFire()
        {
            Debug.Log("fire");
            Debug.Log("Emote");
            if(IsOwner)
            {
                //Only send the spawning call to everyone if the old emote has not yet finished
                if(!spawnedEmote)
                {
                    SubmitEmoteActionEveryoneRpc(transform.position);
                }
            }
        }

        public void OnEmote()
        {
            Debug.Log("Emote");
            if(IsOwner)
            {
                //Only send the spawning call to everyone if the old emote has not yet finished
                //if(!spawnedEmote)
                {
                    SubmitEmoteActionEveryoneRpc(transform.position);
                }
            }
        }
    }
}