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
    public Button reward, vida,  novavida, retry, changeUsername;
    public Image vidacinza, rewardcinza;
    public adbutton AdButton;
    public GameObject[] obstacles;
    public GameObject InputWindow, inputleader, gameMode;
    public TextMeshProUGUI inputField;
    public string theName, currentGameMode;

    public void Start()
    {
        gameOverCanvas.SetActive(false);
        if (PlayerPrefs.GetString("username", "ghfdjkshguifdhs123") == "ghfdjkshguifdhs123"){
            tap2play.SetActive(false);
            InputWindow.SetActive(true);
        }
        else
        {
            tap2play.SetActive(true);
            InputWindow.SetActive(false);
        }
        startscreen.SetActive(true);
        Time.timeScale = 0;
        currentGameMode = "classic";
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
        inputleader.SetActive(true);
        SaveScore(Score.score);
        inputleader.SetActive(false);
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
    public void SaveUsername(){
        theName = inputField.text;
        PlayerPrefs.SetString("username", theName);
        InputWindow.SetActive(false);
        tap2play.SetActive(true);
    }
    public void SaveScore(int score){
        inputleader.GetComponent<leaderboard>().AddNewHighscore(PlayerPrefs.GetString("username"), score);
    }
    public void ShowLeaderboard(){
        tap2play.SetActive(false);
        inputleader.SetActive(true);
        inputleader.GetComponent<leaderboard>().GetLeaderboard();
    }
    public void ChangeUsername(){
        //TODO: Delete old name highscore and add the new one
        inputleader.SetActive(false);
        tap2play.SetActive(false);
        InputWindow.SetActive(true);
    }
    public void exitLeaderboard(){
        inputleader.SetActive(false);
        tap2play.SetActive(true);
    }
    /*
    GameObject classicMode = gameMode.transform.GetChild(0).gameObject; 
    GameObject inverseMode = gameMode.transform.GetChild(7).gameObject; 
    GameObject powerMode = gameMode.transform.GetChild(8).gameObject; 
    GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
    GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
    GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
    GameObject powertoclas = gameMode.transform.GetChild(6).gameObject; 
    GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
    GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
    */
    public void clasToPower(){    
        GameObject classicMode = gameMode.transform.GetChild(0).gameObject; 
        GameObject powerMode = gameMode.transform.GetChild(8).gameObject;
        GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
        GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
        GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
        GameObject powertoclas = gameMode.transform.GetChild(6).gameObject;
        classicMode.SetActive(false);
        powerMode.SetActive(true);
        clastoinversed.SetActive(false);
        clastopower.SetActive(false);
        powertoinverse.SetActive(true);
        powertoclas.SetActive(true);
        currentGameMode = "power";

    }
    public void clasToInverse(){
        GameObject classicMode = gameMode.transform.GetChild(0).gameObject; 
        GameObject inverseMode = gameMode.transform.GetChild(7).gameObject;
        GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
        GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
        GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
        GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
        classicMode.SetActive(false);
        inverseMode.SetActive(true);
        clastoinversed.SetActive(false);
        clastopower.SetActive(false);
        inversetopower.SetActive(true);
        inversetoclas.SetActive(true);
        currentGameMode = "inverse";
    }
    public void powerToClas(){
        GameObject classicMode = gameMode.transform.GetChild(0).gameObject; 
        GameObject powerMode = gameMode.transform.GetChild(8).gameObject; 
        GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
        GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
        GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
        GameObject powertoclas = gameMode.transform.GetChild(6).gameObject; 
        classicMode.SetActive(true);
        powerMode.SetActive(false);
        clastoinversed.SetActive(true);
        clastopower.SetActive(true);
        powertoinverse.SetActive(false);
        powertoclas.SetActive(false);
        currentGameMode = "classic";

    }
    public void powerToInverse(){
        GameObject inverseMode = gameMode.transform.GetChild(7).gameObject; 
        GameObject powerMode = gameMode.transform.GetChild(8).gameObject; 
        GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
        GameObject powertoclas = gameMode.transform.GetChild(6).gameObject; 
        GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
        GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
        inverseMode.SetActive(true);
        powerMode.SetActive(false);
        inversetoclas.SetActive(true);
        inversetopower.SetActive(true);
        powertoinverse.SetActive(false);
        powertoclas.SetActive(false);
        currentGameMode = "inverse";
    }
    public void inverseToClas(){
        GameObject classicMode = gameMode.transform.GetChild(0).gameObject; 
        GameObject inverseMode = gameMode.transform.GetChild(7).gameObject;
        GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
        GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
        GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
        GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
        classicMode.SetActive(true);
        inverseMode.SetActive(false);
        clastoinversed.SetActive(true);
        clastopower.SetActive(true);
        inversetopower.SetActive(false);
        inversetoclas.SetActive(false);
        currentGameMode = "classic";

    }
    public void inverseToPower(){
        GameObject inverseMode = gameMode.transform.GetChild(7).gameObject; 
        GameObject powerMode = gameMode.transform.GetChild(8).gameObject; 
        GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
        GameObject powertoclas = gameMode.transform.GetChild(6).gameObject; 
        GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
        GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
        inverseMode.SetActive(false);
        powerMode.SetActive(true);
        inversetoclas.SetActive(false);
        inversetopower.SetActive(false);
        powertoinverse.SetActive(true);
        powertoclas.SetActive(true);
        currentGameMode = "power"; 
    }
}
