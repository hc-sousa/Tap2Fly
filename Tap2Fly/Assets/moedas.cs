using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moedas : MonoBehaviour
{
    public static int moeda = 0;
    // Start is called before the first frame update
    void Start()
    {
        moeda = PlayerPrefs.GetInt("moedas", 0);
    }

    // Update is called once per frame
    void Update()
    {
        moeda = PlayerPrefs.GetInt("moedas");
        GetComponent<UnityEngine.UI.Text>().text = moeda.ToString();
    }
}
