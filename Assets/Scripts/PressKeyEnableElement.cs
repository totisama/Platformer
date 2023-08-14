using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressKeyEnableElement : MonoBehaviour
{
    [SerializeField] GameObject itemToShow;
    [SerializeField] GameObject textGameObject;
    [SerializeField] PlayerController player;
    [SerializeField] string message;
    private bool inRange;
    private bool opened;
    private TMP_Text text;

    private void Awake()
    {
        text = textGameObject.GetComponent<TMP_Text>();
    }

    private void Start()
    {
        itemToShow.SetActive(false);
        textGameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && inRange)
        {
            if (!opened)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }

    private void Open()
    {
        textGameObject.SetActive(false);
        itemToShow.SetActive(true);
        player.canMove = false;
        opened = true;

        //Stop the player velocity 
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
    }

    public void Close()
    {
        textGameObject.SetActive(true);
        itemToShow.SetActive(false);
        player.canMove = true;
        opened = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            text.SetText(message);
            textGameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // textGameObject condition added to avoid error when closing game
        if (collision.gameObject.CompareTag("Player") && textGameObject)
        {
            inRange = false;
            text.SetText("");
            textGameObject.SetActive(false);
        }
    }
}
