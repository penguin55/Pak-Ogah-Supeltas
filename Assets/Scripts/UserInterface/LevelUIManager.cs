using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private List<LevelUIStar> levelUI = new List<LevelUIStar>(6);

    [SerializeField] private Sprite starActive;
    [SerializeField] private Sprite starInActive;
    
    // Start is called before the first frame update
    void Start()
    {
        RenderLevel();
    }

    void RenderLevel()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j < LevelManagement.levels[i].stars) levelUI[i].imageStar[j].sprite = starActive;
                else levelUI[i].imageStar[j].sprite = starInActive;
            }
        }
    }
}

[Serializable]
public class LevelUIStar
{
    public string name;
    public Image[] imageStar;
}

