/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UI_InputWindow : MonoBehaviour {

    private static UI_InputWindow instance;

    private Button_UI okBtn;
    private Button_UI cancelBtn;
    private TextMeshProUGUI titleText;
    public TMP_InputField inputField;

    private void Awake() {
        instance = this;

        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        cancelBtn = transform.Find("cancelBtn").GetComponent<Button_UI>();

        Hide();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            okBtn.ClickFunc();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            cancelBtn.ClickFunc();
        }
    }

    public void Show(string inputString, string validCharacters, int characterLimit) {
        gameObject.SetActive(false);
        transform.SetAsLastSibling();


        inputField.characterLimit = characterLimit;
        inputField.onValidateInput = (string text, int charIndex, char addedChar) => {
            return ValidateChar(validCharacters, addedChar);
        };

        inputField.text = inputString;
        inputField.Select();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private char ValidateChar(string validCharacters, char addedChar) {
        if (validCharacters.IndexOf(addedChar) != -1) {
            // Valid
            return addedChar;
        } else {
            // Invalid
            return '\0';
        }
    }
/*
    public static void Show_Static(string titleString, string inputString, string validCharacters, int characterLimit, Action onCancel, Action<string> onOk) {
        instance.Show(titleString, inputString, validCharacters, characterLimit, onCancel, onOk);
    }

    public static void Show_Static(string titleString, int defaultInt, Action onCancel, Action<int> onOk) {
        instance.Show(titleString, defaultInt.ToString(), "0123456789-", 20, onCancel, 
            (string inputText) => {
                // Try to Parse input string
                if (int.TryParse(inputText, out int _i)) {
                    onOk(_i);
                } else {
                    onOk(defaultInt);
                }
            }
        );
    }*/
}
