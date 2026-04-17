using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleTopDownMovement : MonoBehaviour
{
  [SerializeField]
  private float _speed = 5f;
  private Vector3 _moveInput = Vector2.zero;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  public void OnMove(InputValue value)
  {
    _moveInput = value.Get<Vector2>();
  }
  public void OnAttack()
  {
    Debug.Log("Attack!");
  }
  public void OnEmote(InputValue value)
  {
    Debug.Log("Emote!");//: " + value.Get<float>());
  }

  void Update()
  {
    transform.position += _moveInput * _speed * Time.deltaTime;
  }
}
