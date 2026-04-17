using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CPSC386
{
  [AddComponentMenu("Custom/Custom Script Example")]
  public class CustomScriptExample : MonoBehaviour
  {
    [Tooltip("This vector can be read and changed outside of the class.")]
    public Vector3 PublicVector = new Vector3(3, 0, 0);

    //This vector is protected, which means it is not exposed to serialization or outside use
    public Vector3 SerializedVector => _serializedVector;

    [Tooltip("This vector is not accessible outside of the class, but can be manipulated and observed through the editor.")]
    [SerializeField]
    private Vector3 _serializedVector = new Vector3(0, 3, 0);

    [Header("Object Settings")]
    [Space(10)]
    [SerializeField]
    [Tooltip("The default values for the component is based on initialization within the class declaration.")]
    private Color _color = Color.magenta;

    [SerializeField]
    [Range(0.2f, 5f)]
    [Tooltip("Constraints on values can be implemented to control serialization, avoiding scenarios like inappropriate negatives or division by 0. This does not prevent you from going outside of these values within the script.")]
    private float _scale = 1;

    //In classes, the default access modifier is private, so this is equivalent to private float _zLevel;
    //Explicitly declaring it as private is a good practice, as it makes the code more readable
    [SerializeField]
    private float _zLevel;

    [SerializeField]
    private bool _logUpdate = true;

    private bool _nonSerializedBoolean = false;

    //Using this functionality, we can create methods that can be executed on command from the editor
    //The context menu entry will be available when right clicking the component's header in the inspector
    [ContextMenu("Randomize Color")]
    void AssignRandomColor()
    {
      _color = Random.ColorHSV();
      Debug.Log("Function Executed");
    }

    void Start()
    {
      Debug.Log("Start");
      GetComponent<SpriteRenderer>().color = _color;
      transform.localScale = new Vector3(_scale, _scale, 1);
      if(_nonSerializedBoolean)
        Debug.Log("You have to change the value of the variable in code or make it serializable to reach this statement.");
    }

    void Awake()
    {
      Debug.Log("Awake");
    }

    void OnEnable()
    {
      Debug.Log("Enabled");
    }

    // Update is called once per frame
    void Update()
    {
      if (_logUpdate)
      {
        Debug.Log("Update");
        _logUpdate = false;
      }
      Debug.Log("Updating");
    }
  }
}