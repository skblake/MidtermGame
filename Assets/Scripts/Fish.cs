using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public bool needsDest;
    public bool hungry; 

    protected Vector3 destination;

    private bool swimsRight = true;

    // Swims towards a destination
    public void Swim()
    {
        Vector3 swimVector = destination - transform.position;
        transform.position += swimVector.normalized * Time.deltaTime;

        // Stops the fish when it reaches its destination
        if (Vector3.Distance(destination, transform.position) < 0.1f)
            destination = transform.position;

        Debug.DrawLine(transform.position, destination, Color.yellow);
    }

    // Special behavior if the fish's destination is a food
    public void SetDestination(Food food)
    {
        food.AddChaser(this); 
        SetDestination(new Vector2(food.transform.position.x, food.transform.position.y));
    }

    public void SetDestination(Vector2 dest) 
    {
        destination = dest;
        swimsRight = dest.x > transform.position.x;

        if (swimsRight) {
            transform.rotation = Quaternion.Euler(0, -180f, 0);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Eat()
    {
        hungry = false;
    }
}