using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FishGod god; 

    private List<Fish> chasers = new List<Fish>();
    private bool eaten = false;

    public void Activate()
    {
        gameObject.SetActive(true);
        god.UpdateFood(this, true);
        transform.position = Input.mousePosition;
    }

    void BeEatenBy(Fish eater) 
    {
        eater.Eat();

        // Flag the fish still chasing this food to have their destinations reset
        foreach (Fish fish in chasers)
            fish.needsDest = true;
        
        Deactivate();
    }

    // Resets variables to default values.
    void Deactivate()
    {
        chasers = new List<Fish>();
        eaten = false;
        god.UpdateFood(this, false);
        gameObject.SetActive(false);
    }

    public void AddChaser(Fish fish) => chasers.Add(fish);
}