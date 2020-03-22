using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodMesurement : MonoBehaviour
{
    Vector2 startMousePos = new Vector2();

    public float rodMesurement = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = MouseWorldPosition();
            startMousePos = mousePos;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = MouseWorldPosition();
            Vector3 cameraPos = Camera.main.transform.position;

            Vector2 direction = mousePos - startMousePos;

            float snapedYPos = Mathf.Round((cameraPos.y - direction.y) * 100) / 100;
            print(snapedYPos);

            Camera.main.transform.position = new Vector3(
                0f,
                snapedYPos,
                cameraPos.z
            );
        }

        rodMesurement = Camera.main.transform.position.y;

    }

    private static Vector2 MouseWorldPosition()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(clickPos);
        return mousePos;
    }
}
