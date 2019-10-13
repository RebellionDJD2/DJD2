﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Create Answer", menuName = "Answer")]
public class PLAYERanswer : ScriptableObject
{
    public int toID;
    public Command[] command = new Command[0];

    public int cost;
    public List<Item> itemsToGive = new List<Item>();
    public List<Quest> questsToGive = new List<Quest>();

    public string text = "APPLY_TEXT_FIELD";

    public enum Command
    {
        Quit,
        GiveItem,
        GiveQuest,
        SubtractMoney,
        AddMoney,

    }
}

