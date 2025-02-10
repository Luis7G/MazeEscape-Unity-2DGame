using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    //###### Lo nuevo ######
    public float boostSpeed = 9.0f;  // Velocidad aumentada durante el sprint
    public float boostDuration = 2.0f; // Duración del sprint
    public float boostCooldown = 5.0f; // Tiempo de espera antes de volver a sprintar
    private bool canBoost = true;  // Controla si el jugador puede hacer sprint
    private float currentSpeed;  // Variable para almacenar la velocidad actual
    
    public Text keyAmount;
    public int keys = 0;
    public GameObject door;
    public Text winText;
    
    void Start()
    {
        //###### Lo nuevo ######
        currentSpeed = speed;
    }
    
    void Update()
    {
        //###### Modificado ######
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-currentSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, currentSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -currentSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canBoost)
        {
            StartCoroutine(Sprint());
        }

        if (keys >= 4)
        {
            Destroy(door);
        }
    }
    
    //###### Lo nuevo ######
    private IEnumerator Sprint()
    {
        canBoost = false;
        currentSpeed = boostSpeed;
        yield return new WaitForSeconds(boostDuration);
        currentSpeed = speed;
        yield return new WaitForSeconds(boostCooldown);
        canBoost = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Keys")
        {
            Destroy(collision.gameObject);
            keys++;
            keyAmount.text = "Keys: " + keys;
        }        
        
        if (collision.gameObject.tag == "Princess")
        {
            winText.text = "You Win!";
        }
        
        if (collision.gameObject.tag == "Enemies")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (collision.gameObject.tag == "Walls")
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
        
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }
        }
    }
}
