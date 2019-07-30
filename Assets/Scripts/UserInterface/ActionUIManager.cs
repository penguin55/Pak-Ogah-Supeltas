using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUIManager : MonoBehaviour
{
    [Header("Action UI")] 
    [SerializeField] private Image[] actionImageUI;
    [SerializeField] private Action[] action;
    
    [Header("Traffic")] 
    [SerializeField] private Traffic[] traffic;
    
    // Start is called before the first frame update
    void Start()
    {
        RenderActionImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RenderActionImage()
    {
        for (int i = 0; i < action.Length; i++)
        {
            actionImageUI[i].sprite = action[i].actionImage;
        }
    }

    public void Use(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (action[index].actionEffectDirection[i])
            {
                traffic[i].trafficLamp = action[index].trafficStatus;
            }
        }
    }
}
