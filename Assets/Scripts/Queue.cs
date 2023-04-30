using System.Collections.Generic;

public static class Queue
{
    public delegate void NextPair(QueuePair oldPair, QueuePair newPair, bool approved);
    public static event NextPair OnNext;

    public delegate void PairAdded(QueuePair newPair);
    public static event PairAdded OnPairAdded;

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
        var oldPair = pair[0];
        pair.RemoveAt(0);
        var newPair = pair[0];
        OnNext?.Invoke(oldPair, newPair, approved);
    }
}
