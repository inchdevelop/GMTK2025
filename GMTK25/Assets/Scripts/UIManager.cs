using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup inGameUI;
    [SerializeField] CanvasGroup pauseUI;
    [SerializeField] CanvasGroup gameOverUI;
    [SerializeField] TextMeshProUGUI scoreTF;
    [SerializeField] TextMeshProUGUI currentScoreTF;
    [SerializeField] TextMeshProUGUI dashesLeft;
    [SerializeField] Animator currentScoreAnimator;

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
      
            dashesLeft.text = "x" + dashNum.ToString();
            Debug.Log(dashesLeft.text);
        
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
        currentScoreAnimator.Play("ScoreIncrease");
        currentScoreTF.text = score.ToString();
    }

}
