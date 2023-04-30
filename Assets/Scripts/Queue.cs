using System.Collections.Generic;

public static class Queue
{
    public delegate void NextPair(QueuePair oldPair, QueuePair newPair, bool approved);
    public static event NextPair OnNext;

    public delegate void PairAdded(QueuePair newPair);
    public static event PairAdded OnPairAdded;

    public delegate void CharacterReachedTarget(Character character);
    public static event CharacterReachedTarget OnReachedTarget;

    private static List<QueuePair> pair = new List<QueuePair>();
    public static Character? GetCurrentCharacter() => pair[0]?.character;
    public static Package? GetCurrentPackage() => pair[0]?.package;

    public static void AddPair(QueuePair _pair)
    {
        pair.Add(_pair);
        OnPairAdded?.Invoke(_pair);
    }

    public static void Next(bool approved)
    {
        if (pair.Count == 1)
        {
            pair.RemoveAt(0);
            return;
        }

        var oldPair = pair[0];
        var newPair = pair[1];
        OnNext?.Invoke(oldPair, newPair, approved);
        pair.RemoveAt(0);

    }

    public static void ReachedTarget(this Character character)
    {
        OnReachedTarget?.Invoke(character);
    }
}
