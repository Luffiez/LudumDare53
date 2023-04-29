using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour
{
    // Start is called before the first frame update
    List<PackageInfromationData> packageInfo = new List<PackageInfromationData>();
    [SerializeField]
    PackageIdGenerator generator;
    [SerializeField]
    CharacterIdGenerator IdGenerator;
    Character tempCharacter = new Character();
    [SerializeField]
    int dataAmount;
    public List<PackageInfromationData> PackageInfo { get { return packageInfo; } }

    private void Start()
    {
        Queue.OnNext += GeneratePackages;
    }

    public void GeneratePackages(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        PackageInfo.Clear();
        GenerateFakePackageIds();
        AddPackageData(oldPair);
    }

    public void GenerateFakePackageIds()
    {
        PackageInfo.Clear();
        for (int i = 0; i < dataAmount; i++)
        {
            PackageInfromationData data = new PackageInfromationData();
            data.PackageId = generator.GeneratePackageId();
            IdGenerator.GenerateId(tempCharacter,false);
            data.PersonId = tempCharacter.PersonIdString;
            int randomStatus = Random.Range(0, 3);
            data.Status = randomStatus == 0 ? randomStatus == 1 ? PackageStatus.Delivered : PackageStatus.NotRetreived : PackageStatus.ReadyToPickUp;
            PackageInfo.Add(data);
        }
    }

    public void AddPackageData(QueuePair pair)
    {
        PackageInfromationData informationData = new PackageInfromationData();
        informationData.PackageId = pair.package.PackageId;
        informationData.PersonId = pair.package.PersonId;
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
        public string PackageId  { get; set; }
        public string PersonId { get; set; }
    }

}
