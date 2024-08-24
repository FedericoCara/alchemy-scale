using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;

public class LevelHintDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        UpdateLevelHint();
        levelManager.LevelNumberChanged += UpdateLevelHint;
    }

    private void UpdateLevelHint()
    {
        text.text = levelManager.CurrentLevel.LevelHint;
    }
}
