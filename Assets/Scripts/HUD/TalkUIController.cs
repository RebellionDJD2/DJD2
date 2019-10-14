﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TalkUIController : MonoBehaviour
{
    public GameObject conversationPanel;
    public GameObject answersPanel;
    public Text toTalkPanel;

    public GameObject answerButton;

    private int CurrentDialogue { get; set; }
    public TalkInteractable Interactable { get; set; }
    public PlayerController PlayerController { get; set; }

    private Coroutine slowLettersCoroutine;
    public void Initialize()
    {
        CurrentDialogue = 0;
        SwitchToConversation();
        toTalkPanel.text = Interactable.currentConvo.managerContents[0].nPCdialogue.text;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (slowLettersCoroutine != null)
                StopCoroutine(slowLettersCoroutine);
            SwitchToAnswers();
            InstanciateAnswers();
        }
    }

    // Creates a button and puts the contents inside
    private void InstanciateAnswers()
    {
        while (answersPanel.transform.childCount > 0)
        {
            DestroyImmediate(answersPanel.transform.GetChild(0).gameObject);
        }

        foreach (PlayerAnswer i in Interactable.currentConvo.managerContents[CurrentDialogue].answers)
        {
            GameObject go = Instantiate(answerButton, answersPanel.transform);
            AnswersButton button = go.GetComponent<AnswersButton>();
            button.Initialize(i);
            button.OnClick += OnAnswer;
        }
    }

    // Checks the contents on the button clicked
    private void OnAnswer(AnswersButton sender)
    {
        ActiveAnswerCheck(sender.pAnswer);

        DialogueManager i = Interactable.currentConvo.managerContents.Find(x => x.nPCdialogue.id == sender.pAnswer.toID);


        if (i == null)
            Close();

        else
        {
            toTalkPanel.text = "";
            CurrentDialogue = Interactable.currentConvo.managerContents.IndexOf(i);
            slowLettersCoroutine = StartCoroutine(SlowLetters(i.nPCdialogue.text));
        }
    }
    private void ActiveAnswerCheck(PlayerAnswer other)
    {
        foreach (PlayerAnswer.Command c in other.command)
            switch (c)
            {
                case PlayerAnswer.Command.Quit:
                    Close();
                    break;

                case PlayerAnswer.Command.GiveItem:
                    foreach (Item i in other.itemsToGive)
                    {
                        PlayerController.Inventory.Add(i);
                    };
                    break;

                case PlayerAnswer.Command.GiveQuest:
                    foreach (Quest q in other.questsToGive)
                    {
                        GameInstance.GameState.QuestController.Add(other.questsToGive[0]);
                    }
                    break;
                case PlayerAnswer.Command.AddMoney:
                    Debug.Log("IMPLEMENT_MONEY");
                    break;
                case PlayerAnswer.Command.SubtractMoney:
                    Debug.Log("IMPLEMENT_MONEY");
                    break;
            }

    }
    private IEnumerator SlowLetters(string other)
    {

        SwitchToConversation();

        for (int i = 0; i < other.Length; i++)
        {
            toTalkPanel.text += (other[i]);
            yield return new WaitForSecondsRealtime(0.15f);
        }
    }
    private void Close()
    {
        if (slowLettersCoroutine != null)
            StopCoroutine(slowLettersCoroutine);
        GameInstance.GameState.Paused = false;
        gameObject.SetActive(false);
    }
    public void SwitchToAnswers()
    {
        if (conversationPanel != null)
            conversationPanel.SetActive(false);

        if (answersPanel != null)
            answersPanel.SetActive(true);
    }

    public void SwitchToConversation()
    {
        if (conversationPanel != null)
            conversationPanel.SetActive(true);

        if (answersPanel != null)
            answersPanel.SetActive(false);
    }
}
