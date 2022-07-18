using UnityEngine;
using TMPro;
public class DialogWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textField;
    [SerializeField]
    private TextMeshProUGUI _name;

    private bool _isOpen => gameObject.activeSelf;
    private void OpenWindow()
    {
        print("Dialog window open");
        gameObject.SetActive(true);
        DialogueManager.Instance.ContinueDialogue();
    }
    public void CloseWindow()
    {
        print("Dialog window close");
        gameObject.SetActive(false);
    }
    public void ParseDialogue(string text)
    {
        _textField.text = text;
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.E))
                DialogueManager.Instance.ContinueDialogue();
        }
    }
    public void Awake()
    {
        DialogueManager.OnDialogStart += OpenWindow;
        DialogueManager.OnDialogEnd += CloseWindow;
        gameObject.SetActive(false);
    }
    public void OnDestroy()
    {
        DialogueManager.OnDialogStart -= OpenWindow;
        DialogueManager.OnDialogEnd -= CloseWindow;
    }
}
