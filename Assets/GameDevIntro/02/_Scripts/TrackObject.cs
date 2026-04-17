using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    [SerializeField] Transform _trackedTransform;
    [SerializeField] bool _trackX = false, _trackY = false, _trackZ = false;
    Vector3 _newPosition;
    // Start is called before the first frame update
    void Start()
    {
        if(_trackedTransform == null)
         Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        _newPosition = transform.position;
        if(_trackX)
            _newPosition.x = _trackedTransform.position.x;
        if(_trackY)
            _newPosition.y = _trackedTransform.position.y;
        if(_trackZ)
            _newPosition.z = _trackedTransform.position.z;
        transform.position = _newPosition;
    }
}
