using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
  public void LoadScene(string sceneName)
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
  }
}
