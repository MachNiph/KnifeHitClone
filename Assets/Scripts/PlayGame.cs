using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI levelText;

    private void Start()
    {
        if(scoreText != null)
        {
            int gameScore = PlayerPrefs.GetInt("Score");
            scoreText.text = gameScore.ToString();
        }

        if(levelText != null)
        {
            int level = PlayerPrefs.GetInt("Level");
            levelText.text = level.ToString();
        }
      
    }
    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
    }



}
