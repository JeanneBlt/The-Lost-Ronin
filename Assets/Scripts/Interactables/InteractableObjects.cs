using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]

public abstract class InteractableObjects : MonoBehaviour
{

    protected bool isReach = false;
    protected bool open = false;

    private GameManager manager;

    private void Start()
    {
        manager = GameManager.GetInstance();
        InputsManager.instance.interactionEvent.AddListener(Interact);
    }

    public abstract void Interact();

    //public void TriggerInteraction()
    //{
    //    Interact();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player entered the trigger zone.");
            isReach = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
            Debug.Log("Player exited the trigger zone.");
            isReach = false;
            open = false;
            Interact();
        }
    }

}
