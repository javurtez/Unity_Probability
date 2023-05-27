using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class Probability : MonoBehaviour
{
    [SerializeField]
    [Probability("items")]
    float[] probability;
    [SerializeField]
    string[] items;

    [Button]
    void Start()
    {
        var result = GetProbability();

        Debug.Log(result);
    }

    /// <summary>
    /// Obtained according to the odds
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">Obj</param>
    /// <param name="prob">機率</param>
    /// <returns></returns>
    private (bool, T) ProbabilityObject<T>(T[] obj, float[] prob)
    {
        if (obj.Length == 1)
        {
            return ((obj[0] != null), obj[0]);
        }

        if (obj.Length == prob.Length + 1)
        {
            List<int> tempRanges = (from value in prob select (int)(value * 100)).ToList();
            tempRanges.Add(100);
            int randomValue = Random.Range(0, 100);

            if (prob.Length == 0)
            {
                return ((obj[0] != null), default);
            }

            for (int i = 0; i < tempRanges.Count; i++)
            {
                if (randomValue <= tempRanges[i])
                {
                    return ((obj[i] != null), obj[i]);
                }

            }
        }

        return (false, default);
    }

    /// <summary>
    /// Gets the probability
    /// </summary>
    /// <returns></returns>
    public string GetProbability()
    {
        bool result;
        string resultProbability;

        (result, resultProbability) = ProbabilityObject(items, probability);

        return resultProbability;
    }
}
