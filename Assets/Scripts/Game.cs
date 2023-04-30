using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] int queueStartSize = 3;
    [SerializeField] int queueMaxSize = 3;
    [SerializeField] float spawnDelay = 0.3f;
    [SerializeField] float fakePercentage = 0.3f;

    [HideInInspector] public int score = 0;

    public delegate void GameStarted();
    public GameStarted OnStarted;

    SpriteManager spriteManager;
    public DateTime CurrentDate = new DateTime(1, 1, 1);

    private void OnDestroy()
    {
        Queue.OnNext -= Queue_OnNext;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }

        CurrentDate = CurrentDate.AddYears(UnityEngine.Random.Range(2011, 2019));
        CurrentDate = CurrentDate.AddMonths(UnityEngine.Random.Range(1, 12));
        CurrentDate = CurrentDate.AddDays(UnityEngine.Random.Range(1, 31));
    }

    private void Start()
    {
        spriteManager = SpriteManager.instance;
        Queue.OnNext += Queue_OnNext;
        StartCoroutine(DelayedSpawn());

        OnStarted?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Queue.Next(true);
        }
    }

    IEnumerator DelayedSpawn()
    {
        for (int i = 0; i < queueStartSize; i++)
        {
            Queue.AddPair(GeneratePair());
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    QueuePair GeneratePair()
    {
        // Character
        var character = new Character();
        bool fake = UnityEngine.Random.Range(0f, 1f) < fakePercentage;
        CharacterIdGenerator.GenerateId(character, fake);

        // Package
        var package = new Package()
        {
            fake = fake,
            PackageId = PackageIdGenerator.GeneratePackageId(),
            PersonId = character.PersonIdString,
            sprite = spriteManager.GetRandomPackageSprite()
        };

        // Link package to character
        character.PackageId = package.PackageId;

        return new QueuePair(character, package);
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        if (approved)
            score++;
        else
            score--;

        Queue.AddPair(GeneratePair());
    }
}
