using UnityEngine;

public class MainMenuBtn : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
  public void GoToMainMenu()
  {
    if (!PlayerPrefs.HasKey("MainMenuType"))
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("04-Menu");
    }
    else switch(PlayerPrefs.GetInt("MainMenuType"))
    {
      case 0:
        UnityEngine.SceneManagement.SceneManager.LoadScene("04-Menu");
        break;
      case 1:
        UnityEngine.SceneManagement.SceneManager.LoadScene("04-2D");
        break;
      case 2:
        UnityEngine.SceneManagement.SceneManager.LoadScene("04-3D");
        break;
      default:
        UnityEngine.SceneManagement.SceneManager.LoadScene("04-Menu");
        Debug.LogWarning("Unknown MainMenuType, loading default scene.");
        break;
    }
  }
}
