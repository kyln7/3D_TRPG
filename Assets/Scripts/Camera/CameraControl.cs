using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float detectLen = 2f;
    private Vector3 resPos;
    // Update is called once per frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        if (GameControl.inputMode == GameControl.InputMode.Game)
        {
            resPos = Camera.main.transform.position;
            if (Mathf.Abs(Input.mousePosition.x - Screen.width) <= detectLen || Input.GetKey(KeyCode.RightArrow))
            {
                resPos += transform.right * Time.deltaTime * moveSpeed;
            }
            if (Mathf.Abs(Input.mousePosition.x) <= detectLen || Input.GetKey(KeyCode.LeftArrow))
            {
                resPos -= transform.right * Time.deltaTime * moveSpeed;
            }
            if (Mathf.Abs(Input.mousePosition.y - Screen.height) <= detectLen || Input.GetKey(KeyCode.UpArrow))
            {
                resPos += transform.up * Time.deltaTime * moveSpeed;
            }
            if (Mathf.Abs(Input.mousePosition.y) <= detectLen || Input.GetKey(KeyCode.DownArrow))
            {
                resPos -= transform.up * Time.deltaTime * moveSpeed;
            }
            RaycastHit hit;
            if (Physics.Raycast(resPos, transform.forward, out hit, Mathf.Infinity))
            {
                transform.position = resPos;
            }
            Debug.DrawLine(transform.position, transform.position + transform.forward * 100f, Color.black, 0.1f);
        }
    }
}
