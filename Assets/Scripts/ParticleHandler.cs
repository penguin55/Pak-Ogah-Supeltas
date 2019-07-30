using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    // Update is called once per frame
    void Update()
    {
        if (particle.IsAlive()) return;
        Destroy(gameObject);
    }
}
