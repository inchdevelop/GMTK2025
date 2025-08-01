using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int totalScore;

    [SerializeField] int scoreMult;
    [SerializeField] int maxMult;
    [SerializeField] float multDecreaseTime;
    [SerializeField] float currentComboTime;
    [SerializeField] bool comboUp;

     bool isPaused = false;

    [SerializeField] int numDashes;
    [SerializeField] int maxDashes;
    public static GameManager instance;

    [SerializeField] public int maxSheep = 20;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        playerInput.onPlayerDash += PlayerDash;
        playerInput.onPlayerDash += MultIncrease;
        playerInput.onPlayerPause += PlayerPause;

        Sheep.onSheepCollide += PlayerDash;
        Sheep.onSheepKnockUp += SheepKnockedUp;

        Dogbowl.onDashRecovery += DashRecovery;
        
        SheepManager.onGameOver += GameOver;
    }

    private void OnDisable()
    {
        playerInput.onPlayerDash -= PlayerDash;
        playerInput.onPlayerDash -= MultIncrease;
        playerInput.onPlayerPause -= PlayerPause;
        
        Sheep.onSheepCollide -= PlayerDash;
        Sheep.onSheepKnockUp -= SheepKnockedUp;

        Dogbowl.onDashRecovery -= DashRecovery;

        SheepManager.onGameOver -= GameOver;
    }

    private void Update()
    {
        if(comboUp)
        {
            currentComboTime += Time.deltaTime;
            if(currentComboTime >= multDecreaseTime)
            {
                comboUp = false;
                currentComboTime = 0;
                MultReset();
            }
        }
    }

    public bool CheckDash()
    {
        return numDashes > 0;
    }

    void PlayerDash()
    {
        numDashes--;
        if(numDashes <= 0)
            numDashes = 0;
        UIManager.instance.DashUI(numDashes);
        
    }

    void MultIncrease()
    {
        scoreMult *= 2;
        if(scoreMult > maxMult)
            scoreMult = maxMult;
        UIManager.instance.ComboUI(Mathf.Log(scoreMult, 2) - 1);
        comboUp = true;
        currentComboTime = 0f;
    }

    void MultReset()
    {
        scoreMult = 1;
        UIManager.instance.ComboUI(Mathf.Log(scoreMult, 2) -1);
    }

    public void DashRecovery()
    {
        numDashes++;
        if(numDashes > maxDashes)
            numDashes = maxDashes;
        UIManager.instance.DashUI(numDashes);
    }

    public void PlayerPause()
    {
        isPaused = !isPaused;
        if(isPaused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;

        UIManager.instance.PauseToggle();
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        UIManager.instance.GameOverToggle(totalScore);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("CombinedScene");
        Time.timeScale = 1.0f;
    }

    public void SheepKnockedUp(GameObject sheep)
    {
        SheepManager.instance.DeleteSheep(sheep);
    }
}
