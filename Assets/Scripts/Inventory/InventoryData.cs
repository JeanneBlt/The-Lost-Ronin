using UnityEngine;

[System.Serializable]
public class SlotsInfos
{
    public SlotsInfos(ItemTemplate pTemplate, int pNumber)
    {
        template = pTemplate;

        name = template.name;
        number = pNumber;
    }

    [SerializeField, HideInInspector] private string name;

    [SerializeField] private int number = 0;

    private ItemTemplate template { get; }

    public string ItemName => template.ItemName;
    public int ItemId => template.ItemId;

    public int Stack => template.Stack;

    public int Number => number;

}

public class InventoryData : MonoBehaviour
{
    [SerializeField] private int slotNumber;
    [SerializeField] private SlotsInfos[] slotsInfos;
    [SerializeField] private ItemTemplate[] templates;

    private InventoryController controller;

    public void Init(InventoryController pController)
    {
        controller = pController;

        slotsInfos = new SlotsInfos[slotNumber];
        for (int i = 0; i < slotNumber; i++)
        {
            slotsInfos[i] = new SlotsInfos(templates[0],0);
        }
        slotsInfos[0] = new SlotsInfos(templates[1],1);
    }

    public int SlotNumber => slotNumber;
    public SlotsInfos[] Slots=> slotsInfos;
}
