using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public bool needsDest;
    public bool hungry;
    public float age = 0f;
    public float secsToAgeUp = 30f;
    public float hungerThreshold = 5f; // Hunger timer in seconds
    
    [HideInInspector] 
    public List<FishAppearance> sons = new List<FishAppearance>();
    [HideInInspector]
    public FishGod.FishState state;

    private Vector3 destination;
    private FishGod god;
    private bool swimsRight = true;
    private float hungerTimer = 0f;

    void Update() 
    { 
        age += Time.deltaTime / secsToAgeUp; 
        hungerTimer += Time.deltaTime;

        if (state != FishGod.FishState.Hungry && hungerTimer > hungerThreshold) 
            ChangeState(FishGod.FishState.Hungry); 
        
        if ((int)age != god.GetLevel()) AgeUp();

        Swim();
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
            evolution.LinkAndSleep(god);
            evolution.GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
            sons.Add(evolution);
        }

        // For some reason, it only works on the second element in the list? 
        // If you pass in 0 or FishGod.level (also 0), the sprites don't update
        // when the fish gets hungry. 
        // Also, the order of the sprites in the list is the reverse of what it is
        // in the Inspector. 
        sons[1].SetIsSleeping(false); // Wake current appearance
    }

    public void Eat()
    {
        Debug.Log("EAT");
        ChangeState(FishGod.FishState.Vibing);
        hungerTimer = 0f;
    }
    
    private void AgeUp()
    {
        Debug.Log("AGE UP");
        if (god.GetLevel() < sons.Count) {
            sons[god.GetLevel()].SetIsSleeping(true);
            god.LevelUp();
            sons[god.GetLevel()].SetIsSleeping(false);
        } else {
            god.GameEnd();
        }
    }

    public void ChangeState(FishGod.FishState s) 
    {
        state = s;
        sons[god.GetLevel()].UpdateState(s);

        if (s == FishGod.FishState.Hungry) god.FindNearestFood(transform.position);
    }

    public void SetGod(FishGod g) => god = g;
}