using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InventoryData), typeof(InventoryDisplay))]

public class InventoryController : MonoBehaviour
{

    private GameManager manager;
    private InventoryData data;
    private InventoryDisplay display;

    private void Start()
    {
        data = GetComponent<InventoryData>();
        display = GetComponent<InventoryDisplay>();

        manager = GameManager.GetInstance();

        data.Init(this);
        display.Init(this);

        display.UpdateDisplay(data.Slots);
    }

public int SlotNumber => data.SlotNumber;

}
