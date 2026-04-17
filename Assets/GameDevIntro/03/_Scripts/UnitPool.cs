using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnitPool : MonoBehaviour
{
  public ObjectPool<GameObject> Pool { get; protected set; }
  GameObject _enemyPrefab;
  Transform _enemyHolder;
  // Start is called before the first frame update
  void OnEnable()
  {
    Pool = new ObjectPool<GameObject>(CreatePooledEnemy, OnGetEnemyFromPool, OnReleasedToPool, OnDestroyFromPool);
  }

  public void SetEnemyHolder(Transform enemyHolder)
  {
    _enemyHolder = enemyHolder;
  }

  public void SetEnemyPrefab(GameObject enemyPrefab)
  {
    _enemyPrefab = enemyPrefab;
  }

  GameObject CreatePooledEnemy()
  {
    GameObject obj = Instantiate(_enemyPrefab);
    obj.SetActive(false);
    obj.transform.SetParent(_enemyHolder ? _enemyHolder : transform);
    return obj;
  }

  void OnGetEnemyFromPool(GameObject b)
  {
    b.gameObject.SetActive(true);
  }

  void OnReleasedToPool(GameObject b)
  {
    b.gameObject.SetActive(false);
  }

  void OnDestroyFromPool(GameObject b)
  {
    Destroy(b.gameObject);
  }
}