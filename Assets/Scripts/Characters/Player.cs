using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private float speed;
    
    private Vector3 _moveDirection;

    [SerializeField] private float jumpForce;

    private bool isGrounded;

    private Rigidbody rb;

    [Header("Camera")]
    [SerializeField, Range(1,20)] private float mouseSensX;
    [SerializeField, Range(1,20)] private float mouseSensY;
    
    [SerializeField, Range(-90,0)] private float minViewAngle;
    [SerializeField, Range(0,90)] private float maxViewAngle;

    [SerializeField] private Transform followTarget; 
    
    private Vector2 currentAngle;

    
    [Header("Shooting")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float projectileForce; 
    
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Init(this);
        
        InputManager.SetGameControls();
        
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDirection);
        
        //calls the check ground method 
        checkGround();
    }

    public void SetMovementDirection(Vector3 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void jump()
    {

        if (isGrounded)
        {
            //if player is grounded adds force to the rigidbody instantly 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        
    }


    private void checkGround()
    {
        //checks to see if the object is touching the ground 
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.size.y);
        Debug.DrawRay(transform.position,Vector3.down * GetComponent<Collider>().bounds.size.y, Color.green,0,false);
        
    }

    public void Shoot()
    {
        
       Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
       
       currentProjectile.AddForce(followTarget.forward * projectileForce, ForceMode.Impulse);
       
       Destroy(currentProjectile.gameObject, 4);
        

    }
    

    public void SetLookRotation(Vector2 readValue)
    {
        currentAngle.x += readValue.x * Time.deltaTime * mouseSensX;
        currentAngle.y += readValue.y * Time.deltaTime * mouseSensY;

        currentAngle.y = Mathf.Clamp(currentAngle.y, minViewAngle, maxViewAngle);
        
        
        transform.rotation = Quaternion.AngleAxis(currentAngle.x, Vector3.up);
        followTarget.localRotation = Quaternion.AngleAxis(currentAngle.y, Vector3.right);
    
    }
}
