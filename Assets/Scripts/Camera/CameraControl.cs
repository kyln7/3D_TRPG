using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4f;
    private Vector3 resPos;
    // Update is called once per frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        if (Mathf.Abs(Input.mousePosition.x - Screen.width) <= 0.5f)
        {
            Debug.Log("xx");
        }
        resPos = Camera.main.transform.position;
        if (Input.GetAxis("Horizontal") == 1)
        {
            resPos += transform.right * Time.deltaTime * moveSpeed;
        }
        if (Input.GetAxis("Horizontal") == -1)
        {
            resPos -= transform.right * Time.deltaTime * moveSpeed;
        }
        if (Input.GetAxis("Vertical") == 1)
        {
            resPos += transform.up * Time.deltaTime * moveSpeed;
        }
        if (Input.GetAxis("Vertical") == -1)
        {
            resPos += -transform.up * Time.deltaTime * moveSpeed;
        }
        RaycastHit hit;
        if (Physics.Raycast(resPos, transform.forward, out hit, Mathf.Infinity))
        {
            transform.position = resPos;
        }
        Debug.DrawLine(transform.position, transform.position + transform.forward * 100f, Color.black, 0.1f);
    }
}
