using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTowerCollisions : MonoBehaviour
{
    public bool obstructed;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered trigger");
       obstructed = true;
    }

    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exited trigger");
        obstructed = false;
    }
}
