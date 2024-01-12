using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public static class Utils
{
    public static float NormalizedValue(this float value)
    {
        return value * 0.01f;
    }

    public static T GetRandomElement<T>(this IList<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count - 1)];
    }

    public static float CalculateTypingTime(int characterCount)
    {
        float typingTime;
        if (characterCount <= 50)
        {
            typingTime = 1f;
        }
        else if (characterCount <= 150)
        {
            typingTime = 2f;
        }
        else
        {
            typingTime = characterCount / 30f;
        }

        return typingTime;
    }
    public static bool IsNullOrEmpty(this IEnumerable @this)
        => !(@this?.GetEnumerator().MoveNext() ?? false);
    
    public static void Shuffle<T>(this IList<T> list)
    {
        var shuffledList = list.OrderBy(x => new Random().Next()).ToList();
        list.Clear();
        list.AddRange(shuffledList);
    }
}