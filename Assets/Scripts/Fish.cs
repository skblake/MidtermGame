using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public bool needsDest;
    public bool hungry;
    public float age = 0f;
    public float secsToAgeUp = 30f;
    [HideInInspector] public int evolution = 0; 

    protected Vector3 destination;

    private FishGod god;
    private bool swimsRight = true;
    public List<FishAppearance> sons = new List<FishAppearance>();

    void Update() 
    { 
        Swim();

        age += Time.deltaTime / secsToAgeUp; 
        //if ((int)age != evolution) god.AgeUp();
    }

    // Swims towards a destination
    public void Swim()
    {
        Vector3 swimVector = destination - transform.position;
        transform.position += swimVector.normalized * Time.deltaTime;
        Debug.DrawLine(transform.position, destination, Color.yellow);

        // Stops the fish when it reaches its destination
        if (Vector3.Distance(destination, transform.position) < 0.1f) {
            destination = transform.position;
            needsDest = true;
        }        
    }

    // Special behavior if the fish's destination is a food
    public void SetDestination(Food food)
    {
        // food.AddChaser(this); 
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

    public void MakeSons(List<FishAppearance> prefabs)
    {
        foreach (FishAppearance prefab in prefabs) {
            FishAppearance evolution = GameObject.Instantiate(prefab, transform);
            evolution.GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
            evolution.SetIsSleeping(true);
            sons.Add(evolution);
        }
    }

    public void Eat()
    {
        hungry = false;
    }
    /*
    void AgeUp()
    {
        if (god.GetLevel() < sons.Count) {
            sons[god.GetLevel()].SetIsSleeping(true);
            god.LevelUp()++;
            sons[god.GetLevel()].SetIsSleeping(false);
        }
    }
    */

    public void SetGod(FishGod g) => god = g;
}