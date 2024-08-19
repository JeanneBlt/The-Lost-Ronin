using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{

    [SerializeField] private SpriteRenderer[] graphisms;
    //Stats
    [SerializeField] private Item[] content;

    //Checkers
    private bool isReach = false;
    private bool open = false;

    //Refs
    private GameManager manager;

    private void Start()
    {
        manager = GameManager.GetInstance();
        InputsManager.instance.interactionEvent.AddListener(Interact);
    }

    public void Interact()
    {
        if (isReach)
        {
            open = true;
            EmptyChest();
        }

        else
        {
            open = false;
        }
    }

    private void EmptyChest()
    {
        foreach (var item in content) 
        {
            CharacterInfos.AddItem(item.id, item.number);
            item.number = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isReach = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isReach = false;
        }
    }
}
