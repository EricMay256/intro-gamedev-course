using UnityEngine;

public class GameManager03 : MonoBehaviour
{
  public static GameManager03 Instance { get; private set; }
  [SerializeField]
  PlayerController03 _playerController;
  [SerializeField]
  Canvas _gameOverCanvas, _pauseCanvas;
  [SerializeField]
  EnemyManager _enemyManager;
  bool _isPaused = false;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
    _gameOverCanvas.gameObject.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void GameOver()
  {
    Time.timeScale = 0;
    Debug.Log("Game Over");
    _gameOverCanvas.gameObject.SetActive(true);
  }

  public void StartGame()
  {
    Time.timeScale = 1;
    Debug.Log("Game Restarted");
    _gameOverCanvas.gameObject.SetActive(false);
    _playerController.StartGame();
    _enemyManager.ClearEnemies();
  }

  public void QuitGame()
  {
    //Note: This will not work in the editor, but will work in a built game
    Application.Quit();
    Debug.Log("Quit Game");
  }

  public void PauseGame()
  {
    if (_isPaused)
    {
      Time.timeScale = 1;
      _pauseCanvas.gameObject.SetActive(false);
      _isPaused = false;
    }
    else
    {
      Time.timeScale = 0;
      _pauseCanvas.gameObject.SetActive(true);
      _isPaused = true;
    }
  }
}
