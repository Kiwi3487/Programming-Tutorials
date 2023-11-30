using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] private float originalSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float increasedSpeed;
    [SerializeField] private float speedPowerDuration;
    private float speedPowerTimer;
    
    [SerializeField] private float jumpForce;
    private bool hasJumpBoost;
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
    
    //Check points
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> checkPoints;
    [SerializeField] private Vector3 vectorPoint;
    [SerializeField] private float dead;
    private int totalDeath = 0;
    
    
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
        if (player.transform.position.y < -dead)
        {
            player.transform.position = vectorPoint;
            totalDeath++;
        }
        
        if (speedPowerTimer > 0f)
        {
            speedPowerTimer -= Time.deltaTime;
            if (speedPowerTimer <= 0f)
            {
                speed = originalSpeed;
            }
        }
        
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            vectorPoint = player.transform.position;
        }
        if (other.gameObject.CompareTag("Win"))
        {
            WinScreen();
        }
        if (other.gameObject.CompareTag("SpeedPower"))
        {
            ApplySpeedPower();
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("JumpBoost"))
        {
            ApplyJumpBoost();
            other.gameObject.SetActive(false);
        }
    }

    private void ApplySpeedPower()
    {
        speed = increasedSpeed;
        speedPowerTimer = speedPowerDuration;
    }

    private void ApplyJumpBoost()
    {
        hasJumpBoost = true;
        jumpForce *= 2; 
    }
    public void ResetToCheckPoint()
    {
        player.transform.position = vectorPoint;
    }

    public int GetCurrentBullets()
    {
        return currentBullets;
    }
    public int GetCurrentDeaths()
    {
        return totalDeath;
    }
    public void WinScreen()
    {
        SceneManager.LoadScene("Scenes/Win");
    }
}