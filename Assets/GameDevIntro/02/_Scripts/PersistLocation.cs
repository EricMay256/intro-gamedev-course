using UnityEngine;

public class PersistLocation : MonoBehaviour
{
    void Awake()
    {
        if(PlayerPrefs.HasKey("PersistLocation"))
        {
            Vector3 v = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PersistLocation"));
            Debug.Log("Loaded " + v);
            transform.position = v;
        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        PlayerPrefs.SetString("PersistLocation", JsonUtility.ToJson(transform.position));
        PlayerPrefs.Save();
        Debug.Log("Saved " + transform.position);
    }
}
