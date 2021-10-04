using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamara : MonoBehaviour
{
    [SerializeField]
    private Camera cameraObj;

    private void Start()
    {
        cameraObj = this.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0f, 0.1f, 0f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0f, 0.1f, 0f);
        }
    }

    public Vector3 GetScrrenTopLeft()
    {
        Vector3 topLeft = cameraObj.ScreenToWorldPoint(Vector3.zero);
        topLeft.Scale(new Vector3(1f, -1f, 1f));

        return topLeft;
    }

    public Vector3 GetScrrenBottomRight()
    {
        Vector3 bottomRight = cameraObj.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        bottomRight.Scale(new Vector3(1f, -1f, 1f));

        return bottomRight;
    }
}
