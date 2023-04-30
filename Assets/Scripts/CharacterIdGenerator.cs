using System;
using UnityRandom = UnityEngine.Random;

public static class CharacterIdGenerator
{
    public static void GenerateId(Character character, bool fake)
    {
        DateTime birthDay = Game.Instance.CurrentDate.
            AddYears(-UnityRandom.Range(15, 84)).
            AddMonths(-UnityRandom.Range(1, 13)).
            AddDays(UnityRandom.Range(1, 32));

        character.BirthDay = birthDay.Day;
        character.BirthMonth = birthDay.Month;
        character.BirthYear = birthDay.Year;
        character.BirthDay = birthDay.Day;
        character.lastSixNumbers = UnityRandom.Range(111111, 999999);
        character.Sex = GetSex(character);

        if (fake)
            Scramble(birthDay);
    }

    static Sex GetSex(Character character)
    {
        // Get last digit
        int lastDigit = character.lastSixNumbers % 10;
        // If even => female, if odd => male (how we do it in sweden I believe)
        return (lastDigit % 2) == 0 ? Sex.Female : Sex.Male;
    }

    private static void Scramble(DateTime birthDay)
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
    }
}
