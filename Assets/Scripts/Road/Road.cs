using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Road : MonoBehaviour
{
    public enum RoadType { StraightRoad, IntersectionRoad };
    public RoadType typeOfRoad;
    public virtual Road GetNextRoad() { return null; }
    public virtual Vector2 GetRoadPosition() { return Vector2.zero; }
    public virtual List<Road> GetRoads(Road lastRoad) { return null; }
    
}
