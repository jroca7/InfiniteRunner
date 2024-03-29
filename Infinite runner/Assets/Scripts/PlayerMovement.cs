﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour

{
    bool alive = true;

    public Button botonSubmit;
    public InputField inputName;
    public Button botonGetScore;
    public Button botonMostrarRanking;

    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float HorizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

   
    private void FixedUpdate()
    {
        if (!alive) return;



        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * HorizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove) ;
    }
    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < -5)
        {
            Die();
        }

    }

    public void Die()
    {
        alive = false;
        botonSubmit.gameObject.SetActive(true);
        inputName.gameObject.SetActive(true);
        botonGetScore.gameObject.SetActive(true);
        botonMostrarRanking.gameObject.SetActive(true);
        
        //Invoke("Restart", 1);
    }

    public void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
