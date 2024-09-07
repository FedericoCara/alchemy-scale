using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class Glossary
{
    private Dictionary<Ingredient, bool> unlockedIngrediensts = new();
    private Dictionary<Ingredient, bool> unlockedWeights = new();
    private Dictionary<Sinergy, bool> unlockedSynergies = new();
    private Dictionary<int, bool> unlockedTutorials = new();

    public bool IsUnlocked(Ingredient ingredient)
    {
        return unlockedIngrediensts.ContainsKey(ingredient) && unlockedIngrediensts[ingredient];
    }

    public void Unlock(Ingredient ingredient)
    {
        unlockedIngrediensts[ingredient] = true;
    }
    
    public bool IsUnlockedWeight(Ingredient ingredient)
    {
        return unlockedWeights.ContainsKey(ingredient) && unlockedWeights[ingredient];
    }
    
    public void UnlockWeight(Ingredient ingredient)
    {
        unlockedWeights[ingredient] = true;
    }
    
    public bool IsUnlocked(Sinergy synergy)
    {
        return unlockedSynergies.ContainsKey(synergy) && unlockedSynergies[synergy];
    }
    
    public void Unlock(Sinergy synergy)
    {
        unlockedSynergies[synergy] = true;
    }
    public bool IsUnlocked(int tutorial)
    {
        return unlockedTutorials.ContainsKey(tutorial) && unlockedTutorials[tutorial];
    }

    public void Unlock(int tutorial)
    {
        unlockedTutorials[tutorial] = true;
    }
}
