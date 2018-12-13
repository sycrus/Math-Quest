/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using UnityEngine;

/// <summary>
/// Stores the dialogue.
/// </summary>
[System.Serializable]
public class Dialogue
{

    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

}
