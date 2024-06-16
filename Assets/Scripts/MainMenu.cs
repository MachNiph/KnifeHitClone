using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite[] images;

    private void Start()
    {
        int knifeIndex = PlayerPrefs.GetInt("knifeIndex", 0);
        if (knifeIndex >= 0 && knifeIndex < images.Length)
        {
            image.sprite = images[knifeIndex];
        }
        else
        {
            Debug.LogWarning("Invalid knife index: " + knifeIndex);
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToKnifeSelect()
    {
        SceneManager.LoadScene("KnifeSelect");
    }

    
  
}
