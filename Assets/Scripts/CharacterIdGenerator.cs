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

    public void GenerateId(Character character)
    {
        DateTime BirthDay = CurrentDate.AddYears(-UnityRandom.Range(15, 84)).AddMonths(-UnityRandom.Range(1, 13)).AddDays(UnityRandom.Range(1, 32));

        character.BirthDay = BirthDay.Day;
        character.BirthMonth = BirthDay.Month;
        character.BirthYear = BirthDay.Year;
        character.BirthDay = BirthDay.Day;
        character.lastSixNumbers = UnityRandom.Range(111111, 999999);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
