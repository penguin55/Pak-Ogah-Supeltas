using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    
    private int indexCurrent = 0;

    [Header("Tutorial Properties UI")] 
    public Image realImageUI;
    public Image commandImageUI;
    public TextMeshProUGUI textDetail;
    
    [SerializeField] private List<Command> commands;
    
    public void ActiveTutorial()
    {
        tutorialPanel.SetActive(true);
        RenderTutorial();
    }

    public void Next()
    {
        indexCurrent++;
        if (indexCurrent == commands.Count)
        {
            indexCurrent = 0;
            Close();
        }
        else
        {
            RenderTutorial();
        }
    }

    void Close()
    {
        tutorialPanel.SetActive(false);
    }

    void RenderTutorial()
    {
        realImageUI.sprite = commands[indexCurrent].realImage;
        commandImageUI.sprite = commands[indexCurrent].commandImage;
        textDetail.text = commands[indexCurrent].detail;
    }
}

[Serializable]
public class Command
{
    public Sprite realImage;
    public Sprite commandImage;
    [TextArea(10,20)] public string detail;
}
