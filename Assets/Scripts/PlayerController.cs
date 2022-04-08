using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerSpeed;
    public int jumpForce;
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    Quaternion playerRotation;
    Quaternion camRotation;
    public Camera cam;
    public int rotationSpeed;
    float minX = -90f;
    float maxX = 90f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        //cam= GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void FixedUpdate()
    {
        float inputx = Input.GetAxis("Horizontal");
        float inputz = Input.GetAxis("Vertical");
        transform.position+=new Vector3(inputx * playerSpeed * Time.deltaTime, 0f, inputz * playerSpeed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.Space)&& IsGrounded())
        {
            rb.AddForce( Vector3.up * jumpForce);   
        }
        float mouseX = Input.GetAxis("Mouse X")*rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y")*rotationSpeed;


        playerRotation  =Quaternion.Euler( 0f,mouseY, 0f)*playerRotation;
        print("player" + playerRotation);
        camRotation=ClampRotationOfPlayer( Quaternion.Euler(-mouseX,0f, 0f) * camRotation);
        this.transform.localRotation = playerRotation;
        cam.transform.localRotation =camRotation;
        print("Cam" + camRotation);
    }
    public bool IsGrounded()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position,capsuleCollider.radius,Vector3.down,out hit,((capsuleCollider.height/2)-(capsuleCollider.radius))+.1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Quaternion ClampRotationOfPlayer(Quaternion n)
    {
        n.w = 1f;
        n.x /= n.w;
        n.y /= n.w;
        n.z /= n.w;
        float angleX=2.0f*Mathf.Rad2Deg*Mathf.Atan(n.x);
        angleX=Mathf.Clamp(angleX,minX,maxX);
        n.x=Mathf.Tan(Mathf.Deg2Rad*angleX*0.5f);
        return n;
    }
}
