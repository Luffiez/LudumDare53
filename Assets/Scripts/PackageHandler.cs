using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour
{
    [SerializeField] int PackageCount = 10;
    [SerializeField] GameObject packageUiPrefab;
    [SerializeField] GameObject packageUIContent;
    [SerializeField] GameObject packageView;
    List<Package> packageInfo = new List<Package>();
    Character tempCharacter = new Character();
    
    public List<Package> PackageInfo { get { return packageInfo; } }

    private void Start()
    {
        Game.Instance.OnStarted += OnGameStarted;
        Queue.OnNext += Queue_OnNext;
    }

    private void OnGameStarted()
    {
        Queue_OnNext(null, new QueuePair(Queue.GetCurrentCharacter(), Queue.GetCurrentPackage()), true);
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        ClearPackageUi();
        GenerateFakePackageIds();
        AddPackageData(newPair);
        GameObject startObject = Instantiate(packageUiPrefab, packageUIContent.transform);
        startObject.GetComponent<PackageInformationUi>().DisableButton();
        for (int i = 0; i < PackageInfo.Count; i++)
        {
            GameObject uiObject = Instantiate(packageUiPrefab, packageUIContent.transform);
            PackageInformationUi packageInformationUi = uiObject.GetComponent<PackageInformationUi>();
            packageInformationUi.SetInformation(PackageInfo[i].PersonId, PackageInfo[i].PackageId,PackageInfo[i].Status.ToString());
            if (PackageInfo[i].Status == PackageStatus.Delivered)
            {
                packageInformationUi.DisableButton();
            }
        }
        PackageInfo.Clear();
    }

    private void GenerateFakePackageIds()
    {

        PackageInfo.Clear();
        for (int i = 0; i < PackageCount; i++)
        {
            CharacterIdGenerator.GenerateId(tempCharacter, true);

            Package fakePackage = new Package()
            {
                fake = true,
                PackageId = PackageIdGenerator.GeneratePackageId(),
                PersonId = tempCharacter?.PersonIdString,
                sprite = null,
            };
            int randomStatus = Random.Range(0, 2);
            fakePackage.Status = randomStatus == 0 ?  PackageStatus.Delivered : PackageStatus.ReadyToPickUp;
            PackageInfo.Add(fakePackage);
        }
    }

    private void AddPackageData(QueuePair pair)
    {
        if (pair.package.fake)
        {
            pair.package.Status =  PackageStatus.Delivered;
        }
        else
        {
            pair.package.Status = PackageStatus.ReadyToPickUp;
        }
        
        int randomIndex = Random.Range(0, packageInfo.Count);
        packageInfo.Insert(randomIndex, pair.package);
    }

    public void EnablePackageView()
    {
        packageView.SetActive(true);
    }

    public void DisablePackageView()
    {
        packageView.SetActive(false);
    }

    public void ClearPackageUi()
    {
        for(int i = packageUIContent.transform.childCount-1; i >= 0; i--)
        {
            Destroy(packageUIContent.transform.GetChild(i).gameObject);
        }
    }
}
