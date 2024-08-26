using UnityEngine;

[System.Serializable]

public class Dialog
{
    [SerializeField] private string charName;

    [SerializeField, TextArea(1,5)] private string content;

    public string getCharName => charName;
    public string getContent => content;

}
