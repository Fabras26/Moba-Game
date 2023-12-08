using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardCanvas : MonoBehaviour
{
    Transform cam;
    public Transform owner;
    void Start()
    {
        cam = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.rotation = cam.rotation;
        transform.position = owner.position + new Vector3(0,2f,-0.5f);
    }
}
