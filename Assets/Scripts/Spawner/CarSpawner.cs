using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] [Range(1,4)] private float minSpeed;
    [SerializeField] [Range(1,4)] private float maxSpeed;
    
    [SerializeField] private Vector3 rotate;
    
    [SerializeField] private Transform[] direction;
    
    private Stack<IntelligenceCarSystem> carStack = new Stack<IntelligenceCarSystem>();

    private float randomTime;
    private int randomIndex;

    private bool canSpawn = false;

    [SerializeField] private bool nexSpawn = false;

    public void Spawn()
    {
        canSpawn = true;
        nexSpawn = true;
    }

    private void Update()
    {
        if (GameVariables._pauseGame) return;
        if (!nexSpawn) return;
        if (carStack.Count > 0 && canSpawn)
        {
            canSpawn = false;
            randomTime = Random.Range(0.5f, 2f);
            StartCoroutine(delaySpawn());
        }
    }

    public void AddStack(IntelligenceCarSystem car)
    {
        carStack.Push(car);
    }

    IEnumerator delaySpawn()
    {
        yield return new WaitForSeconds(randomTime);
        if (!nexSpawn || GameVariables._pauseGame) yield return null;
        else
        {
            IntelligenceCarSystem car = carStack.Pop();
            car.transform.position = transform.position;
            car.transform.rotation = Quaternion.Euler(rotate);
            GameManagement.manager.Happiness(car.GetStatusDelay());
            car.SetSpeed(minSpeed, maxSpeed);

            randomIndex = Random.Range(0, direction.Length);
            car.SetDirectionAhead(direction[randomIndex]);

            car.StartDrive();
        }
        canSpawn = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            nexSpawn = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            nexSpawn = true;
        }
    }
}
