using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;


public class SkyColour : MonoBehaviour
{
    public healthSystem healthSystem;

    public Volume red;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        red.weight = 1f-healthSystem.GetHealthAmountNormalized();
    }
}
