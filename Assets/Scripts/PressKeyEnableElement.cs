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
            textGameObject.SetActive(false);
            itemToShow.SetActive(true);
            player.canMove = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.SetText(message);
            textGameObject.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.text = "";
            inRange = false;
            textGameObject.SetActive(false);
            itemToShow.SetActive(false);
        }
    }
}
