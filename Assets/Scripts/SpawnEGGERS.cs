using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEGGERS : MonoBehaviour
{
    [SerializeField] private Rigidbody EggersPrefab;
    private Rigidbody rb;
    private float nextExecutionTime;
    private float minInterval = 5f;
    private float maxInterval = 10f;

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
            spawnEggers();

            // Calculate the next random execution time
            SetNextExecutionTime();
        }
    }

    private void SetNextExecutionTime()
    {
        nextExecutionTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    public void spawnEggers()
    {
        Instantiate(EggersPrefab, transform.position, Quaternion.identity);
    }
}
        
