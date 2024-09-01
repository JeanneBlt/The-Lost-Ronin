using UnityEngine;

public class InventoryData : MonoBehaviour
{
    [SerializeField] private int slotNumber;
    private InventoryController controller;
    public void Init(InventoryController pController)
    {
        controller = pController;
    }

    public int SlotNumber => slotNumber;
}
