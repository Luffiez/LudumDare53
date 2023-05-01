using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] int health = 5;
    [SerializeField] int queueStartSize = 3;
    [SerializeField] int queueMaxSize = 3;
    [SerializeField] float spawnDelay = 0.3f;
    [SerializeField] float fakePercentage = 0.3f;
    [SerializeField] float minCharacterPatience = 60f;
    [SerializeField] float maxCharacterPatience = 120f;
    [SerializeField] float maxCharacterScore = 100;


    [HideInInspector] public int score = 0;

    public delegate void GameStarted();
    public event GameStarted OnStarted;
    public event GameStarted OnGameOver;

    public delegate void HealthUpdated(int current);
    public event HealthUpdated OnHealthUpdated;

    SpriteManager spriteManager;
    public DateTime CurrentDate = new DateTime(1, 1, 1);

    private void OnDestroy()
    {
        Queue.OnNext -= Queue_OnNext;
        Queue.OnPairRemoved -= Queue_OnPairRemoved;
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
        Queue.OnPairRemoved += Queue_OnPairRemoved;
       
        StartCoroutine(DelayedSpawn());

        OnStarted?.Invoke();
    }

    private void Queue_OnPairRemoved(QueuePair newPair)
    {
        Queue.AddPair(GeneratePair());
        ReduceHealth();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeclineCustomer();
        }
    }
#endif

    public bool IsTheif()
    {
        var character = Queue.GetCurrentCharacter();
        var package = Queue.GetCurrentPackage();
        return character.FakeId ||package.IsFake;
    }

    public void DeclineCustomer()
    {
        // If thief, we declined him succesfully!
        Queue.TryNext(IsTheif());
    }

    IEnumerator DelayedSpawn()
    {
        for (int i = 0; i < queueStartSize; i++)
        {
            Queue.AddPair(GeneratePair());
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public QueuePair GeneratePair()
    {
        // Character
        var character = new Character();
        bool fake = UnityEngine.Random.Range(0f, 1f) < fakePercentage;
        bool packageFake = UnityEngine.Random.Range(0f, 1f) < fakePercentage;
        CharacterIdGenerator.GenerateId(character, fake);

        character.StartPatience = UnityEngine.Random.Range(minCharacterPatience, maxCharacterPatience);
        character.Patience = character.StartPatience;
        // Package
        var package = new Package()
        {
            IsFake = packageFake,
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
        // Characters with lower patience give a higher score
        // complete order faster to achieve even higher score

        if (approved)
            score += Mathf.RoundToInt((maxCharacterScore - oldPair.character.StartPatience / 2) * oldPair.character.PatiencePercentage);
        else
            ReduceHealth();

        if(Queue.Count < queueMaxSize)
            Queue.AddPair(GeneratePair());
    }

    private void ReduceHealth()
    {
        health--;
        OnHealthUpdated?.Invoke(health);
        if (health <= 0)
        {
            Queue.ClearQueue();
            Time.timeScale = 0;
            OnGameOver?.Invoke();
        }
    }
}
