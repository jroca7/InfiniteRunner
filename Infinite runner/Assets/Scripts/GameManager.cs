using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager inst;

    [SerializeField] Text scoreText;
 


    public void IncrementScore()
    {
        score++;
        scoreText.text = "SCORE:  " + score;

    
    }

    private void Awake()
    {
        inst = this;
    }
    
    private void Start()
    {
        
    }

   
    private void Update()
    {
        
    }
}
