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

    [HideInInspector]public UnityEvent interactionEvent;

    private PlayerInput inputs;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;

        inputs = GetComponent<PlayerInput>();

        moveAction = inputs.actions.FindAction("Move");
    }

    public Vector2 GetMovingInputs()
    {
        return moveAction.ReadValue<Vector2>();
    }

    public void OnInteract()
    {
        interactionEvent.Invoke();
    }

}
