using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
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

    [SerializeField] float difficultyIncrease;
    [SerializeField] float difficultyThreshold;
    [SerializeField] float difficultyRate;
    [SerializeField] float difficultyTimer;


     bool isPaused = false;

    [SerializeField] int numDashes;
    [SerializeField] int maxDashes;

    [Header("Sounds")]
    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioClip loopSound;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip dashRecoverSound;
    [SerializeField] AudioClip sheepHitSound;
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
        playerInput.onPlayerPause += PlayerPause;
        playerInput.onPlayerDash += PlayDash;

        Sheep.onSheepCollide += PlayerDash;
        Sheep.onSheepCollide += PlaySheepHit;
        Sheep.onSheepKnockUp += SheepKnockedUp;

        Dogbowl.onDashRecovery += DashRecovery;
        Dogbowl.onDashRecovery += PlayWaterbowl;
        
        SheepManager.onGameOver += GameOver;
        SheepManager.onGameOver += PlayGameOver;

        lassoDetect.onSheepCollected += ScoreIncrease;
        lassoDetect.onLassoCollect += MultIncrease;
        lassoDetect.onLassoCollect += PlayLoop;
    }

    private void OnDisable()
    {
        playerInput.onPlayerDash -= PlayerDash;
        playerInput.onPlayerPause -= PlayerPause;
        playerInput.onPlayerDash -= PlayDash;
        
        Sheep.onSheepCollide -= PlayerDash;
        Sheep.onSheepCollide-= PlaySheepHit;
        Sheep.onSheepKnockUp -= SheepKnockedUp;

        Dogbowl.onDashRecovery -= DashRecovery;
        Dogbowl.onDashRecovery -= PlayWaterbowl;

        SheepManager.onGameOver -= GameOver;
        SheepManager.onGameOver -= PlayGameOver;

        lassoDetect.onSheepCollected -= ScoreIncrease;
        lassoDetect.onLassoCollect -= MultIncrease;
        lassoDetect.onLassoCollect -= PlayLoop;
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

        difficultyTimer += Time.deltaTime * difficultyRate;
        if(difficultyTimer >= difficultyThreshold)
        {
            SheepManager.instance.spawnInterval -= difficultyIncrease;
            if (SheepManager.instance.spawnInterval <= SheepManager.instance.minSpawnInterval)
                SheepManager.instance.spawnInterval = SheepManager.instance.minSpawnInterval;
            difficultyTimer = 0;
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

    void ScoreIncrease(int score)
    {
        totalScore += score * scoreMult;
        UIManager.instance.ScoreUI(totalScore);
    }



    void MultIncrease()
    {
        Debug.Log("comboing");
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

    void PlayDash()
    {
        AudioSource.PlayClipAtPoint(dashSound, Camera.main.transform.position);
    }

    void PlayWaterbowl()
    {
        AudioSource.PlayClipAtPoint(dashRecoverSound, Camera.main.transform.position);
    }

    void PlayGameOver()
    {
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
    }

    void PlayLoop()
    {
        AudioSource.PlayClipAtPoint(loopSound, Camera.main.transform.position);
    }

    void PlaySheepHit()
    {
        AudioSource.PlayClipAtPoint(sheepHitSound, Camera.main.transform.position);
    }
}
