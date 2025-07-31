using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCursor : MonoBehaviour
{
    public Vector2 lookVector = new Vector2(0, 0);

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane; // what was this for? interfering with perspective camera
        //  mousePosition.z = 0;
        transform.position = mousePosition;

        // the above worked with an orthographic camera, the below is for a perspective camera:

        Vector3 mousePos = Input.mousePosition;
        // Debug.Log(mousePos);

        // can we get the most recent input type? (controller or keyboard)
        if (true)
        {
            Vector2 lookVec = ((Vector2) lookVector + new Vector2(1, 1)) / 2;
            Debug.Log(lookVec);
            float radius = Mathf.Min(Screen.width, Screen.height);
            mousePos = new Vector3(Screen.width * lookVec.x, Screen.height * lookVec.y, 0);
        }

        // Ray ray = Camera.main.ScreenPointToRay(mousePos);
        // Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
        // float distance;
        // xy.Raycast(ray, out distance);
        // transform.position = ray.GetPoint(distance);
    }

}