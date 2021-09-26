using System;

[Serializable]
public class User
{
    public string userName;
    public int userScore;

    public User()
    {
        userName = GameManager.playerName;
        userScore = GameManager.playerScore;
    }
}
