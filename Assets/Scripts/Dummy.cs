using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utomo;

public class Dummy : MonoBehaviour
{
    public static bool VisualizeRoadNode;
    public bool visualize;
    

    private void OnDrawGizmos()
    {
        VisualizeRoadNode = visualize;
        
    }

}
