using System.Collections.Generic;

public static class Queue
{
    public delegate void NextPair(QueuePair oldPair, QueuePair newPair, bool approved);
    public static NextPair OnNext;

    public delegate void PairAdded(QueuePair newPair);
    public static PairAdded OnPairAdded;

    private static List<QueuePair> pair = new List<QueuePair>();
    public static Character? GetCurrentCharacter() => pair[0]?.character;
    public static Package? GetCurrentPackage() => pair[0]?.package;

    public static void AddPair(QueuePair _pair)
    {
        pair.Add(_pair);
        OnPairAdded(_pair);
    }

    public static void Next(bool approved)
    {
        var oldPair = pair[0];
        pair.RemoveAt(0);
        var newPair = pair[0];
        OnNext?.Invoke(oldPair, newPair, approved);
    }
}

public class QueuePair
{
    public Character character;
    public Package package;

    public QueuePair(Character character, Package package)
    {
        this.character = character;
        this.package = package;
    }
}

public class Character
{
    public bool FakeIdentification { get; set; }
    public string PersonId { get { return $"{BirthYear}{BirthMonth}{BirthDay}-{lastSixNumbers}"; } }
    public string SurName { get; set; }
    public string GivenName { get; set; }
    public int BirthDay { get; set; }

    public int BirthMonth { get; set; }

    public int BirthYear { get; set; }

    public int ExpireYear;
    public int ExpireMonth;

    public string PackageId { get; set; }

    public int lastSixNumbers { get; set; }
}

public class Package
{
    public bool fake { get; set; }
    public string PersonId { get; set; }
    public string PackageId { get; set; }
    //Parcel ID
}
