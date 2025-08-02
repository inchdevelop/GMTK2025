using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup inGameUI;
    [SerializeField] CanvasGroup pauseUI;
    [SerializeField] CanvasGroup gameOverUI;
    [SerializeField] TextMeshProUGUI scoreTF;
    [SerializeField] TextMeshProUGUI currentScoreTF;

    [SerializeField] Image[] dashes;
    [SerializeField] Image[] combos;
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseToggle()
    {
        if(pauseUI.alpha == 0f)
            pauseUI.alpha = 1f;
        else
            pauseUI.alpha = 0f;
        pauseUI.interactable = !pauseUI.interactable;
        pauseUI.blocksRaycasts = !pauseUI.blocksRaycasts;
    }

    public void GameOverToggle(int score)
    {
        gameOverUI.alpha = 1.0f;
        gameOverUI.interactable = true;
        gameOverUI.blocksRaycasts = true;

        scoreTF.text = "Your Score: " + score;
    }

    public void DashUI(int dashNum)
    {
        foreach(Image image in dashes)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }
        for(int i = 0; i < dashNum; i++)
        {
            dashes[i].color = new Color(dashes[i].color.r, dashes[i].color.g, dashes[i].color.b, 1f);
        }
    }

    public void ComboUI(float comboNum)
    {
        foreach(Image image in combos)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }

        for(int i = 0; i < combos.Length; i++)
        {
            if( i == comboNum )
            {
                combos[i].color = new Color(combos[i].color.r, combos[i].color.g, combos[i].color.b, 1f);
            }
        }
    }

    public void ScoreUI(int score)
    {
        currentScoreTF.text = score.ToString();
    }

}
