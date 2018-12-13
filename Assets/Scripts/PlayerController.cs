/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using UnityEngine;

/// <summary>
/// Player controller.
/// </summary>
public class PlayerController : MonoBehaviour {

    public GameObject Player;
    public bool canMove;

	// Use this for initialization
	void Start () {
        Player.transform.position = new Vector3(0.5f, -3.5f, 0f);
        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove) {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Debug.Log("Up Arrow key was pressed.");
                Player.transform.localPosition += new Vector3(0f, 1f, 0f);

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Debug.Log("Down Arrow key was pressed.");
                Player.transform.localPosition += new Vector3(0f, -1f, 0f);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Debug.Log("Left Arrow key was pressed.");
                Player.transform.localPosition += new Vector3(-1f, 0f, 0f);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Debug.Log("Right Arrow key was pressed.");
                Player.transform.localPosition += new Vector3(1f, 0f, 0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("Q key was pressed.");

        }
      
	}
}
