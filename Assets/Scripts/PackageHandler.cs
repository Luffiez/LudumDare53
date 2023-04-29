using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> packageIds = new List<string>(); 
    PackageIdGenerator generator;

    public List<string> PackageIds { get { return packageIds; } set { packageIds = value; } }

    public void GenerateFakePackageIds(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            PackageIds.Add(generator.GeneratePackageId());
        }
    }
}
