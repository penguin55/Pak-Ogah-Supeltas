using System;
using System.Collections.Generic;
using UnityEngine;
using Utomo;

public class IntelligenceCarSystem : CarBehaviour
{
    private Transform directionAhead = null;

    /*[SerializeField] private Transform frontCarChecking;
    [SerializeField] private LayerMask checkingMask;*/

    #region Intersection Road Properties
    public float baseMultiply;
    private float multiply;
    private Vector2 controlPoint;
    
    public List<Vector2> points = new List<Vector2>();

    private Vector2 direction;
    private bool moveCurves = false;
    private float baseDegree;
    private float degree;
    private int pointCount;
    #endregion

    /*private Collider2D obj;*/

    public void StartDrive()
    {
        StartEngine();
        Collider2D obj = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Road"));
        direction = obj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables._pauseGame) return;
        if (stop) return;
        if (moveCurves)
        {
            CurveDriving();
        }
        else
        {
            MoveTo(direction);
            CheckDirection();
        }
        
        /*CheckingWhatIsFrontOfCar();*/
    }

    public void SetDirectionAhead(Transform direction)
    {
        directionAhead = direction;
    }

    private void CheckDirection()
    {
        if ((Vector2) transform.position == direction && direction != (Vector2) directionAhead.position )
        {
            NextRoad();
            if (currentRoad) direction = currentRoad.GetRoadPosition();
        }

        if (direction == (Vector2) directionAhead.position)
        {
            Stop();
            transform.localPosition = Vector3.zero;
            startEngine = false;
            GetComponent<Collider2D>().enabled = false;
            HappyParameter.parameterManager.IncreaseHappyParameter(status.GetDelay());
            GameManagement.manager.Arrived();
        }
    }

    private void NextRoad()
    {
        lastRoad = currentRoad;
        try
        {
            if (currentRoad.typeOfRoad == Road.RoadType.StraightRoad)
            {
                currentRoad = currentRoad.GetNextRoad();
            }

            if (currentRoad.typeOfRoad == Road.RoadType.IntersectionRoad)
            {
                currentRoad = DecideNextRoadOnIntersection(currentRoad.GetRoads(lastRoad));
                moveCurves = true;
                FindCurvePath();
            }
        } catch (NullReferenceException nullReff)
        {
            direction = directionAhead.position;
        }
    }

    #region Moving in Intersection Road
    private Road DecideNextRoadOnIntersection(List<Road> roads)
    {
        float distance = int.MaxValue;
        Road temp = null;
        foreach (Road road in roads)
        {
            if (distance > Vector2.Distance(directionAhead.position, road.transform.position))
            {
                distance = Vector2.Distance(directionAhead.position, road.transform.position);
                temp = road;
            }
        }

        return temp;
    }

    void CurveDriving()
    {
        FacingDirection(((pointCount - points.Count) / (float) pointCount) * degree);
        if (points.Count == 0) moveCurves = false;
        else
        {
            MoveTo(points[0]);
            if ((Vector2) transform.position == points[0]) points.RemoveAt(0);
        }
    }

    void FindCurvePath()
    {
        FindControlPosition(lastRoad.GetRoadPosition(), currentRoad.GetRoadPosition());
        Bezier.BezierCurve(ref points, lastRoad.GetRoadPosition(), currentRoad.GetRoadPosition(), controlPoint);
        pointCount = points.Count;
    }
    
    void FindControlPosition(Vector2 startPoint, Vector2 destinationPoint)
    {
        degree = UtoMath.GetDegreeFromRadiantSection(startPoint, destinationPoint);

        baseDegree = transform.eulerAngles.z;
        multiply = baseMultiply;
        multiply *= UtoMath.CheckRadiantGroup(startPoint, destinationPoint);
        Vector2 multiplier = new Vector2(UtoMath.GetSign(destinationPoint.x) * multiply , UtoMath.GetSign(startPoint.y) * multiply);

        if (UtoMath.GetRadiantSection(startPoint) % 2 == 0)
        {
            controlPoint =
                new Vector2(destinationPoint.x, startPoint.y) + multiplier;
        }
        else
        {
            controlPoint =
                new Vector2(startPoint.x,destinationPoint.y) + multiplier;
        }
    }

    void FacingDirection(float degree)
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, baseDegree + degree);
    }
    #endregion

    /*private void CheckingWhatIsFrontOfCar()
    {
        obj = Physics2D.OverlapCircle(frontCarChecking.position, 0.1f, checkingMask);
        
        speed = baseSpeed;
        if (obj)
        {
            if (obj.CompareTag("Car"))
            {
                speed = 0;
            }
        }
    }*/
    
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        foreach (Vector2 pos in points)
        {
            Gizmos.DrawSphere(pos, 0.2f);
        }

        try
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(lastRoad.GetRoadPosition(), 0.4f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(currentRoad.GetRoadPosition(), 0.4f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(controlPoint, 0.4f);
        }
        catch (NullReferenceException na)
        {
            
        }
        
        /*Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(frontCarChecking.position, 0.1f);#1#
    }*/

    public float GetStatusDelay()
    {
        return status.GetDelay();
    }
}
