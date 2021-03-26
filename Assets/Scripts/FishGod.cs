using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGod : MonoBehaviour
{
    public List<Fish> fishPrefabs = new List<Fish>(); 
    List<Fish> fishList = new List<Fish>();

    void Start() 
    {
        
    }

    void Update()
    {
        // If I CLICK, spawn a new fish.

        // If I press SPACEBAR, tell all fish to randomly swim somewhere else.
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (Fish fish in fishList)
                fish.destination = getRandomPos();
        }
    }
    public Color makeColor (float r, float g, float b) => new Color (r/255, g/255, b/255);
    private Vector2 getRandomPos() => new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
}