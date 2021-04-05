using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGod : MonoBehaviour
{
    // ASSIGN IN INSPECTOR
    public Food foodPrefab;
    public Fish fish;
    public List<FishAppearance> fishPrefabs = new List<FishAppearance>();

    public int maxFood = 5;

    private Camera cam; 
    
    private List<Food> readyFoods = new List<Food>();
    private List<Food> activeFoods = new List<Food>();
    
    private List<FishAppearance> evolutions = new List<FishAppearance>();
    
    private int level = 0; //TODO: Make this a property with getter/setter
    private int gameEnd;

    void Start() 
    {
        cam = GetComponent<Camera>();

        fish.SetGod(this);
        fish.MakeSons(fishPrefabs);
        gameEnd = fishPrefabs.Count;

        ScrambleFish();

        for (int i = 0; i < maxFood; i++) {
            Food newFood = GameObject.Instantiate(foodPrefab);
            newFood.SetGod(this);
            readyFoods.Add(newFood);
        }
    }

    void Update()
    {
        // If I CLICK, feed fish.
        if(Input.GetMouseButtonDown(0) && readyFoods.Count > 0) {
            Food newFood = readyFoods[0];
            newFood.Activate(cam.ScreenToWorldPoint(Input.mousePosition));
            // fish.SetDestination(newFood);
        }

        // If I press SPACEBAR, tell all fish to randomly swim somewhere else.
        if (Input.GetKeyDown(KeyCode.Space) || fish.needsDest) ScrambleFish();
    }

    void ScrambleFish() 
    {
        fish.SetDestination(GetRandomPos());
        fish.needsDest = false;
    }

    // Manages lists of available and active foods
    public void UpdateFood(Food food, bool isActive)
    {
        if (isActive) {
            if ( readyFoods .Contains(food)) readyFoods .Remove(food);
            if (!activeFoods.Contains(food)) activeFoods.Add(food);
        } else {
            if (!readyFoods .Contains(food)) readyFoods .Add(food);
            if ( activeFoods.Contains(food)) activeFoods.Remove(food);
        }
    }
    
    /*
    public void AgeUp()
    {
        if (level < fish.sons.Count) {
            fish.sons[level].SetIsSleeping(true);
            LevelUp();
            fish.sons[level].SetIsSleeping(false);
        }
    }*/
    
    public void LevelUp() => level++;
    public int GetLevel() => level;

    public Color MakeColor (float r, float g, float b) => new Color (r/255, g/255, b/255);
    private Vector2 GetRandomPos() => new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
}