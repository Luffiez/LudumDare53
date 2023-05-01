using System;
using System.Collections.Generic;
using UnityRandom = UnityEngine.Random;

public static class CharacterIdGenerator
{

    public static List<string> MaleNames = new List<string> { "Liam", "Noah", "Oliver", "John", "David", "Thomas", "Christopher", "Matthew", "Kevin", "Jeffrey", "Larry", "Jerry", "Nathan", "Walter", "Terry", "Joe", "Billy", "Ralph" };
    public static List<string> FemaleNames = new List<string> { "Mary", "Elizabeth", "Susan", "Jessica", "Lisa", "Ashley", "Emily", "Michelle", "Amanda", "Stephanie", "Dorothy", "Sharon", "Cynthia", "Amy", "Anna", "Nicole", "Ruth" };
    public static List<string> Surnames = new List<string> { "The Hedgehog", "Ahmed", "Müller", "Fernández", "Smith", "Martínez", "Longbottom", "Davis", "Lopez", "Wilson", "Anderson", "Gomez", "Evans", "Gordon", "Howard", "Parker", "Abara", "Achebe", "Emem", "Nnadi", "Obama" };
  
    public static void GenerateId(Character character, bool fake)
    {
        DateTime currentDate = Game.Instance.CurrentDate;

        DateTime birthDay = currentDate.
            AddYears(-UnityRandom.Range(15, 84)).
            AddMonths(-UnityRandom.Range(1, 13)).
            AddDays(UnityRandom.Range(1, 32));

        character.BirthDay = birthDay.Day;
        character.BirthMonth = birthDay.Month;
        character.BirthYear = birthDay.Year;
        character.lastSixNumbers = UnityRandom.Range(111111, 999999);
        character.Sex = GetSex(character);
        character.FakeId = fake;

        DateTime expireDate = currentDate.AddMonths(UnityRandom.Range(0,12));
        expireDate = expireDate.AddYears(UnityRandom.Range(0, 3));
        if (expireDate.Month == currentDate.Month && expireDate.Year == currentDate.Year)
            expireDate.AddMonths(1);
        
        if (fake)
        {
            int randomEffect = UnityRandom.Range(0, 1000) % 2;
            switch(randomEffect)
            {
                case 0: ScramblePersonId(birthDay);  break;
                case 1: MakeExpiredDate(); break;
            }
        }

        character.ExpireMonth = expireDate.Month;
        character.ExpireYear = expireDate.Year;
        character.IdBirthDay = birthDay.Day;
        character.IdBirthMonth = birthDay.Month;
        character.IdBirthYear = birthDay.Year;
        character.SurName = Surnames[UnityRandom.Range(0, Surnames.Count)];

        if (character.Sex == Sex.Female)
            character.GivenName = FemaleNames[UnityRandom.Range(0, FemaleNames.Count)];
        else
            character.GivenName = MaleNames[UnityRandom.Range(0, MaleNames.Count)];
    }

    static Sex GetSex(Character character)
    {
        // Get last digit
        int lastDigit = character.lastSixNumbers % 10;
        // If even => female, if odd => male (how we do it in sweden I believe)
        return (lastDigit % 2) == 0 ? Sex.Female : Sex.Male;
    }

    private static DateTime ScramblePersonId(DateTime birthDay)
    {
        int numberOfChanges = UnityRandom.Range(1, 4);
        int startMonth = birthDay.Month;
        int startDay = birthDay.Day;
        int startYear = birthDay.Year;
        for (int i = 0; i < numberOfChanges; i++)
        {
            int randomChange = UnityRandom.Range(0, 3);
            switch (randomChange)
            {
                case 0:
                    int randomdays = UnityRandom.Range(-10, 10);
                    birthDay.AddDays(randomdays == 0 ? 1 : randomdays);
                    if (birthDay.Day == startDay)
                    {
                        birthDay.AddDays(-1);
                    }
                    break;
                case 1:
                    int randomMonth = UnityRandom.Range(-3, 3);
                    birthDay.AddMonths(randomMonth == 0 ? 1 : randomMonth);
                    if (birthDay.Month == startMonth)
                    {
                        birthDay.AddMonths(-1);
                    }
                    break;
                case 2:
                    int randomYear = UnityRandom.Range(-1, 1);
                    birthDay.AddYears(randomYear == 0 ? 1 : randomYear);
                    if (startYear == birthDay.Year)
                    {
                        birthDay.AddYears(-1);
                    }
                    break;
            }
        }
        return birthDay;
    }

    private static DateTime MakeExpiredDate()
    {
       return Game.Instance.CurrentDate.AddMonths(-UnityRandom.Range(1, 12));
    }
}
