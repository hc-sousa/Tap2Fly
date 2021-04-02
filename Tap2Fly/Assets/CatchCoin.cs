using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchCoin : MonoBehaviour
{
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision){
            Destroy(coin);
            Score.score++;
            PlayerPrefs.SetInt("moedas", (PlayerPrefs.GetInt("moedas", 0) + 1));
            PlayerPrefs.SetInt("totalCoins", (PlayerPrefs.GetInt("totalCoins", 0) + 1));
            SoundManagerScript.PlaySound("Score");
            if (PlayerPrefs.GetString("gameMode", "classic") == "inverse"){
                if (Score.score > PlayerPrefs.GetInt("InverseHighscore", 0)) PlayerPrefs.SetInt("InverseHighscore", Score.score);
            } 
            else if (PlayerPrefs.GetString("gameMode", "classic") == "power"){
                if (Score.score > PlayerPrefs.GetInt("PowerHighscore", 0)) PlayerPrefs.SetInt("PowerHighscore", Score.score);
            }
            if (Score.score > melhorpontuacao.highscore)
                {
                    melhorpontuacao.highscore = Score.score;
                    PlayerPrefs.SetInt("Highscore", melhorpontuacao.highscore);
                }
    }
}
