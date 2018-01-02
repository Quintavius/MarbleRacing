using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CameraOutline : MonoBehaviour
{
    [HideInInspector]
    public bool showOutline;
    Camera cam;
    public RectTransform outlinePanel;
    public Color playerColor;
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (showOutline)
        {
			outlinePanel.gameObject.SetActive(true);
            var x = cam.rect.position.x;
            var y = cam.rect.position.y;
            var w = cam.rect.width;
            var h = cam.rect.height;
            outlinePanel.position = new Vector3(Screen.width * x, Screen.height * y);
            outlinePanel.sizeDelta = new Vector2(Screen.width * w, Screen.height * h);
            outlinePanel.GetComponent<Image>().color = playerColor;
        }
        else
        {
			outlinePanel.gameObject.SetActive(false);
        }
    }
}
