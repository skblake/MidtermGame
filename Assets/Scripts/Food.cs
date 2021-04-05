using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float driftSpeed = 2f;
    public bool chased = false;

    private FishGod god; 
    private bool eaten = false;

    public void Activate(Vector2 pos)
    {
        gameObject.SetActive(true);
        god.UpdateFood(this, true);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D activator)
    {
        if (activator.GetComponent<FishAppearance>()!= null) BeEatenBy(god.fish);
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
        if (chased) god.fish.needsDest = true;

        eaten = chased = false;
        god.UpdateFood(this, false);
        gameObject.SetActive(false);
    }

    // public void AddChaser(Fish fish) => chasers.Add(fish);
    public void SetGod(FishGod g) => god = g;
}