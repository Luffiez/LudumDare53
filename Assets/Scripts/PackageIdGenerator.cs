using System.Collections.Generic;
using UnityEngine;

public static class PackageIdGenerator
{
    const string characterList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#%?";

    public static List<string> packageIds = new List<string>();
    //Dictionary<string,int> DuplicateChecker =  new Dictionary<string, int>();

    public static string GeneratePackageId()
    {
        string newPackageId = "";
        for (int i = 0; i < 6; i++)
        {
            newPackageId += characterList[Random.Range(0, characterList.Length)];
        }

        packageIds.Add(newPackageId);    
        return newPackageId;
    }
}
