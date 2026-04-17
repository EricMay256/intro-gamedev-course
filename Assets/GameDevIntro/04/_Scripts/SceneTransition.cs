using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
  [SerializeField]
  string sceneName;
  void OnCollisionEnter(Collision collision)
  {
    Debug.Log($"Collision: {collision.gameObject.name}");
    OpenSceneForPlayer(collision.gameObject);
  }
  void OnCollisionEnter2D(Collision2D collision)
  {
    Debug.Log($"Collision2D: {collision.gameObject.name}");
    OpenSceneForPlayer(collision.gameObject);
  }
  void OnTriggerEnter(Collider other)
  {
    Debug.Log($"Trigger: {other.gameObject.name}");
    OpenSceneForPlayer(other.gameObject);
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log($"Trigger2D: {other.gameObject.name}");
    OpenSceneForPlayer(other.gameObject);
  }
  void OnControllerColliderHit(ControllerColliderHit hit)
  {
    Debug.Log($"ColliderHit: {hit.gameObject.name}");
    OpenSceneForPlayer(hit.gameObject);
  }
  public void OpenSceneForPlayer(GameObject player)
  {
    if (player.CompareTag("Player"))
    {
      TryOpenScene();
    }
  }
  public void TryOpenScene()
  {
    if(sceneName != null && sceneName != "")
    {
      SceneManager.LoadScene(sceneName);
    }
  }
}
