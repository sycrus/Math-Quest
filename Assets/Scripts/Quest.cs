/*
 * Created by Joe Chung, 2018
 * joechung.me
 */
using UnityEngine;

/// <summary>
/// Quest class.
/// </summary>
public class Quest {
    public string questName;
    public string questType; //quest, take --> different questPanel and rewardPanel set ups for each
    public string triggeredBy;
    public string questDescription;
    public Sprite question;
    public int answer;

    //check if quest has been completed
    public bool isCorrect;

    //check if quest items have been given
    public bool itemAcquired;

    //check if interactable has already given quest
    public bool hasGiven;

    //rewards
    // wood, stone, gold, axe, mould, key
    public int[] items;

    //Reward text
    public string rewardText;

}
