using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using UnityRandom = UnityEngine.Random; 

public class CharacterIdGenerator : MonoBehaviour
{

    DateTime CurrentDate = new DateTime(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {

       
        CurrentDate = CurrentDate.AddYears(UnityRandom.Range(2011, 2019));
        CurrentDate = CurrentDate.AddMonths(UnityRandom.Range(1, 12));
        CurrentDate = CurrentDate.AddDays(UnityRandom.Range(1, 31));
        Debug.Log(CurrentDate);
        IdData idData = gameObject.AddComponent<IdData>();

      

    }

    public void GenerateId(Character character, bool fake)
    {
        DateTime birthDay = CurrentDate.AddYears(-UnityRandom.Range(15, 84)).AddMonths(-UnityRandom.Range(1, 13)).AddDays(UnityRandom.Range(1, 32));

        character.BirthDay = birthDay.Day;
        character.BirthMonth = birthDay.Month;
        character.BirthYear = birthDay.Year;
        character.BirthDay = birthDay.Day;
        character.lastSixNumbers = UnityRandom.Range(111111, 999999);
        if (fake)
        {
            int numberOfChanges = UnityRandom.Range(1, 4);
            int startMonth = birthDay.Month;
            int startDay = birthDay.Day;
            int startYear = birthDay.Year;
            for (int i = 0; i < numberOfChanges; i++)
            {
                int randomChange = UnityRandom.Range(0,3);
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
                        birthDay.AddYears(randomYear == 0 ? 1:randomYear);
                        if (startYear == birthDay.Year)
                        {
                            birthDay.AddYears(-1);
                        }
                    break;
                }
            }
        }
        character.PersonId = birthDay.Day + birthDay.Month * 100 + birthDay.Year * 10000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
