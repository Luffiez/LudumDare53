using System.Collections.Generic;
using UnityEngine;

public class QueueUI : MonoBehaviour
{
    public static QueueUI Instance;

    [SerializeField] GameObject characterUIPrefab;
    [SerializeField] Transform[] queuePoints;
    [SerializeField] Transform entrancePoint;
    public Transform exitPoint;
    List<CharacterUI> characterUIs = new List<CharacterUI>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        Queue.OnPairAdded += Queue_PairAdded;
        Queue.OnNext += Queue_OnNext;
        Queue.OnPairRemoved += Queue_OnPairRemoved;
    }

    private void OnDestroy()
    {
        Queue.OnPairAdded -= Queue_PairAdded;
        Queue.OnNext -= Queue_OnNext;
        Queue.OnPairRemoved -= Queue_OnPairRemoved;
    }

    private void Queue_OnPairRemoved(QueuePair pair)
    {
        if (pair?.character?.UI == null)
            return;
        
        pair.character.UI.targetPoint = exitPoint;
        pair.character.UI.destroyOnReachedTarget = true;
        characterUIs.Remove(pair.character.UI);
        UpdateQueuePoints();
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        if (characterUIs.Count > 0)
        {
            characterUIs[0].targetPoint = exitPoint;
            characterUIs[0].destroyOnReachedTarget = true;
            characterUIs.RemoveAt(0);

            UpdateQueuePoints();
        }
    }

    void UpdateQueuePoints()
    {
        for (int i = 0; i < characterUIs.Count; i++)
        {
            characterUIs[i].targetPoint = queuePoints[i];
        }
    }

    private void Queue_PairAdded(QueuePair newPair)
    {
        if (characterUIs.Count >= queuePoints.Length)
            return;

        Transform point = queuePoints[characterUIs.Count];
        GameObject character = Instantiate(characterUIPrefab, entrancePoint.position, Quaternion.identity, transform);
        character.transform.SetAsFirstSibling();
        var characterUI = character.GetComponent<CharacterUI>();
        characterUI.targetPoint = point;
        characterUI.Character = newPair.character;
        characterUIs.Add(characterUI);

        newPair.character.UI = characterUI;
    }

    public void RejectCustomer()
    {
        bool isFake = Queue.GetCurrentCharacter().FakeId;
        if (isFake)
            TextBubble.Instance.Display("How did you know!?");
        else
            TextBubble.Instance.Display("I'll let your manager know about this!!!");

        Queue.Next(isFake);
    }
}
