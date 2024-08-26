using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Canvas))]

public class UIDialog : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI contentText;


    private bool isDialogVisible;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        CloseDialog();
        isDialogVisible = false;
    }

    public void SetDialog(Dialog pDialog)
    {
        canvas.enabled = true;
        contentText.text = pDialog.getCharName+"\n"+pDialog.getContent;
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
