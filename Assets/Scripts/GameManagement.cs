using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public static GameManagement manager;

    [SerializeField] private TextMeshProUGUI textIntro;

    [SerializeField] private FadeManager fading;

    private float maxScoreHappines;
    
    [SerializeField] private int carArrived;

    private int maxArrived;

    [Header("Win Properties")] 
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite starsGet;
    [SerializeField] private Sprite starsDontGet;
    private int maxStars;
    
    [Header("Lose Properties")] 
    [SerializeField] private GameObject losePanel;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = this;
        GameVariables._pauseGame = true;
        maxArrived = CarSpawnerManager.spawnManager.carMaxSpawn;
        textIntro.text = "x" + maxArrived;
        fading.FadeIn();
    }

    public void StartTheGame()
    {
        GameVariables._pauseGame = false;
    }

    public void Happiness(float value)
    {
        maxScoreHappines += value;
    }

    public void Arrived()
    {
        carArrived++;
        if (carArrived == maxArrived)
        {
            GameVariables._pauseGame = true;
            RenderStars();
            winPanel.SetActive(true);
        }
    }

    public void RenderStars()
    {
        if (HappyParameter.parameterManager.Percentage(maxScoreHappines) > 0.6f) maxStars = 3;
        else if (HappyParameter.parameterManager.Percentage(maxScoreHappines) > 0.4f) maxStars = 2;
        else if (HappyParameter.parameterManager.Percentage(maxScoreHappines) > 0.2f) maxStars = 1;
        else maxStars = 0;

        for (int i = 0; i < 3; i++)
        {
            if (i < maxStars) stars[i].sprite = starsGet;
            else stars[i].sprite = starsDontGet;
        }

        LevelManagement.levels[LevelManagement.levelCurrent].stars = maxStars;
        LevelManagement.levelManager.Save();
    }

    public void Lose()
    {
        losePanel.SetActive(true);
    }
}
