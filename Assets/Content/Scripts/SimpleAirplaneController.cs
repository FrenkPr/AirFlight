using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAirplaneController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody rb;
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = rb.velocity.z <= -190 ? new Vector3(0, 1, -1) : new Vector3(0, 0, -1);

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }

        rb.velocity += moveSpeed * dir * Time.deltaTime;

        //transform.eulerAngles = new Vector3(0, -90, 0);
        mainCamera.transform.localEulerAngles = new Vector3(13, 180, 0);

        //print(rb.velocity);
    }
}
