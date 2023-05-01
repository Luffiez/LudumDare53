using System;
using UnityEngine;
using UnityEngine.UI;

public class PackageUI : MonoBehaviour
{
    public static PackageUI Instance { get; private set; }
    [SerializeField] Image image;
    [SerializeField] float speed = 600;
    [SerializeField] Transform startPos;
    Transform target;
    Character currentCharacter;
    Package currentPackage;

    InteractableUI interactableUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        interactableUI = GetComponent<InteractableUI>();
        image.gameObject.SetActive(false);
        Queue.OnNext += Queue_OnNext;
        Queue.OnPairRemoved += Queue_OnPairRemoved;
    }

    private void OnDestroy()
    {
        Queue.OnNext -= Queue_OnNext;
        Queue.OnPairRemoved -= Queue_OnPairRemoved;
    }

    private void Queue_OnPairRemoved(QueuePair newPair)
    {
        image.gameObject.SetActive(false);
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        image.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (target == null)
            return;

        if (Vector2.Distance(image.transform.position, target.position) > 5f)
        {
            image.transform.position = Vector2.MoveTowards(image.transform.position, target.position, Time.deltaTime * speed);
            return;
        }

        GiveCompleted();
    }

    public void GivePackage()
    {
        if (!image.gameObject.activeSelf)
            return;

        if (currentPackage.PackageId != currentCharacter.PackageId)
        {
            TextBubble.Instance.Display("That's not my package!");
            currentCharacter.Patience -= currentCharacter.StartPatience * 0.3f;
            return;
        }

        if(interactableUI)
            interactableUI.enabled = false;
        target = currentCharacter.UI.transform;
    }

    void GiveCompleted()
    {
        target = null;
        image.gameObject.SetActive(false);
        bool approved = !currentCharacter.FakeId && !currentPackage.IsFake;
        if (currentCharacter.FakeId)
            TextBubble.Instance.Display("GOT YOU, hehehe!");

        Queue.Next(approved);
    }

    public void SetPackage(Character character, Package package)
    {
        if (interactableUI)
            interactableUI.enabled = true;

        currentCharacter = character;
        currentPackage = package;
        image.transform.position = startPos.position;
        image.sprite = package.sprite;
        image.gameObject.SetActive(true);
    }
}
