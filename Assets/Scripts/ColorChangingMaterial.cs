using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingMaterial : MonoBehaviour
{

    [SerializeField] private Renderer myObject;
    public bool changeColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (changeColor == true)
        {
            myObject.material.color = Color.red;
        }
        else
        {
            myObject.material.color = Color.green;
        }
    }
}
