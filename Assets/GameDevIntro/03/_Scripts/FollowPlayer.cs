using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
  float _z;
  Transform _player;
  // Start is called before the first frame update
  void Start()
  {
    _z = transform.position.z;
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    if (!_player)
    {
      Debug.LogError("Player not found");
      this.enabled = false;
    }
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = new Vector3(_player.position.x, _player.position.y, _z);
  }
    
}
