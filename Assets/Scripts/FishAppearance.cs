using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAppearance : MonoBehaviour
{
    //// ASSIGN IN INSPECTOR ////
    public Sprite vibeSprite;
    public Sprite hungrySprite;
    /////////////////////////////

    private FishGod god;

    // Connects this appearance to FishGod and puts it to sleep
    public void LinkAndSleep(FishGod g)
    {
        god = g;
        SetIsSleeping(true);
    }

    void Activate()   => gameObject.SetActive(true );
    void Deactivate() => gameObject.SetActive(false);

    public void SetIsSleeping(bool awake) {
        if (awake) { Activate();   }
        else       { Deactivate(); }
    }

    public void UpdateState(FishGod.FishState s) 
    {
        switch (s) {
            case (FishGod.FishState.Vibing):
                GetComponent<SpriteRenderer>().sprite = vibeSprite;
            break;
            case (FishGod.FishState.Hungry):
                GetComponent<SpriteRenderer>().sprite = hungrySprite;
            break;
        }
    }
}