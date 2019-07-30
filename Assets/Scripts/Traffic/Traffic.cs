using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    public TrafficManager.TrafficLampStatus trafficLamp;
    private CarBehaviour currentCar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCar && trafficLamp == TrafficManager.TrafficLampStatus.Green)
        {
            currentCar.Drive();
            currentCar = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (trafficLamp == TrafficManager.TrafficLampStatus.Red)
        {
            if (other.CompareTag("Car"))
            {
                currentCar = other.GetComponent<CarBehaviour>();
                currentCar.Stop();
            }
        }
    }
}
