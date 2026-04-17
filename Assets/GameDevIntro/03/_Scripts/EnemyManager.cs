using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitPool))]
public class EnemyManager : MonoBehaviour
{
  [SerializeField]
  int _maxEnemiesSpawned = 100, _spawnsPerSecond = 1, _curSpawned = 0;
  [SerializeField]
  GameObject _enemyPrefab;
  [SerializeField]
  Transform _enemyHolder;
  [SerializeField]
  bool _usePooling = false, _uniqueMaterial = false;

  public UnitPool Pool => _pool;//This is shorthand to allow public read-only access to _pool
  private UnitPool _pool;
  Transform _player;
  float _timer = 0;

  // Start is called before the first frame update
  void Awake()
  {
    _pool = GetComponent<UnitPool>();
    _pool.SetEnemyHolder(_enemyHolder ? _enemyHolder : transform);
    _pool.SetEnemyPrefab(_enemyPrefab);
    _player = GameObject.FindGameObjectWithTag("Player").transform;
  }
  void Start()
  {
  }

  public void EnemyKilled(GameObject enemy)
  {
    _curSpawned--;
    Debug.Log("Enemy Killed");
    if (_usePooling)
      _pool.Pool.Release(enemy);
    else
      Destroy(enemy);
  }

  GameObject SpawnEnemy()
  {
    GameObject go;
    if (_usePooling)
    {
      go = _pool.Pool.Get();
      go.GetComponent<Enemy>().Ready();
    }
    else
    {
      go = Instantiate(_enemyPrefab, _enemyHolder);
    }
    go.transform.position = new Vector3(_player.position.x + Random.Range(-8f, 8),
        _player.position.y + Random.Range(-8f, 8));

    if (_uniqueMaterial)
    {
      //This code block automatically instantiates the materials and makes them unique to this renderer. It is your responsibility to destroy the materials when the game object is being destroyed. 
      go.GetComponent<SpriteRenderer>().material.color = Random.ColorHSV();
    }
    //go.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    _curSpawned++;
    return go;
  }

  // Update is called once per frame
  void Update()
  {
    _timer += Time.deltaTime * Time.timeScale;
    while (_curSpawned < _maxEnemiesSpawned && _timer > 1f / _spawnsPerSecond)
    {
      SpawnEnemy();
      _timer -= 1f / _spawnsPerSecond;
    }
  }

  public void ClearEnemies()
  {
    if (_usePooling)
    {
      foreach (Transform child in _enemyHolder)
      {
        _pool.Pool.Release(child.gameObject);
      }
    }
    else
    {
      foreach (Transform child in _enemyHolder)
      {
        Destroy(child.gameObject);
      }
    }
  }
}
