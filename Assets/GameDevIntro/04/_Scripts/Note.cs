using UnityEngine;

public class Note : MonoBehaviour
{
  // This is a multiline string that can be edited in the inspector
  // It is used to store the text of the note
  // The Multiline attribute allows for multiple lines of text
  // The text will be displayed in the inspector as a text area
  [TextArea(3, 10)]
  [SerializeField]
  [Multiline]
  public string noteText;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
