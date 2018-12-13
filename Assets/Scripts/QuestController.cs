/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages different types of quests.
/// </summary>
public class QuestController : MonoBehaviour {
    
    public GameObject questPanel;
    public Sprite correct;
    public Sprite wrong;
    public Sprite notAnswered;

    //Stores all obtained quests
    List<Quest> quests;

    //For selecting current quest
    int browseCount;
    int questCount;

    Quest defaultQuest;

    string userStringAnswer;
    int userIntAnswer;


	// Use this for initialization
	void Start () {

        //sets up quest list and iterators
        quests = new List<Quest>();
        questCount = 0;
        browseCount = 0; 

        //set up questPanel
        questPanel = GameObject.Find("QuestPanel");
        questPanel.SetActive(false);
        questPanel.transform.Find("UseButton").gameObject.SetActive(false);
        //sets up defaultquest
        defaultQuest = new Quest();
        createDefaultQuest();
        quests.Add(defaultQuest);
	}
	
	// Update is called once per frame
	void Update () {


        //opens Quest menu
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!questPanel.activeInHierarchy)
            {
                questPanel.SetActive(true);
                FindObjectOfType<PlayerController>().canMove = false;
                browseCount = questCount - 1;
                displayQuest(quests[browseCount]); //always show most recent quest
            }
            else
            {
                questPanel.SetActive(false);
                FindObjectOfType<PlayerController>().canMove = true;
                questPanel.transform.Find("InputField").GetComponent<InputField>().DeactivateInputField();
            }
        }

        //user pressed Enter
        if (questPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Return))
        {
            if ((quests[browseCount].questType == "quest") && ((!quests[browseCount].isCorrect))) {
                CheckAnswer();
                displayQuest(quests[browseCount]);
            }

            if ((quests[browseCount].questType == "take") && 
                ((questPanel.transform.Find("UseButton").GetComponent<Button>().IsInteractable())))
            {
                if (!quests[browseCount].itemAcquired && 
                        GameObject.Find(quests[browseCount].triggeredBy).GetComponent<InteractableController>().InTrigger() ) {
                    //once button is activated, user has sufficient in inventory
                    FindObjectOfType<InventoryController>().updateInventory(quests[browseCount].items);
                    FindObjectOfType<InventoryController>().displayInventory(FindObjectOfType<InventoryController>().getInventory());

                    quests[browseCount].isCorrect = true;
                    quests[browseCount].itemAcquired = true;

                    questPanel.transform.Find("Indicator").GetComponent<Image>().sprite = correct;
                    GameObject.Find(quests[browseCount].triggeredBy).GetComponent<InteractableController>().ChangeSprite();
                    questPanel.transform.Find("UseButton").gameObject.SetActive(false);
                    FindObjectOfType<DialogueManager>().displayToInfoPanel(quests[browseCount].rewardText);

                    if (quests[browseCount].triggeredBy == "DoorClosed") {
                        //WIN
                        FindObjectOfType<VictoryController>().LoadVictoryScreen();
                    }
                }
               
            }

        }

        //already in Quest Menu, begin browsing
        if (questPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.RightArrow)) { cycleDisplayRight(); }
        if (questPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.LeftArrow)) { cycleDisplayLeft(); }
	}

    //"TAKE" QUESTS ONLY
    public bool CheckInventory(Quest quest) {
        //compare inventory array with quest array

        int inventorySize = FindObjectOfType<InventoryController>().getInventorySize();
        int[] inventory = new int[inventorySize];
        List<int> checkList;
        checkList = new List<int>();

        inventory = FindObjectOfType<InventoryController>().getInventory();
        for (int i = 0; i < quest.items.Length; i++) {
            //locate the requirements and the location in arrays
            if (quest.items[i] == -1) {
                checkList.Add(i);
            }
        }
        foreach (int x in checkList) {
            int difference = quest.items[x] + inventory[x];
            if (difference < 0) { //failed the test
                return false;
            } 
        }
        return true;
    }

    //"QUEST" QUESTS ONLY
    public void CheckAnswer() 
    {
        Debug.Log("CheckAnswer");
        //get user input
        userStringAnswer = questPanel.transform.Find("InputField").GetComponent<InputField>().text;
        userIntAnswer = Convert.ToInt32(userStringAnswer);

        if (userIntAnswer == quests[browseCount].answer) //Correct answer
        {
            Debug.Log("Correct");
            quests[browseCount].isCorrect = true; 
            quests[browseCount].itemAcquired = true;
            questPanel.transform.Find("Indicator").GetComponent<Image>().sprite = correct;
            FindObjectOfType<InventoryController>().updateInventory(quests[browseCount].items);
            FindObjectOfType<InventoryController>().displayInventory(FindObjectOfType<InventoryController>().getInventory());
            FindObjectOfType<DialogueManager>().displayToInfoPanel(quests[browseCount].rewardText);
        }
        else //Wrong answer
        {
            Debug.Log("Wrong");
            quests[browseCount].isCorrect = false;
        }
    }

    public void createDefaultQuest()
    {
        defaultQuest.questName = "No Quests Yet!";
        defaultQuest.questDescription = "Keep looking, I'm sure you'll find one.";
        defaultQuest.questType = "default";
        defaultQuest.question = null;
        defaultQuest.answer = 0;
        defaultQuest.isCorrect = false;
        defaultQuest.hasGiven = false;
        defaultQuest.items = new int[] {0,0,0,0,0,0};
        defaultQuest.rewardText = " ";
        questCount++;
    }

    public void cycleDisplayRight() 
    {
        if (questCount == 1) { displayQuest(quests[0]); }
        else //more than 1 quest
        {
            if (browseCount == (questCount - 1)) { browseCount = 1; } 
            else { browseCount++; }
            if (!quests[browseCount].isCorrect) {
                questPanel.transform.Find("Indicator").GetComponent<Image>().sprite = notAnswered;
            }
            displayQuest(quests[browseCount]);
        }
    }

    public void cycleDisplayLeft()
    {
        
        if (questCount == 1) { displayQuest(quests[0]); }
        else {
            if (browseCount == 1) { browseCount = questCount - 1; } 
            else { browseCount--; }
            if (!quests[browseCount].isCorrect)
            {
                questPanel.transform.Find("Indicator").GetComponent<Image>().sprite = notAnswered;
            }
            displayQuest(quests[browseCount]); 
        }
    }


    public void displayQuest(Quest quest)
    {
        Debug.Log("Current isCorrect: " + quest.isCorrect);
        if (questCount == 1) { 
            //only one quest, the default one
            questPanel.transform.Find("QuestNameText").GetComponent<Text>().text = quest.questName;
            questPanel.transform.Find("DialogueText").GetComponent<Text>().text  = quest.questDescription;
            questPanel.transform.Find("InputField").gameObject.SetActive(false);
            questPanel.transform.Find("Indicator").gameObject.SetActive(false);

        } else {

            //displays common elements to all quests
            questPanel.transform.Find("QuestNameText").GetComponent<Text>().text = quest.questName;
            questPanel.transform.Find("DialogueText").GetComponent<Text>().text = quest.questDescription;


            //questType quest
            if (quest.questType == "quest") {
                questPanel.transform.Find("Question").GetComponent<SpriteRenderer>().enabled = true;
                questPanel.transform.Find("Question").GetComponent<SpriteRenderer>().sprite = quest.question;
                questPanel.transform.Find("InputField").gameObject.SetActive(true);
                questPanel.transform.Find("Indicator").gameObject.SetActive(true);
                questPanel.transform.Find("UseButton").gameObject.SetActive(false);

                if (quest.isCorrect)
                { 
                    //user gets it correct, no need to answer again
                    questPanel.transform.Find("Indicator").GetComponent<Image>().sprite = correct;
                    questPanel.transform.Find("InputField").gameObject.SetActive(false); 
                }
                else
                { 
                    questPanel.transform.Find("Indicator").GetComponent<Image>().sprite = wrong;
                    questPanel.transform.Find("InputField").GetComponent<InputField>().ActivateInputField();
                }
            }


            //questType take
            if (quest.questType == "take") {
                questPanel.transform.Find("Question").GetComponent<SpriteRenderer>().enabled = false;
                questPanel.transform.Find("Indicator").gameObject.SetActive(true);
                questPanel.transform.Find("UseButton").gameObject.SetActive(true);
                questPanel.transform.Find("InputField").gameObject.SetActive(false);

                if (!quest.isCorrect) 
                {
                    if (CheckInventory(quests[browseCount]) && 
                        GameObject.Find(quests[browseCount].triggeredBy).GetComponent<InteractableController>().InTrigger()) //required items available
                    { 
                        questPanel.transform.Find("UseButton").GetComponent<Button>().interactable = true;
                    }
                    else
                    {   //insufficient items
                        questPanel.transform.Find("UseButton").GetComponent<Button>().interactable = false;
                        questPanel.transform.Find("Indicator").GetComponent<Image>().sprite = notAnswered;

                    }
                } else //quest already answered
                {
                    questPanel.transform.Find("UseButton").GetComponent<Button>().gameObject.SetActive(false);
                    questPanel.transform.Find("Indicator").GetComponent<Image>().sprite = correct;
                }
            }
        }
    }

    public void removeQuest(Quest quest)
    {
        quests.Remove(quest);
        questCount--;
    }

    public void addQuest(Quest quest)
    {
        quests.Add(quest);
        questCount++;
        Debug.Log("Quest added: " + quest.questName);
    }

}
