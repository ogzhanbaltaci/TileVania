using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int bulletCount = 20;
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bulletCountText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] Canvas ongoingCanvas;
    [SerializeField] Canvas tryAgainCanvas;
    public Canvas winCanvas;
    public bool gameFinished = false;
    public bool outOfBullet = false;
    
    
    void Awake()
    {
        tryAgainCanvas.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(false);
        
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {   
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        bulletCountText.text = bulletCount.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ongoingCanvas.gameObject.SetActive(false);
            tryAgainCanvas.gameObject.SetActive(true);
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void BulletUsed()
    {
        if(bulletCount==1){
            outOfBullet=true;
            bulletCount = 0;
            bulletCountText.text = bulletCount.ToString();
            return;}
        bulletCount--;
        bulletCountText.text = bulletCount.ToString();
    }

    public void BulletPickup()
    {
        if(bulletCount == 0){outOfBullet=false;}
        bulletCount = bulletCount + 3;
        bulletCountText.text = bulletCount.ToString();
    }
    
    public void TotalScore()
    {
        int bulletScore = bulletCount*100;
        int liveScore = playerLives*500;
        int finalScore = score + bulletScore + liveScore;
        finalScoreText.text = "Final Score = " + score.ToString() + " + " + playerLives.ToString() + " x 500 + " + bulletCount.ToString() + " x 100 = " + finalScore.ToString();
        
    }
    
}
