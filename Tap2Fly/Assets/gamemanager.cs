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
using System.Text.RegularExpressions; // needed for Regex

public class gamemanager : MonoBehaviour
{
    public GameObject gameOverCanvas, startscreen, tap2play, exitChangeUser;
    public Rigidbody2D tappy;
    public Text userInputText;
    public Button reward, vida,  novavida, retry, changeUsername;
    public Image vidacinza, rewardcinza;
    public adbutton AdButton;
    public GameObject[] obstacles;
    public GameObject InputWindow, inputleader, gameMode, inverseMode, powerMode;
    public TextMeshProUGUI inputField;
    public string theName, currentGameMode;
    private string initialString;

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
        if (PlayerPrefs.GetString("gameMode", "classic") == "inverse") clasToInverse();
        else if (PlayerPrefs.GetString("gameMode", "classic") == "power") clasToPower();
        Time.timeScale = 0;
        currentGameMode = PlayerPrefs.GetString("gameMode", "classic");
        initialString = inputField.text;
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
        PlayerPrefs.SetString("gameMode", "classic");
        transform.gameObject.tag = "jogando";
        novavida.gameObject.SetActive(false);
        startscreen.SetActive(false);
        gameOverCanvas.SetActive(false);
        if (currentGameMode == "inverse") playInverse();
        else if (currentGameMode == "power") playPower();
        Time.timeScale = 1;
    }

    public void playInverse(){
        PlayerPrefs.SetString("gameMode", "inverse");
        tappy.gravityScale *= -1;
        inverseMode.SetActive(true);
    }
    public void playPower(){
        PlayerPrefs.SetString("gameMode", "power");
        powerMode.SetActive(true);
    }
    public void SaveUsername(){
        theName = inputField.text;
        if (theName == initialString){
            userInputText.text = "Invalid username";
            userInputText.color = Color.red;  
        }
        else{ 
            PlayerPrefs.SetString("username", theName);
            userInputText.text = "Everyone can see your name!";
            userInputText.color = Color.black;
            InputWindow.SetActive(false);
            tap2play.SetActive(true);
        }
    }
    public void ExitUserInput(){
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
    public void ChangeUsernameAgain(){
        inputleader.SetActive(false);
        tap2play.SetActive(false);
        InputWindow.SetActive(true);
        exitChangeUser.SetActive(true);
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
        GameObject classicModeLogo = gameMode.transform.GetChild(0).gameObject; 
        GameObject powerModeLogo = gameMode.transform.GetChild(8).gameObject;
        GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
        GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
        GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
        GameObject powertoclas = gameMode.transform.GetChild(6).gameObject;
        classicModeLogo.SetActive(false);
        powerModeLogo.SetActive(true);
        clastoinversed.SetActive(false);
        clastopower.SetActive(false);
        powertoinverse.SetActive(true);
        powertoclas.SetActive(true);
        powerMode.SetActive(true);
        currentGameMode = "power";

    }
    public void clasToInverse(){
        GameObject classicModeLogo = gameMode.transform.GetChild(0).gameObject; 
        GameObject inverseModeLogo = gameMode.transform.GetChild(7).gameObject;
        GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
        GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
        GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
        GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
        classicModeLogo.SetActive(false);
        inverseModeLogo.SetActive(true);
        clastoinversed.SetActive(false);
        clastopower.SetActive(false);
        inversetopower.SetActive(true);
        inversetoclas.SetActive(true);
        inverseMode.SetActive(true);
        currentGameMode = "inverse";
    }
    public void powerToClas(){
        GameObject classicModeLogo = gameMode.transform.GetChild(0).gameObject; 
        GameObject powerModeLogo = gameMode.transform.GetChild(8).gameObject; 
        GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
        GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
        GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
        GameObject powertoclas = gameMode.transform.GetChild(6).gameObject; 
        classicModeLogo.SetActive(true);
        powerModeLogo.SetActive(false);
        clastoinversed.SetActive(true);
        clastopower.SetActive(true);
        powertoinverse.SetActive(false);
        powertoclas.SetActive(false);
        powerMode.SetActive(false);
        currentGameMode = "classic";

    }
    public void powerToInverse(){
        GameObject inverseModeLogo = gameMode.transform.GetChild(7).gameObject; 
        GameObject powerModeLogo = gameMode.transform.GetChild(8).gameObject; 
        GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
        GameObject powertoclas = gameMode.transform.GetChild(6).gameObject; 
        GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
        GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
        inverseModeLogo.SetActive(true);
        powerModeLogo.SetActive(false);
        inversetoclas.SetActive(true);
        inversetopower.SetActive(true);
        powertoinverse.SetActive(false);
        powertoclas.SetActive(false);
        inverseMode.SetActive(true);
        powerMode.SetActive(false);
        currentGameMode = "inverse";
    }
    public void inverseToClas(){
        GameObject classicModeLogo = gameMode.transform.GetChild(0).gameObject; 
        GameObject inverseModeLogo = gameMode.transform.GetChild(7).gameObject;
        GameObject clastoinversed = gameMode.transform.GetChild(4).gameObject; 
        GameObject clastopower = gameMode.transform.GetChild(1).gameObject; 
        GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
        GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
        classicModeLogo.SetActive(true);
        inverseModeLogo.SetActive(false);
        clastoinversed.SetActive(true);
        clastopower.SetActive(true);
        inversetopower.SetActive(false);
        inversetoclas.SetActive(false);
        inverseMode.SetActive(false);
        currentGameMode = "classic";
    }
    public void inverseToPower(){
        GameObject inverseModeLogo = gameMode.transform.GetChild(7).gameObject; 
        GameObject powerModeLogo = gameMode.transform.GetChild(8).gameObject; 
        GameObject powertoinverse = gameMode.transform.GetChild(2).gameObject; 
        GameObject powertoclas = gameMode.transform.GetChild(6).gameObject; 
        GameObject inversetoclas = gameMode.transform.GetChild(3).gameObject; 
        GameObject inversetopower = gameMode.transform.GetChild(5).gameObject; 
        inverseModeLogo.SetActive(false);
        powerModeLogo.SetActive(true);
        inversetoclas.SetActive(false);
        inversetopower.SetActive(false);
        powertoinverse.SetActive(true);
        powertoclas.SetActive(true);
        inverseMode.SetActive(false);
        powerMode.SetActive(true);
        currentGameMode = "power"; 
    }
}
