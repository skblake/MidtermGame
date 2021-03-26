using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Vector3 destination;

    // Swims towards a destination
    public void Swim()
    {
        Vector3 swimVector = destination - transform.position;
        transform.position += swimVector.normalized * Time.deltaTime;
        transform.up = swimVector.normalized;

        if (Vector3.Distance(destination, transform.position) < 0.1f) {
            destination = transform.position;
            transform.rotation = (Quaternion.Euler(0, 0, 90));
        }

        Debug.DrawLine(transform.position, destination, Color.yellow);
    }
}