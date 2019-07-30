using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HappyParameter : MonoBehaviour
{
    private float parameter;

    private float angryParameter;
    private float happyParameter;

    [Header("Emot Sprite")] 
    [SerializeField] private Image emot;
    public Sprite happy;
    public Sprite flat;
    public Sprite angry;
    
    public static HappyParameter parameterManager;

    [SerializeField] private GameManagement management;

    [SerializeField] private TextMeshProUGUI parameterText;
    
    // Start is called before the first frame update
    void Start()
    {
        parameterManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables._pauseGame) return;
        parameter = happyParameter - angryParameter;
        parameterText.text = (parameter >= 0 ? ("+" + ((int) parameter)) : (""+(int) parameter));
        ChangeEmot();
        if (CheckLose()) management.Lose();
    }

    public void IncreaseSadParameter(float value)
    {
        angryParameter += value;
    }

    public void IncreaseHappyParameter(float value)
    {
        happyParameter += value > 0 ? value : 0;
        
    }

    public float Percentage(float max)
    {
        return happyParameter / max;
    }

    public void ParameterAccident()
    {
        parameter = -50;
        happyParameter = 0;
        management.Lose();
    }

    bool CheckLose()
    {
        if (parameter < -50)
        {
            GameVariables._pauseGame = true;
            return true;
        }
        return false;
    }

    void ChangeEmot()
    {
        if (parameter >= 0 && parameter < 10) emot.sprite = flat;
        else if (parameter >= 10) emot.sprite = happy;
        else emot.sprite = angry;
    }
}
