using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]

public class UIDialog : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI contentText;

    private bool isDialogVisible;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        CloseDialog();
        isDialogVisible = false;
    }

    public void SetDialog(string pContent)
    {
        canvas.enabled = true;
        contentText.text = pContent;
        isDialogVisible = true;
    }

    public void CloseDialog()
    {
        canvas.enabled = false;
        isDialogVisible = false;
    }
    public bool IsDialogActive()
    {
        return isDialogVisible;
    }
}
