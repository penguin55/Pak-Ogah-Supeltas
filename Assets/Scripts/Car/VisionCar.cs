using UnityEngine;

public class VisionCar : MonoBehaviour
{ 
    [Range(0, 5)] private float range = 2;
    [Range(0, 1)] private float offsetRange = 0.3f;
    [SerializeField] private IntelligenceCarSystem aI;
    private GameObject car;

    private float distance;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            car = other.gameObject;
        }
    }

    private void Update()
    {
        ManageSpeed();
    }

    void ManageSpeed()
    {
        if (car == null) return;
        distance = Vector2.Distance(car.transform.position, transform.position);
        if (distance >= offsetRange && distance <= range)
        {
            aI.ChangeSpeed(distance/range);
        }
        else if (distance > range)
        {
            aI.ChangeSpeed(1);
            car = null;
        } else if (distance < offsetRange)
        {
            aI.ChangeSpeed(0);
        }
    }
}
