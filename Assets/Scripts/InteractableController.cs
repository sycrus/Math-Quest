/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using UnityEngine;

/// <summary>
/// Manages all the interactables in the game.
/// </summary>
public class InteractableController : MonoBehaviour {
    
    public GameObject interactable;
    public Sprite altSprite;

    public Dialogue dialogue;

    Quest quest;

    public string questName;
    public string questType;
    public string triggeredBy;

    //all: questName, questType, questDescription

    //quest = quest added: question, InputField, indicator, items, rewardText 
    //take = quest takes something from player: UseButton, items, rewardText
    //default = spacefiller

    [TextArea(3, 10)]
    public string questDescription;
    public Sprite question;

    public int answer;

    //Set Rewards here, 1 or -1
    public int wood;
    public int stone;
    public int gold;
    public int axe;
    public int mould;
    public int key;
    public int[] items;

    public string rewardText;

    bool inTrigger;

	void Start () {
        
        this.quest = new Quest();
        InitializeInteractablePositionAndActive();
        InitializeInteractableQuest();
        inTrigger = false;
	}

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.name == "Player")
        {
            this.inTrigger = true;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.name == "Player")
        {
            this.inTrigger = false;
        }
    }

    void InitializeInteractablePositionAndActive() {
        if (this.name == "Sage") { interactable.transform.position = new Vector3(1.5f, -0.5f, 0f); }

        if (this.name == "Axe") { interactable.transform.position = new Vector3(5.5f, -1.5f, 0f); }

        if (this.name == "Tree") { interactable.transform.position = new Vector3(6.5f, -3.5f, 0f); }

        if (this.name == "TreeChopped")
        {
            interactable.transform.position = new Vector3(6.5f, -3.5f, 0f);
            interactable.gameObject.SetActive(false);
        }

        if (this.name == "Firepit") { interactable.transform.position = new Vector3(-5.5f, -0.5f, 0f); }

        if (this.name == "FireLit")
        {
            interactable.transform.position = new Vector3(-5.5f, -0.5f, 0f);
            interactable.gameObject.SetActive(false);
        }

        if (this.name == "Boat") { interactable.transform.position = new Vector3(3.5f, 0.5f, 0f); }

        if (this.name == "Sign") { interactable.transform.position = new Vector3(-2.5f, 0.5f, 0f); }

        if (this.name == "DoorClosed") { interactable.transform.position = new Vector3(-3.5f, 2.5f, 0f); }

        if (this.name == "DoorOpened")
        {
            interactable.transform.position = new Vector3(-3.5f, 2.5f, 0f);
            interactable.gameObject.SetActive(false);
        }
    }

    void InitializeInteractableQuest () {
        //sets up this GameObject's Quest class instance
        this.quest.questName = questName;
        this.quest.questType = questType;
        this.quest.triggeredBy = triggeredBy;
        this.quest.questDescription = questDescription;
        this.quest.question = question;
        this.quest.answer = answer;

        items = new int[6];
        items[0] = wood;
        items[1] = stone;
        items[2] = gold;
        items[3] = axe;
        items[4] = mould;
        items[5] = key;
        this.quest.items = items;

        this.quest.rewardText = rewardText;
        this.quest.isCorrect = false;
        this.quest.hasGiven = false;
    }
	
	
    private void Update()
    {
        if (this.inTrigger && Input.GetKeyUp(KeyCode.Space))
        {
            //only trigger dialogue and give quest for first time
            if (!this.quest.hasGiven) { 
                Debug.Log("Interactable " + this.interactable.name + " activated");
                TriggerDialogue(this.quest);

                string text = "You have received a new quest!";
                FindObjectOfType<DialogueManager>().displayToInfoPanel(text);
            }
        }

    }

    public bool InTrigger() {
        if (this.inTrigger)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public void ChangeSprite() {
        this.transform.GetComponent<SpriteRenderer>().sprite = altSprite;
    }

    public void TriggerDialogue(Quest quest)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<QuestController>().addQuest(quest);
        quest.hasGiven = true;
    }

}
