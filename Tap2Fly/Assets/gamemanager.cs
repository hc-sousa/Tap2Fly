using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static SoundManagerScript;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.Reflection;
using TMPro;

public class gamemanager : MonoBehaviour
{
    public GameObject gameOverCanvas, startscreen, tap2play;
    public Button reward, vida,  novavida, retry;
    public Image vidacinza, rewardcinza;
    public adbutton AdButton;
    public GameObject[] obstacles;
    public GameObject InputWindow;
    public TextMeshProUGUI inputField;
    public string theName;

    public void Start()
    {
        gameOverCanvas.SetActive(false);
        tap2play.SetActive(false);
        InputWindow.SetActive(true);
        startscreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void Update()
    {
        if (!(novavida.gameObject.activeInHierarchy))
        {
            if (AdButton.rewardBasedVideoAd.IsLoaded())
            {
                reward.gameObject.SetActive(true);
                rewardcinza.gameObject.SetActive(false);
            }
            else { rewardcinza.gameObject.SetActive(true); reward.gameObject.SetActive(false); }
            if ((PlayerPrefs.GetInt("moedas", 0) > 500)) {
                vidacinza.gameObject.SetActive(false); 
                vida.gameObject.SetActive(true);
            }
            else { vidacinza.gameObject.SetActive(true); vida.gameObject.SetActive(false); }
        } else { rewardcinza.gameObject.SetActive(false); reward.gameObject.SetActive(false); vidacinza.gameObject.SetActive(false); vida.gameObject.SetActive(false); }
    }    
    public void newLife()
    {
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.name == "coliders(Clone)" || obstacle.name == "gears(Clone)") GameObject.Destroy(obstacle);
        }
            
        PlayerPrefs.SetInt("moedas", (PlayerPrefs.GetInt("moedas", 0) - 500));
        vida.gameObject.SetActive(false);
        reward.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        vidacinza.gameObject.SetActive(false);
        rewardcinza.gameObject.SetActive(false);
        novavida.gameObject.SetActive(true);
            
    }
    public void GameOver()
    {
        transform.gameObject.tag = "naojogando";
        retry.gameObject.SetActive(true);
        novavida.gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        transform.gameObject.tag = "jogando";
        novavida.gameObject.SetActive(false);
        startscreen.SetActive(false);
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void SaveScore(){
        theName = inputField.text;
        InputWindow.SetActive(false);
        tap2play.SetActive(true);
    }
}
