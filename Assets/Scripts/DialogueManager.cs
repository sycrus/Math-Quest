/*
 * Created by Joe Chung, 2018
 * joechung.me
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages and displays dialogue
/// </summary>
public class DialogueManager : MonoBehaviour {
   
    public GameObject dialoguePanel;
    public GameObject infoPanel;

    Text nameText; //name of NPC
    Text dialogueText; //dialogue text
    Text infoText;
    string text;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        nameText = dialoguePanel.transform.Find("NameText").GetComponent<Text>();
        dialogueText = dialoguePanel.transform.Find("DialogueText").GetComponent<Text>();
        infoText = infoPanel.transform.Find("InfoText").GetComponent<Text>();

        dialoguePanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Makes the dialogue panel active

        dialoguePanel.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = false;

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void Update()
    {
        if (dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.RightArrow)) {
            DisplayNextSentence();
        }
         
        //dismiss infoPanel
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (infoPanel.activeInHierarchy) {
                infoPanel.SetActive(false);
            }
        }

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }


    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        //announce quest added
         
        FindObjectOfType<PlayerController>().canMove = true;

    }

    public void displayToInfoPanel(string info) 
    {
        infoPanel.SetActive(true);
        infoText.text = info ;

    }


}
