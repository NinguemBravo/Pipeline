using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;
    public float jumpHeight;
    //*public float gravity = 2f;
    public bool isGrounded;

    public bool travarMouse = true;


    public float maxaX;
    public float minaX;

    private Quaternion camRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (!travarMouse)
        {
            return;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();

        camRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Controles();
        Correr();
        Rotacionar();
    }

    private void Controles()
    {
        if (Input.GetButton("W"))
        {
            rb.AddForce(transform.forward * velocity, ForceMode.Acceleration);
        }
        if (Input.GetButton("A"))
        {
            rb.AddForce(transform.right * -velocity, ForceMode.Acceleration);
        }
        if (Input.GetButton("S"))
        {
            rb.AddForce(transform.forward * -velocity, ForceMode.Acceleration);
        }
        if (Input.GetButton("D"))
        {
            rb.AddForce(transform.right * velocity, ForceMode.Acceleration);
        }
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpHeight);
            }
        }
        if (!isGrounded)
        {
            // rb.AddForce(Vector3.down * gravity);
        }
    }

    private void Correr()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetButton("W")))
        {
            if (velocity < 20)
            {
                velocity = velocity + 3;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W))
        {
            if (velocity > 15)
            {
                velocity = velocity - 3;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    private void Rotacionar() {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }
}
