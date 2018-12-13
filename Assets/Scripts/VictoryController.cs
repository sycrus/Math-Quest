/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Displays screen on completion of game.
/// </summary>
public class VictoryController : MonoBehaviour {

    GameObject inventoryPanel;
    GameObject userMenuSmall;
    GameObject infoPanel;
    GameObject questPanel;
    GameObject dialoguePanel;

    public GameObject victoryScreen;
	// Use this for initialization
	void Start () {
        victoryScreen.SetActive(false);

        inventoryPanel = FindObjectOfType<InventoryController>().inventoryPanel;
        userMenuSmall = FindObjectOfType<ScreenController>().userMenuSmall;
        infoPanel = FindObjectOfType<DialogueManager>().infoPanel;
        questPanel = FindObjectOfType<QuestController>().questPanel;
        dialoguePanel = FindObjectOfType<DialogueManager>().dialoguePanel;


	}

    private void Update()
    {
        if (victoryScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    public void LoadVictoryScreen() {

        inventoryPanel.SetActive(false);
        userMenuSmall.SetActive(false);
        infoPanel.SetActive(false);
        questPanel.SetActive(false);
        dialoguePanel.SetActive(false);

        victoryScreen.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = false;
    }
}
