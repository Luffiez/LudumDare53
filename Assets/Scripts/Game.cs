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
    CharacterIdGenerator characterIdGenerator;

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
    }

    private void Start()
    {
        characterIdGenerator = transform.parent.GetComponentInChildren<CharacterIdGenerator>();
        spriteManager = SpriteManager.instance;
        Queue.OnNext += Queue_OnNext;
        StartCoroutine(DelayedSpawn());

        OnStarted?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Queue.AddPair(GeneratePair());
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
        bool fake = Random.Range(0f, 1f) < fakePercentage;
        characterIdGenerator.GenerateId(character, fake);

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
