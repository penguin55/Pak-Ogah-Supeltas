using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleAngry;
    
    [SerializeField] protected CarStatus status;
    
    protected Road currentRoad;
    protected Road lastRoad;

    public float baseSpeed;
    protected float speed = 0;

    protected bool stop = true;
    protected bool startEngine = false;

    protected void StartEngine()
    {
        GetComponent<Collider2D>().enabled = true;
        currentRoad = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Road")).GetComponentInParent<Road>();
        speed = baseSpeed;
        stop = false;
        startEngine = true;
    }

    protected void MoveTo(Vector2 direction)
    {
        //transform.Translate(speed * direction * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }

    public void Stop()
    {
        stop = true;
    }

    public void Drive()
    {
        stop = false;
    }

    public void ChangeSpeed(float percentage)
    {
        speed = percentage * baseSpeed;
    }

    public bool IsStopping()
    {
        return startEngine && (speed == 0 || stop);
    }

    public void SetSpeed(float minSpeed, float maxSpeed)
    {
        baseSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Stop();
            ChangeSpeed(0);
            GameVariables._pauseGame = true;
            AudioManager._audioManager.Play("Crash");
            status.SetDelay();
            ParticleSystem par = Instantiate(particleAngry, transform.position, Quaternion.identity);
            par.transform.parent = transform;
            HappyParameter.parameterManager.ParameterAccident();
        }
    }
}
