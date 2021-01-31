using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombinationDatabase : MonoBehaviour
{
    [SerializeField] List<Combination> combinations; 
    
    public Combination FindCombination(List<InputPatterns> patterns)
    {
        return combinations.Find(comb => comb.patterns.SequenceEqual(patterns));
    }
}
