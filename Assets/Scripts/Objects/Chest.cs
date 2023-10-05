using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{

    //Stats
    [SerializeField] private Item[] content;

    //Checkers
    private bool isReach = false;
    private bool open = false;

    //Refs
    private GameManager manager;

    //Inputs
    private InputAction interactAction;

    private void Start()
    {
        manager = GameManager.GetInstance();
        interactAction = manager.GetInputs().actions.FindAction("Interact");
    }

    private void Update()
    {
        float interact = interactAction.ReadValue<float>();  

        if (isReach && interact > 0)
        {
            open = true;
            EmptyChest();
        }

        else if (!isReach)
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
