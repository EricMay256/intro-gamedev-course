using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform _player;
    // Start is called before the first frame update
    [SerializeField]
    float _moveSpeed = 1f, _moveSpeedMultiplier = 1f;
    void Start()
    {
      //Finding an object by tag is more effective than using FindFirstObjectByType
      _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      Debug.Log("Enemy Created");
      Ready();
    }

    public void Ready()
    {
        _moveSpeedMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        _moveSpeedMultiplier += Time.deltaTime * 0.5f;
        transform.position = 
            Vector2.MoveTowards(transform.position, _player.position, 
            Time.deltaTime * _moveSpeed * _moveSpeedMultiplier);
    }

    void OnDestroy()
    {
        Debug.Log("Enemy Destroyed");
    }
}
