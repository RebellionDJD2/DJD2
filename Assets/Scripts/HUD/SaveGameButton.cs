﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

sealed public class SaveGameButton : MonoBehaviour
{
    public delegate void EventHandler(SaveGameButton sender);

    public event EventHandler   OnOverride,
                                OnDelete,
                                OnLoad;

    [SerializeField] private Text savedGameTitleLabel = null;
    [SerializeField] private Text savedGameDateLabel = null;

    public System.IO.FileInfo FileInfo { get; private set; }

    public void Initialize (System.IO.FileInfo fileInfo)
    {
        FileInfo = fileInfo;
        savedGameTitleLabel.text = "Name : " + 
            fileInfo.Name.Replace(fileInfo.Extension, "");
        savedGameDateLabel.text = "Last Modified : " + 
            fileInfo.LastWriteTimeUtc.ToString();
    }

    public void OnOverrideClick()
    {
        OnOverride?.Invoke(this);
    }

    public void OnDeleteClick()
    {
        OnDelete?.Invoke(this);
    }

    public void OnLoadClick ()
    {
        OnLoad?.Invoke(this);
    }
}
