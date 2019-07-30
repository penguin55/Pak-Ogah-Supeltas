using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    [SerializeField] private IntelligenceCarSystem aI;
    [SerializeField] [Range(10,20)] private float baseDelayWaiting = 20;
    [SerializeField] private float delayWaiting;

    [SerializeField] private ParticleSystem particle;

    private bool toggleA1 = false;

    public void Awake()
    {
        delayWaiting = Random.Range(10,baseDelayWaiting);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables._pauseGame) return;
        if (aI.IsStopping()) Waiting();
    }

    void Waiting()
    {
        delayWaiting -= Time.deltaTime;
        if (delayWaiting < 0)
        {
            if (!toggleA1)
            {
                toggleA1 = true;
                StartCoroutine(emotAngry(0));
            }
            HappyParameter.parameterManager.IncreaseSadParameter(Time.deltaTime);
        } else if (delayWaiting < baseDelayWaiting / 4)
        {
            if (!toggleA1)
            {
                toggleA1 = true;
                StartCoroutine(emotAngry(2));
            }
        } else if (delayWaiting < baseDelayWaiting / 2)
        {
            if (!toggleA1)
            {
                toggleA1 = true;
                StartCoroutine(emotAngry(4));
            }
        }
    }

    public float GetDelay()
    {
        return delayWaiting;
    }
    
    public void SetDelay()
    {
        if (delayWaiting > 0) delayWaiting = 0;
    }

    IEnumerator emotAngry(int addTime)
    {
        ParticleSystem par = Instantiate(particle, transform.position, Quaternion.identity);
        par.transform.parent = transform;
        yield return new WaitForSeconds(1);
        AudioManager._audioManager.Play("Horn");
        yield return new WaitForSeconds(4+addTime);
        toggleA1 = false;
    }
}
