using UnityEngine;
using UnityEngine.UI;

public class PackageUI : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float speed = 600;
    [SerializeField] Transform startPos;
    Transform target;
    Character currentCharacter;
    Package currentPackage;

    InteractableUI interactableUI;

    private void Start()
    {
        interactableUI = GetComponent<InteractableUI>();
        image.gameObject.SetActive(false);
        Queue.OnNext += Queue_OnNextPackage;
        Game.Instance.OnStarted += Game_OnStarted;
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
        if(interactableUI)
            interactableUI.enabled = false;
        target = currentCharacter.UI.transform;
    }

    void GiveCompleted()
    {
        target = null;
        image.gameObject.SetActive(false);
        bool approved = !currentCharacter.FakeIdentification && currentCharacter.PackageId == currentPackage.PackageId;
        Queue.Next(approved);
    }

    private void Game_OnStarted()
    {
        SetPackage(Queue.GetCurrentCharacter(), Queue.GetCurrentPackage());
    }

    private void Queue_OnNextPackage(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        SetPackage(newPair.character, newPair.package);
    }

    private void SetPackage(Character character, Package package)
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
