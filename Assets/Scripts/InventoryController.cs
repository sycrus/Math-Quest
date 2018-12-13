/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using UnityEngine;

/// <summary>
/// Manages inventory.
/// </summary>
public class InventoryController : MonoBehaviour {
    
    public GameObject inventoryPanel;
    GameObject[] inventoryList;
    private int[] inventoryKey; //only accessible from here

    bool wood;
    bool stone;
    bool gold;
    bool axe;
    bool mould;
    bool key;

	// Use this for initialization
	void Start () {
        inventoryKey = new int[6];
        inventoryList = new GameObject[6];

        //inventoryPanel.transform.Find("Background").gameObject.SetActive(false);
        inventoryPanel.SetActive(false);

        inventoryList[0] = inventoryPanel.transform.Find("Wood").gameObject;
        inventoryList[1] = inventoryPanel.transform.Find("Stone").gameObject;
        inventoryList[2] = inventoryPanel.transform.Find("Gold").gameObject;
        inventoryList[3] = inventoryPanel.transform.Find("Axe").gameObject;
        inventoryList[4] = inventoryPanel.transform.Find("Mould").gameObject;
        inventoryList[5] = inventoryPanel.transform.Find("Key").gameObject;
       
	}

    private void Update()
    {
        //User presses "I" when inventory is not open ==> open it
        if (!inventoryPanel.transform.Find("Background").gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.I)) {
            inventoryPanel.SetActive(true);
            displayInventory(inventoryKey);
            Debug.Log("Inventory: " + inventoryKey[0] + ", "
                      + inventoryKey[1] + ", "
                      + inventoryKey[2] + ", "
                      + inventoryKey[3] + ", "
                      + inventoryKey[4] + ", "
                      + inventoryKey[5]);
        }
        //User presses "I" when inventory is open ==> close it
        else if (inventoryPanel.transform.Find("Background").gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(false);
        }
    }

    // updates inventory; called every time an object is given
    public void updateInventory (int[] items) {
        Debug.Log("Updating inventory");
        for (int i = 0; i < items.Length; i++) {
            inventoryKey[i] += items[i];
        }

	}

    public int[] getInventory() {
        return inventoryKey;
    }

    public int getInventorySize()
    {
        return inventoryKey.Length;
    }

    public void displayInventory(int[] inv) {
        for (int i = 0; i < inv.Length; i++)
        {
            if (inv[i] == 1) {
                inventoryList[i].SetActive(true);
            } else {
                inventoryList[i].SetActive(false);
            }
        }
    }
}
