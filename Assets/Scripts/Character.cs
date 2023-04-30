public class Character
{
    public bool FakeIdentification { get; set; }
    public string PersonIdString { get { return $"{BirthYear}{BirthMonth}{BirthDay}-{lastSixNumbers}"; } }
    public int PersonId { get; set; }
    public string SurName { get; set; }
    public string GivenName { get; set; }
    public int BirthDay { get; set; }
    public int BirthMonth { get; set; }
    public int BirthYear { get; set; }
    public int ExpireYear;
    public int ExpireMonth;
    public int lastSixNumbers { get; set; }
    public string PackageId { get; set; }
    public Sex Sex { get; set; }

    public CharacterUI UI { get; set; }
}

public enum Sex 
{ 
    Male, 
    Female 
}
