using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FishGod god; 
    public float driftSpeed = 2f;

    private List<Fish> chasers = new List<Fish>();
    private bool eaten = false;

    public void Activate(Vector2 pos)
    {
        gameObject.SetActive(true);
        god.UpdateFood(this, true);
        transform.position = pos;
    }

    void Update()
    {
        // DRIFT DOWNWARDS
        transform.position += new Vector3(0f, -driftSpeed, 0f) * Time.deltaTime;

        // -5.5f is the bottom of the screen in world space.
        if (transform.position.y < -5.5f) Deactivate();
    }

    void BeEatenBy(Fish eater) 
    {
        eater.Eat();
        
        Deactivate();
    }

    // Resets variables to default values.
    void Deactivate()
    {
        // If fish is chasing this food, flag it to have its destination reset
        foreach (Fish fish in chasers) fish.needsDest = true;

        chasers = new List<Fish>();
        eaten = false;
        god.UpdateFood(this, false);
        gameObject.SetActive(false);
    }

    public void AddChaser(Fish fish) => chasers.Add(fish);
}