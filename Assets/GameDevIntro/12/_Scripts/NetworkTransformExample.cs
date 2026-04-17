using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransformExample : NetworkBehaviour
{
    void Update()
    {
        if (IsServer)
        {
            float theta = Time.frameCount / 30.0f;
            transform.position = new Vector3((float) Math.Cos(theta), (float) Math.Sin(theta));
        }
    }
}