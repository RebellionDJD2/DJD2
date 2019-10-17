﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnswer
{
    [SerializeField]
    private string name;
    [SerializeField]
    SwitchType switchTo = SwitchType.Dialogue;
    [SerializeField]
    private int toID = 0;
    [SerializeField]
    private int cost = 0;
    [SerializeField]
    private List<Item> itemsToGive = new List<Item>();
    [SerializeField]
    private List<Quest> questsToGive = new List<Quest>();
    [SerializeField]
    private string text = "APPLY_TEXT_FIELD";
    [SerializeField]
    private ConversationRequesite requisites = null;

    public ConversationRequesite Requesites { get => requisites; }
    public SwitchType SwitchTo { get => switchTo; }
    public string Text { get => text; }
    public int ToID { get => toID; }
    public int Cost { get => cost; }
    public List<Item> ItemsToGive { get => itemsToGive; }
    public List<Quest> QuestsToGive { get => questsToGive; }
}

