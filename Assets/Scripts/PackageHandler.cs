using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour
{
    [SerializeField] int fakePackageCount = 10;

    List<PackageInfromationData> packageInfo = new List<PackageInfromationData>();
    Character tempCharacter = new Character();
    CharacterIdGenerator characterIdGenerator;
    
    public List<PackageInfromationData> PackageInfo { get { return packageInfo; } }

    private void Start()
    {
        Queue.OnNext += Queue_OnNext;
        characterIdGenerator = transform.parent.GetComponentInChildren<CharacterIdGenerator>();
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
            PackageInfromationData data = new PackageInfromationData();

            characterIdGenerator.GenerateId(tempCharacter, true);

            Package fakePackage = new Package()
            {
                fake = true,
                PackageId = PackageIdGenerator.GeneratePackageId(),
                PersonId = tempCharacter?.PackageId,
                sprite = null,
            };

            data.Package = fakePackage; 
            int randomStatus = Random.Range(0, 3);
            data.Status = randomStatus == 0 ? randomStatus == 1 ? PackageStatus.Delivered : PackageStatus.NotRetreived : PackageStatus.ReadyToPickUp;
            PackageInfo.Add(data);
        }
    }

    private void AddPackageData(QueuePair pair)
    {
        PackageInfromationData informationData = new PackageInfromationData();
        informationData.Package = pair.package;

        if (pair.package.fake)
        {
            int random = Random.Range(0, 2);
            informationData.Status = random == 1 ? PackageStatus.Delivered : PackageStatus.NotRetreived;
        }
        else
        {
            informationData.Status = PackageStatus.ReadyToPickUp;
        }
        
        int randomIndex = Random.Range(0, packageInfo.Count);
        packageInfo.Insert(randomIndex, informationData);
    }


    public enum PackageStatus { Delivered, NotRetreived,ReadyToPickUp }

    public class PackageInfromationData
    {
        public PackageStatus Status { get; set; }
        public Package Package { get; set; }
    }

}
