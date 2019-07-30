using System.Collections.Generic;
using UnityEngine;

public class IntersectionRoad : Road
{
    [Header("Road Connector")] 
    [SerializeField] private List<Connector> connectorRoad;

    public override List<Road> GetRoads(Road lastRoad)
    {
        foreach (Connector conn in connectorRoad)
        {
            if (conn.comeFrom == lastRoad) return conn.connectRoad;
        }

        return null;
    }
}

[System.Serializable]
public class Connector
{
    public Road comeFrom;
    public List<Road> connectRoad;
}
