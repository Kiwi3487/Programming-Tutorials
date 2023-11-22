using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayers;
    private Vector2 currentRotation;
    //Camera movement
    [SerializeField, Range(1,20)] private float mouseSensX;
    [SerializeField, Range(1,20)] private float mouseSensY;
    
    [SerializeField, Range(-90,0)] private float minViewAngle;
    [SerializeField, Range(0,90)] private float maxViewAngle;
    [SerializeField] private Transform followTarget;
    //shoot
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float projectileForce;
    
    //Reload stuffs
    [SerializeField] private int maxBullets = 5;
    [SerializeField] private int minBullets = 0;
    private int currentBullets;
    
    
    private Vector2 currentAngle;
    
    private bool isGrounded;
    private Vector3 _moveDirection;
    private Rigidbody rb;
    private float depth;
    
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Init(this);
        InputManager.SetGameControls();
        rb = GetComponent<Rigidbody>();
        depth = GetComponent<Collider>().bounds.size.y;
        currentBullets = maxBullets;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDirection);
        CheckGround();
    }
    public void SetMovementDirection(Vector3 currentDirection)
    {
        _moveDirection = currentDirection;
    }
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, depth,groundLayers);
        Debug.DrawRay(transform.position, Vector3.down * depth,
            Color.green, 0, false);
    }
    public void SetLookRotation(Vector2 readValue)
    {
        currentAngle.x += readValue.x * Time.deltaTime * mouseSensX;
        currentAngle.y += readValue.y * Time.deltaTime * mouseSensY;
        currentAngle.y = Mathf.Clamp(currentAngle.y, minViewAngle, maxViewAngle);
        
        
        transform.rotation = Quaternion.AngleAxis(currentAngle.x, Vector3.up);
        followTarget.localRotation = Quaternion.AngleAxis(currentAngle.y, Vector3.right);
    }
    public void Shoot()
    {
        if (currentBullets > 0)
        {
            Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            currentProjectile.AddForce(followTarget.forward * projectileForce, ForceMode.Impulse);
            Destroy(currentProjectile.gameObject, 4);
            //bullet -1
            currentBullets--;
        }
        else
        {
            Debug.Log("No bullet bozo");
        }
    }
    public void Reload()
    {
        int bulletAmount = maxBullets - currentBullets;
        currentBullets += bulletAmount;
        Debug.Log("Shoot MOAR");
    }
    
    public int GetCurrentBullets()
    {
        return currentBullets;
    }
}