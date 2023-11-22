using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Collectable : MonoBehaviour
{
    //public static int pointCounter = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /*pointCounter++;
            Debug.Log("Points: " + pointCounter);*/
            // So this script can access the player script that has the methods needed
            Player player = other.GetComponent<Player>();
            //To avoid NullReferenceException ;-;
            /*NullReferenceException: Object reference not set to an instance of an object
            Collectable.OnTriggerEnter (UnityEngine.Collider other) (at Assets/Scripts/Collectables.cs:17)*/
            if (player != null)
            {
                player.Reload();
            }
            Destroy(gameObject);
        }
    }
}