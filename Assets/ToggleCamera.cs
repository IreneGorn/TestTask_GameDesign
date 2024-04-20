using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook virtualCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            virtualCamera.enabled = !virtualCamera.enabled;
        }
    }
}
