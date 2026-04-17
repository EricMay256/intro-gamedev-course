using UnityEngine;
using UnityEngine.UI;

public class PlayerController03 : MonoBehaviour
{
  float _moveSpeed = 5f;
  EnemyManager _enemyManager;
  int _totalHealth = 100, _curHealth = 100;
  public float HealthPct => (float)_curHealth / _totalHealth;
  [SerializeField]
  Slider _healthBarSlider;
  bool _isDead = false;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    _curHealth = _totalHealth;
    //Finding objects by tag is more effective than using FindObjectOfType, but this is being called once total
    _enemyManager = FindFirstObjectByType<EnemyManager>();
    if (!_enemyManager)
    {
      Debug.Log("EnemyManager not found");
    }
    StartGame();
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += new Vector3(
      PlayerInputManager.Instance.Movement.x,
      PlayerInputManager.Instance.Movement.y,
      0) * Time.deltaTime * _moveSpeed;
    if (PlayerInputManager.Instance.PausePressed && !_isDead)
    {
      GameManager03.Instance.PauseGame();
    }  
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (_enemyManager)
    {
      if (collision.CompareTag("Enemy"))
      {
        _enemyManager.EnemyKilled(collision.gameObject);
      }
    }
    else
    {
      Destroy(collision.gameObject);
    }
    _curHealth -= 5;
    _healthBarSlider.value = HealthPct;
    if (_curHealth <= 0)
    {
      _curHealth = 0;
      GameManager03.Instance.GameOver();
    }
  }

  public void StartGame()
  {
    _curHealth = _totalHealth;
    _healthBarSlider.value = HealthPct;
    _isDead = false;
    transform.position = Vector3.zero;
  }
}
