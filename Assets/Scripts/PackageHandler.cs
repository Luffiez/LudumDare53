using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour
{
    [SerializeField] int fakePackageCount = 10;

    List<Package> packageInfo = new List<Package>();
    Character tempCharacter = new Character();
    
    public List<Package> PackageInfo { get { return packageInfo; } }

    private void Start()
    {
        Queue.OnNext += Queue_OnNext;
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        PackageInfo.Clear();
        GenerateFakePackageIds();
        AddPackageData(newPair);
    }

    private void GenerateFakePackageIds()
    {
        PackageInfo.Clear();
        for (int i = 0; i < fakePackageCount; i++)
        {
            CharacterIdGenerator.GenerateId(tempCharacter, true);

            Package fakePackage = new Package()
            {
                fake = true,
                PackageId = PackageIdGenerator.GeneratePackageId(),
                PersonId = tempCharacter?.PackageId,
                sprite = null,
            };

            int randomStatus = Random.Range(0, 3);
            fakePackage.Status = randomStatus == 0 ? randomStatus == 1 ? PackageStatus.Delivered : PackageStatus.NotRetreived : PackageStatus.ReadyToPickUp;
            PackageInfo.Add(fakePackage);
        }
    }

    private void AddPackageData(QueuePair pair)
    {
        if (pair.package.fake)
        {
            int random = Random.Range(0, 2);
            pair.package.Status = random == 1 ? PackageStatus.Delivered : PackageStatus.NotRetreived;
        }
        else
        {
            pair.package.Status = PackageStatus.ReadyToPickUp;
        }
        
        int randomIndex = Random.Range(0, packageInfo.Count);
        packageInfo.Insert(randomIndex, pair.package);
    }
}
