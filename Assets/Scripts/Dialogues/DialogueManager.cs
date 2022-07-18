using System;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; } = null;
    [SerializeField]
    private TextAsset _inkJson;
    private Story _store;
    private string _state = string.Empty;
    private bool _isPlayingDialog = false;

    public bool IsPlayingDialog
    {
        get => _isPlayingDialog;
        private set
        {
            if (value != _isPlayingDialog)
            {
                _isPlayingDialog = value;
                if (_isPlayingDialog)
                {
                    print("Dialog started");
                    OnDialogStart?.Invoke();
                }
                else
                {
                    print("Dialog ended");
                    OnDialogEnd?.Invoke();
                }
            }

        }
    }

    [SerializeField]
    private DialogWindow _dialogWindow;

    public static event System.Action OnDialogStart, OnDialogEnd;
    private void Awake()
    {
        if (Instance is null) Instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this);
        print("Dialogue manager created");
    }
    private void Start()
    {
        _isPlayingDialog = false;
        _store = new(_inkJson.text);
        _state = _store.state.ToJson();
        _store.onError += (msg, type) =>
        {
            if (type == Ink.ErrorType.Warning)
                Debug.LogWarning(msg);
            else
                Debug.LogError(msg);
        };
    }
    public void StartDialogue(string knotName)
    {
        _store.state.LoadJson(_state);
        _store.ChoosePathString(knotName);
        IsPlayingDialog = true;
    }

    public void ContinueDialogue()
    {
        print("Continue dialogue");
        if (_store.canContinue)
        {
            _dialogWindow.ParseDialogue(_store.Continue());
            _state = _store.state.ToJson();
        }
        else IsPlayingDialog = false;

    }
}