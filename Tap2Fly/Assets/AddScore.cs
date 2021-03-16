using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score.score++;
        SoundManagerScript.PlaySound("Score");
        if (Score.score > melhorpontuacao.highscore)
        {
            melhorpontuacao.highscore = Score.score;
            PlayerPrefs.SetInt("Highscore", melhorpontuacao.highscore);
        }
    }
}
