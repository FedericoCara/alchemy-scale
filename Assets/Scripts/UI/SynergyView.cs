using System.Collections;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SynergyView : MonoBehaviour
{
    [SerializeField] private TMP_Text firstPartText;
    [SerializeField] private TMP_Text lastPartText;
    [SerializeField] private Image targetIngredientImage;
    
    private Sinergy synergy;
    
    public void Init(Sinergy synergy)
    {
        this.synergy = synergy;
        firstPartText.text = $">{this.synergy.amount} {(synergy.repeatable?"R":"")} {synergy.ingredient.name}";
        lastPartText.text = $" -> {this.synergy.weightAdded}p";
    }
}
