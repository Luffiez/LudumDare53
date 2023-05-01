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
    
    public static PackageHandler Instance;

    public List<Package> PackageInfo { get { return packageInfo; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        Game.Instance.OnStarted += OnGameStarted;
        Queue.OnNext += Queue_OnNext;
        Queue.OnPairRemoved += Queue_OnPairRemoved;

    }

    private void OnDestroy()
    {
        Game.Instance.OnStarted -= OnGameStarted;
        Queue.OnNext -= Queue_OnNext;
        Queue.OnPairRemoved -= Queue_OnPairRemoved;
    }
    private void Queue_OnPairRemoved(QueuePair newPair)
    {
        UpdatePackageList(Queue.GetCurrentPair());
    }

    private void OnGameStarted()
    {
        Queue_OnNext(null, new QueuePair(Queue.GetCurrentCharacter(), Queue.GetCurrentPackage()), true);
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        UpdatePackageList(newPair);
    }

    private void UpdatePackageList(QueuePair newPair)
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
            packageInformationUi.SetInformation(PackageInfo[i]);
            if (PackageInfo[i].Status == PackageStatus.Delivered)
            {
                packageInformationUi.DisableButton();
            }
        }
        PackageInfo.Clear();
    }

    private void GenerateFakePackageIds()
    {
        return;
        PackageInfo.Clear();
        for (int i = 0; i < PackageCount; i++)
        {
            CharacterIdGenerator.GenerateId(tempCharacter, true);

            Package fakePackage = new Package()
            {
                IsFake = true,
                PackageId = PackageIdGenerator.GeneratePackageId(),
                PersonId = tempCharacter?.PersonIdString,
                sprite = SpriteManager.instance.GetRandomPackageSprite(),
            };
            int randomStatus = Random.Range(0, 2);
            fakePackage.Status = randomStatus == 0 ?  PackageStatus.Delivered : PackageStatus.ReadyToPickUp;
            PackageInfo.Add(fakePackage);
        }
    }

    private void AddPackageData(QueuePair pair)
    {
        if (pair.package.IsFake)
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
        PlayRandomComputerSound();
        packageView.SetActive(true);
    }

    public void DisablePackageView()
    {
        PlayRandomComputerSound();
        packageView.SetActive(false);
    }


    public void PlayRandomComputerSound()
    {
        int soundtype = Random.Range(0, 5) +1;
        SoundManager.Instance.PlaySound($"Computer{soundtype}", 1);
    }

    public void ClearPackageUi()
    {
        for(int i = packageUIContent.transform.childCount-1; i >= 0; i--)
        {
            Destroy(packageUIContent.transform.GetChild(i).gameObject);
        }
    }
}
