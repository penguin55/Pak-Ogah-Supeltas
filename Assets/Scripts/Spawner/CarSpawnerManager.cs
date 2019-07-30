using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerManager : MonoBehaviour
{

    public static CarSpawnerManager spawnManager;
    
    [SerializeField] private List<IntelligenceCarSystem> cars = new List<IntelligenceCarSystem>();
    
    private Stack<IntelligenceCarSystem> carStack = new Stack<IntelligenceCarSystem>();

    [SerializeField] private CarSpawner[] spawnLocation;

    public int carMaxSpawn;
    private int carSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = this;
        Shuffle();
        Stacked();
    }

    void Shuffle()
    {
        foreach (IntelligenceCarSystem car in cars)
        {
            carStack.Push(car);
            carSpawn++;
            
            if (carSpawn == carMaxSpawn) return;
        }
        
        if (carSpawn < carMaxSpawn) Shuffle();
    }

    void Stacked()
    {
        foreach (CarSpawner spawn in spawnLocation)
        {
            if (carStack.Count > 0) spawn.AddStack(carStack.Pop());
        }

        if (carStack.Count > 0) Stacked();
        else
        {
            foreach (CarSpawner spawn in spawnLocation)
            {
                spawn.Spawn();
            }
        }
    }
}
