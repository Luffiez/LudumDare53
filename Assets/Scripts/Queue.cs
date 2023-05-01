using System.Collections.Generic;
using UnityEngine;

public static class Queue
{
    public delegate void NextPair(QueuePair oldPair, QueuePair newPair, bool approved);
    public static event NextPair OnNext;

    public delegate void PairAdded(QueuePair newPair);
    public static event PairAdded OnPairAdded;
    public static event PairAdded OnPairRemoved;

    public delegate void CharacterReachedTarget(Character character);
    public static event CharacterReachedTarget OnReachedTarget;

    static List<QueuePair> pair = new List<QueuePair>();
    public static Character? GetCurrentCharacter() => pair[0]?.character;
    public static Package? GetCurrentPackage() => pair[0]?.package;
    public static QueuePair? GetCurrentPair() => pair[0];

    public static QueuePair? GetPairFor(Character character) => pair.Find(pair => pair.character == character);
    public static QueuePair? GetPairFor(Package package) => pair.Find(pair => pair.package == package);
    public static void ClearQueue() =>  pair.Clear();
    public static int Count => pair.Count;
    public static void AddPair(QueuePair _pair)
    {
        pair.Add(_pair);
        OnPairAdded?.Invoke(_pair);
    }

    public static void RemovePair(QueuePair _pair)
    {
        pair.Remove(_pair);
        OnPairRemoved?.Invoke(_pair);
    }

    public static void Next(bool approved)
    {
        var oldPair = pair[0];
        if (pair.Count == 1)
        {
            Debug.Log("Only One, remove it only.");
            pair.RemoveAt(0);
            OnNext?.Invoke(oldPair, null, approved);

            return;
        }

        var newPair = pair[1];
        OnNext?.Invoke(oldPair, newPair, approved);
        pair.RemoveAt(0);
    }

    public static void ReachedTarget(this Character character)
    {
        Debug.Log("Reached Target!");
        OnReachedTarget?.Invoke(character);
    }
}
