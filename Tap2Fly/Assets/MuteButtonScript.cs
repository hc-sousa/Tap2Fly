using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MuteButtonScript : MonoBehaviour
{
    public Sprite OffSprite;
    public Sprite OnSprite;
    public Button but;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((PlayerPrefs.GetFloat("volume") == 0.0))
            but.image.sprite = OffSprite;
        else
        {
            but.image.sprite = OnSprite;
        }
    }
}
