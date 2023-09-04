using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        _transform.Rotate(45 * Time.deltaTime, 0, 0);
    }

    #region Private

    private Transform _transform;

    #endregion
}
