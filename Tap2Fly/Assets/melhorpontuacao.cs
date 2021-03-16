using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melhorpontuacao : MonoBehaviour
{
    public static int highscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<UnityEngine.UI.Text>().text = highscore.ToString();
    }
}
