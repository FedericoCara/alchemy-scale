using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private LevelManager levelManager;
    
    private void Start()
    {
        UpdateLevelNumber();
        levelManager.LevelNumberChanged += UpdateLevelNumber;
    }

    private void UpdateLevelNumber()
    {
        text.text = levelManager.LevelNumber.ToString();
    }
}
