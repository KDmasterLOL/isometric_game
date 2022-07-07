using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogTrigger : MonoBehaviour
{
    private DialogData _dialog;
    void Start()
    {
        _dialog = GetComponent<DialogData>();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag(Tags.Player))
        {
            DialogueManager.Instance.RiseDialog(_dialog);
        }
    }
}
