/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using UnityEngine;

/// <summary>
/// Dialogue trigger.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
