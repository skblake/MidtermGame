using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAppearance : MonoBehaviour
{
    void Activate() 
    {
        gameObject.SetActive(true);
    }

    void Deactivate() 
    {
        gameObject.SetActive(false);
    }

    public void SetIsSleeping(bool awake) {
        if (awake) { Activate();   }
        else       { Deactivate(); }
    }
}