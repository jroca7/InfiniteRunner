using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;


public class GameManager : MonoBehaviour
{
    public int score;
    public InputField nameText;
    public static GameManager inst;

    [SerializeField] Text scoreText;
    User user = new User();
    public static int playerScore;
    public static string playerName;


    public void IncrementScore()
    {
        score++;
        scoreText.text = "SCORE:  " + score;

    
    }

    private void Awake()
    {
        inst = this;
    }

    private void UpdateScore()
    {
        scoreText.text = "Puntos: " + user.userScore;
    }

    private void PostToDataBase()
    {
        User user = new User();
        RestClient.Put("https://uai-simuladores21-tp1grupo6-default-rtdb.firebaseio.com/" + playerName + ".json", user);
    }
    public void OnSubmit()
    {
        playerName = nameText.text;
        PostToDataBase();
    }
    private void RetrieveFromDataBase()
    {
        RestClient.Get<User>("https://uai-simuladores21-tp1grupo6-default-rtdb.firebaseio.com/" + nameText.text + ".json").Then(response =>

        {
            user = response;
            UpdateScore();
        });
    }
    public void OnGetScore()
    {
        RetrieveFromDataBase();
    }


}
