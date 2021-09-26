using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

public class PlayerScores : MonoBehaviour
{
    public Text scoreText;
    public InputField nameText;

    private System.Random random = new System.Random();

    User user = new User();

    public static int playerScore;
    public static string playerName;

    void Start()
    {
        playerScore = random.Next(0, 101);
        scoreText.text = "Puntos: " + playerScore;
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
