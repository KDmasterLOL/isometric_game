using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private string _knotName;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag(Tags.Player))
        {
            DialogueManager.Instance.StartDialogue(_knotName);
        }
    }
}
