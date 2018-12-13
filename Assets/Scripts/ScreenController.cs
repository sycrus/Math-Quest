/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using UnityEngine;

/// <summary>
/// Loads initial screen.
/// </summary>
public class ScreenController : MonoBehaviour {

    public GameObject titleScreen;
    public GameObject userMenuSmall;
    public GameObject userMenuLarge;

    bool isOpen;

	// Use this for initialization
	void Start () {
        titleScreen.SetActive(true);
        userMenuSmall.SetActive(false);
        userMenuLarge.SetActive(false);
        FindObjectOfType<PlayerController>().canMove = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            titleScreen.SetActive(false);
            userMenuSmall.SetActive(true);
            FindObjectOfType<PlayerController>().canMove = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!userMenuLarge.activeInHierarchy) {
                userMenuLarge.SetActive(true);
                userMenuSmall.SetActive(false);
                FindObjectOfType<PlayerController>().canMove = false;
            } else {
                userMenuLarge.SetActive(false);
                userMenuSmall.SetActive(true);
                FindObjectOfType<PlayerController>().canMove = true;
            } 

        }
	}
}
