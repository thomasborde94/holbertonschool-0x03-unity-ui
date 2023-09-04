using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Show in Inspector

    public GameObject player;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    #endregion

    private void Start()
    {
        _offset = _transform.position - player.transform.position;
    }

    void Update()
    {
        _transform.position = player.transform.position + _offset;
    }

    #region Private

    private Vector3 _offset;
    private Transform _transform;

    #endregion
}
