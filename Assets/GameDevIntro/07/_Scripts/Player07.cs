using UnityEngine;
using UnityEngine.InputSystem;

public class Player07 : MonoBehaviour
{
  Rigidbody2D _rb;
  [SerializeField]
  float _moveSpeed = 50f;
  Vector2 _movement;
  [SerializeField]
  ParticleSystem _particleSystem;
  ParticleSystem.ShapeModule _shapeModule;
  ParticleSystem.EmissionModule _emissionModule;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    if (!_rb)
    {
      Debug.LogError("Rigidbody2D component not found on Player07.");
    }
  }

  // Update is called once per frame
  void Update()
  {
    _rb.AddForce(_movement * _moveSpeed * Time.deltaTime);
    //_particleSystem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, (Mathf.PI + Mathf.Atan2(_rb.linearVelocityY, _rb.linearVelocityX)) * Mathf.Rad2Deg));
    _shapeModule = _particleSystem.shape;
    _emissionModule = _particleSystem.emission;
    _shapeModule.rotation = new Vector3(0, 0, (Mathf.PI + Mathf.Atan2(_rb.linearVelocityY, _rb.linearVelocityX)) * Mathf.Rad2Deg);
    _shapeModule.position = new Vector3(_rb.position.x, _rb.position.y, 0);
    _emissionModule.rateOverTime = _rb.linearVelocity.magnitude * 10f;
  }
    
  public void OnMove(InputValue value)
  {
    _movement = value.Get<Vector2>();
    if (_movement.magnitude > 1f)
    {
      _movement.Normalize();
    }
  }
}
