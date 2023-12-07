using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
   public Transform highlightedObj;
    public Transform selectedObj;
    public LayerMask selectableLayer;

    private Outline highlighOutline;
    private RaycastHit hit;

    private Camera cam;
    private Ray ray;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        Hover();
    }
    public void Hover()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                if (hit.transform == selectedObj && selectedObj != null) return;

                if (highlightedObj != hit.transform)
                {
                    if (highlightedObj != null) highlighOutline.enabled = false;

                    highlightedObj = hit.transform;
                    highlighOutline = highlightedObj.GetComponent<Outline>();
                    highlighOutline.OutlineColor = Color.yellow;
                    highlighOutline.enabled = true;
                }
            }
            else
            {
                if (highlightedObj != null)
                {
                    if(highlightedObj != selectedObj) highlighOutline.enabled = false;
                    highlightedObj = null;
                }
            }
        }
    }
    public void SelectHighlight(Transform obj)
    {
        if (selectedObj == obj) return;
        DeselectHighlight();
        highlightedObj = null;
        selectedObj = obj;
        obj.GetComponent<Outline>().enabled = true;
        obj.GetComponent<Outline>().OutlineColor = Color.red;

    }
    public void DeselectHighlight()
    {
        if (selectedObj == null) return;
        selectedObj.GetComponent<Outline>().enabled = false;
        selectedObj = null;
    }
    public void UnableHighlight()
    {
        if (highlightedObj != null)
        {
            if (highlightedObj != selectedObj) highlighOutline.enabled = false;
            highlightedObj = null;
        }
        this.enabled = false;
    }
}
