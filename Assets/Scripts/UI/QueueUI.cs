using System.Collections.Generic;
using UnityEngine;

public class QueueUI : MonoBehaviour
{
    [SerializeField] GameObject characterUIPrefab;
    [SerializeField] Transform[] queuePoints;
    [SerializeField] Transform entrancePoint;
    [SerializeField] Transform exitPoint;
    List<CharacterUI> characterUIs = new List<CharacterUI>();

    private void Awake()
    {
        Queue.OnPairAdded += Queue_PairAdded;
        Queue.OnNext += Queue_OnNext;
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        // TODO?
        if (characterUIs.Count > 0)
        {
            characterUIs[0].targetPoint = exitPoint;
            characterUIs[0].destroyOnReachedTarget = true;
            characterUIs.RemoveAt(0);

            for (int i = 0; i < characterUIs.Count; i++)
            {
                characterUIs[i].targetPoint = queuePoints[i];
            }
        }
    }

    private void Queue_PairAdded(QueuePair newPair)
    {
        if (characterUIs.Count >= queuePoints.Length)
            return;

        Transform point = queuePoints[characterUIs.Count];
        GameObject character = Instantiate(characterUIPrefab, entrancePoint.position, Quaternion.identity, transform);
        var characterUI = character.GetComponent<CharacterUI>();
        characterUI.targetPoint = point;
        characterUIs.Add(characterUI);
    }
}
