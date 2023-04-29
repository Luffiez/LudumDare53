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

    public string PackageId { get; set; }

    public int lastSixNumbers { get; set; }
}
