using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler instance;
    [SerializeField] public Camera cam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetCameraPosition(Vector3 pos)
    {
        cam.transform.position = pos;
    }
}
