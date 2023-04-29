using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageIdGenerator : MonoBehaviour
{

    public List<string> PackageId;

    const string characterList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#%?";
    Dictionary<string,int> DuplicateChecker =  new Dictionary<string, int>();
    // Start is called before the first frame update



    public string GeneratePackageId()
    {
        string newPackageId = "";
        for (int i = 0; i < 6; i++)
        {
            newPackageId = newPackageId + characterList[Random.Range(0, characterList.Length)];
        }
        return newPackageId;
    }
    
}
