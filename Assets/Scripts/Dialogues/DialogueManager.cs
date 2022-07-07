using System;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; } = null;
    [SerializeField]
    private Story _store;

    public static event Action OnDialogStart, OnDialogEnd;
    private bool _isPlayingDialog;
    private DialogWindow _dialogWindow;
    private void Awake()
    {
        if (Instance is null) Instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        _isPlayingDialog = false;
        
    }
    private void FixedUpdate()
    {

    }
    public void RiseDialog(DialogData dialog)
    {
        OnDialogStart?.Invoke();
    }
    void Update()
    {

    }
}
/*
Игрок подходит к обьекту с тригером и если это он делает в первый раз или нажимает на кнопку скрипт отправляет событие появление диалога

*/