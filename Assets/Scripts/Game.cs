using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] int queueStartSize = 3;
    [SerializeField] int queueMaxSize = 3;
    [SerializeField] float spawnDelay = 0.3f;

    [HideInInspector] public int score = 0;

    private void OnDestroy()
    {
        Queue.OnNext -= Queue_OnNext;
    }

    private void Start()
    {
        Queue.OnNext += Queue_OnNext;
        StartCoroutine(DelayedSpawn());
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
        // TODO: Generate properly
        return new QueuePair(new Character(), new Package());
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
