using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class InputsManager : MonoBehaviour
{
    public static InputsManager instance { private set; get; }

    private InputAction moveAction;

    [HideInInspector] public UnityEvent interactionEvent;
    [HideInInspector] public UnityEvent dialogEvent;
    [HideInInspector] public UnityEvent inventoryEvent;

    private PlayerInput inputs;

    private InventoryManager inventoryManager;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;

        inputs = GetComponent<PlayerInput>();

        moveAction = inputs.actions.FindAction("Move");

        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public Vector2 GetMovingInputs()
    {
        return moveAction.ReadValue<Vector2>();
    }

    public void OnInteract()
    {
        interactionEvent.Invoke();
        dialogEvent.Invoke();
    }

    public void OnInventory()
    {
        inventoryManager.ToggleInventory();
    }

}
