using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "AlchemyScale/Ingredient")]
    public class Ingredient : ScriptableObject
    {
        [SerializeField] private int weight;
        [SerializeField] private List<Sinergy> sinergies;
    }
}