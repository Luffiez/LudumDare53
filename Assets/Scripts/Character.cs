public class Character
{
    public bool FakeIdentification { get; set; }
    public string PersonIdString { get 
    {
            string birthMonthString = BirthMonth < 10 ? "0" + BirthMonth : BirthMonth.ToString();
            string birthDayString = BirthDay < 10 ? "0" + BirthDay : BirthDay.ToString();
            return $"{BirthYear}{birthMonthString}{birthDayString}-{lastSixNumbers}"; }
    }
    public string SurName { get; set; } = "Hemmingway";
    public string GivenName { get; set; } = "Ernst";
    public int BirthDay { get; set; }
    public int BirthMonth { get; set; }
    public int BirthYear { get; set; }
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
