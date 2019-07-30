using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRoad : Road
{
    [Header("Road Object")]
    [SerializeField] private Road nextRoad;

    public override Road GetNextRoad()
    {
        return nextRoad;
    }

    public override Vector2 GetRoadPosition()
    {
        return transform.position;
    }
    
    private void OnDrawGizmos()
    {
        if (Dummy.VisualizeRoadNode)
        {
            if (nextRoad)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere((transform.position + nextRoad.transform.position) / 2,0.2f);
                Gizmos.color = Color.green;
            }
            else Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position,0.4f);
        }
    }
}
