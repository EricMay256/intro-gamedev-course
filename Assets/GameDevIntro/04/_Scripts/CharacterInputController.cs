using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputController : MonoBehaviour
{
  CharacterController _characterController;
  Vector3 _move, _look;
  float _verticalRotation, _verticalRotationLimit = 85f;
  [SerializeField] float _moveSpeed = 10f;
  void Awake()
  {

    _characterController = GetComponent<CharacterController>();
  }
  public void OnMove(InputValue value)
  {
    Vector2 move = value.Get<Vector2>();
    _move = new Vector3(move.x, 0, move.y);
    //Debug.Log($"Move: {_move}");
  }
  public void OnLook(InputValue value)
  {
    _look = value.Get<Vector2>();
    //Debug.Log($"Look: {_look}");
  }
  //Update is called once per frame
  void Update()
  {
    //Note: Multiplying the vector last is an optimization compared to multiplying it first
    _characterController.Move(_moveSpeed * Time.deltaTime *
    //We rotate the move vector by the Y-axis of the transform to make it relative to the camera
    //Note: Quaternion * Vector3 is a rotation of the vector, but we can not use Vector3 * Quaternion
    (Quaternion.Euler(0, transform.eulerAngles.y, 0) * _move));
    //Rotate horizontally around the Y-axis
    transform.Rotate(0, _look.x, 0, Space.World);

    //transform.Rotate(-_look.y, 0, 0, Space.Self); //This works, but allows us to flip backwards, so instead we:
    //Accumulate vertical rotation and clamp its value to prevent flipping backwards
    _verticalRotation -= _look.y;
    _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRotationLimit, _verticalRotationLimit);
    //Apply vertical rotation around the X-axis
    transform.localRotation = Quaternion.Euler(_verticalRotation, transform.eulerAngles.y, 0);

    //Reset look input to zero after applying it
    _look = Vector2.zero;
  }
  
}
