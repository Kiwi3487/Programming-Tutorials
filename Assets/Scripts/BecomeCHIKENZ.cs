using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeCHIKENZ : MonoBehaviour
{
    [SerializeField] private Rigidbody CHIKENZZPrefab;
    private Rigidbody rb;
    private float nextExecutionTime;
    private float time = 5f;
 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNextExecutionTime();
    }

    private void Update()
    {
        // Check if it's time to execute the method
        if (Time.time >= nextExecutionTime)
        {
            // Execute the method
            SpawnchickChick();

            // Calculate the next random execution time
            SetNextExecutionTime();
        }
    }

    private void SetNextExecutionTime()
    {
        nextExecutionTime = Time.time + time;
    }

    public void SpawnchickChick()
    {
        Instantiate(CHIKENZZPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

