using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamara : MonoBehaviour
{
    [SerializeField]
    private Camera cameraObj;
    private GameObject player;
    private Transform playerTf;
    private Transform cameraTf;
    public float bottomY { get; private set; }

    private void Awake()
    {
        cameraObj = this.GetComponent<Camera>();
        player = GameObject.Find("Player");
        playerTf = player.transform;
        cameraTf = playerTf;
    }

    private void Start()
    {
        bottomY = transform.position.y - 5f;
    }

    private void Update()
    {
        bottomY = transform.position.y - 5f;

        transform.position = new Vector3(transform.position.x, cameraTf.position.y + 3.5f, transform.position.z); ;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0f, 0.1f, 0f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0f, 0.1f, 0f);
        }
    }
}
