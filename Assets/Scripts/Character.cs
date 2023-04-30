public class Character
{
    public bool FakeIdentification { get; set; }
    public string PersonIdString { get 
    {
            string birthMonthString = IdBirthMonth < 10 ? "0" + IdBirthMonth : IdBirthMonth.ToString();
            string birthDayString = IdBirthDay < 10 ? "0" + IdBirthDay : IdBirthDay.ToString();
            return $"{IdBirthYear}{birthMonthString}{birthDayString}-{lastSixNumbers}"; }
    }
    public string SurName { get; set; } = "Hemmingway";
    public string GivenName { get; set; } = "Ernst";
    public int BirthDay { get; set; }
    public int BirthMonth { get; set; }
    public int BirthYear { get; set; }
    public int IdBirthDay { get; set; }
    public int IdBirthMonth { get; set; }
    public int IdBirthYear { get; set; }
    public int ExpireYear { get; set; }
    public int ExpireMonth { get; set; }
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
