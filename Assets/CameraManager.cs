using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cmVirtualCam;
    public Camera mainCamera;
    public bool usingVirtualCam = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            usingVirtualCam = !usingVirtualCam;
            if (usingVirtualCam)
            {
                cmVirtualCam.gameObject.SetActive(true);
            }
            else
            {
                cmVirtualCam.gameObject.SetActive(false);
            }
        }
        if (!usingVirtualCam)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            if (x < 10)
            {
                mainCamera.transform.position -= Vector3.left * Time.deltaTime * 10;
            }

            else if (x > Screen.width - 10)
            {
                mainCamera.transform.position -= Vector3.right * Time.deltaTime * 10;
            }
            if (y < 10)
            {
                mainCamera.transform.position -= Vector3.back * Time.deltaTime * 10;
            }
            else if (y > Screen.width - 10)
            {
                mainCamera.transform.position -= Vector3.forward * Time.deltaTime * 10;
            }
        }
    }
}