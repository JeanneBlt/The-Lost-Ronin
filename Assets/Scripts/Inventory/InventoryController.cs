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

        if (data != null && display != null)
        {
            data.Init(this);
            display.Init(this);
            display.UpdateDisplay(data.Slots);
        }
    }

    private void OnEnable()
    {
        if (data != null && display != null)
        {
            UpdateInventory();
        }
    }

    public void UpdateInventory()
    {
        if (data != null && display != null)
        {
            data.Init(this);
            display.UpdateDisplay(data.Slots);
        }
    }

    public int SlotNumber => data != null ? data.SlotNumber : 0;

}
