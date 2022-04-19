using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceMarker : MonoBehaviour
{
    [SerializeField] Transform Up;
    [SerializeField] Transform Down;
    [SerializeField] Transform Left;
    [SerializeField] Transform Right;

    [SerializeField] Vector2 Target = new Vector2(128, 128);

    [SerializeField] Camera Cam;

    private Vector2 LastScreenScale;

    // Start is called before the first frame update
    void Start()
    {
        //Cam = Camera.main;
    }

    // Update is called once per frame
    [ContextMenu("Correct")]
    void Correct()
    {
        Vector3 up = Cam.WorldToScreenPoint(Up.position);
        Vector3 down = Cam.WorldToScreenPoint(Down.position);
        Vector3 left = Cam.WorldToScreenPoint(Left.position);
        Vector3 right = Cam.WorldToScreenPoint(Right.position);

        Vector2 screenScale = new Vector2(right.x - left.x, down.y - up.y );

        Vector2 unitScale = Target / screenScale;

        Debug.Log($"ScreenSpaceMarker :  up = {up} down {down} left {left} right {right}");
        Debug.Log($"ScreenSpaceMarker : scale vertical = {screenScale.y} horizontal = {screenScale.x}");
        Debug.Log($"ScreenSpaceMarker : mult vertical = {unitScale.y} horizontal = {unitScale.x}");
    }
}
