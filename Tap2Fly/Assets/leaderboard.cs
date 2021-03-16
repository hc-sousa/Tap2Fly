using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaderboard : MonoBehaviour
{
    #region PRIVATE CODE
    const string privateCode = "xoMZ8erYiUSPAo2HOGVNigrsu5X2sc60WqfP6PRGV5Gw";
    #endregion
    const string publicCode = "604fd22a8f40bc2280b85209";
    const string webURL = "http://dreamlo.com/lb/";
    public Highscore[] Leaderboard;

    public void AddNewHighscore(string username, int score){ StartCoroutine(UploadNewHighScore(username, score)); }

    IEnumerator UploadNewHighScore(string username, int score){
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error)) Debug.Log("UploadSuccess");
        else Debug.Log("UploadError: " + www.error);
    }

    IEnumerator DownloadLeaderboard(){
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error)) FormatLeaderboard(www.text);
        else Debug.Log("downloadError: " + www.error);
    }

    void FormatLeaderboard(string text){
        string[] entries = text.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        Leaderboard = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++) {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            Leaderboard[i] = new Highscore(username, score);
            Debug.Log(Leaderboard[i].username + ": " + Leaderboard[i].score);
        }
    }
}

public struct Highscore{
    public string username;
    public int score;

    public Highscore(string _username, int _score){
        username = _username;
        score = _score;
    }
}
