using UnityEngine;

public class IDCardUI : MonoBehaviour
{
    [SerializeField] float showSpeed = 400f;
    [SerializeField] float returnSpeed = 800f;
    [SerializeField] Transform idImage;
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    Transform target;
    bool isReturn;

    float speed;

    private void Start()
    {
        Queue.OnNext += Queue_OnNext;
        Queue.OnReachedTarget += Queue_OnReachedTarget;
    }

    private void Update()
    {
        if(target == null)
        {
            idImage.gameObject.SetActive(false);
            return;
        }

        if (Vector2.Distance(idImage.transform.position, target.position) > 5f)
        {
            idImage.transform.position = Vector2.MoveTowards(idImage.transform.position, target.position, Time.deltaTime * speed);
            return;
        }

        if (isReturn)
        {
            target = null;
            idImage.transform.position = startPos.position;
            idImage.gameObject.SetActive(false);
        }
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        speed = returnSpeed;
        isReturn = true;
        target = oldPair.character.UI.transform;
    }

    private void Queue_OnReachedTarget(Character character)
    {
        if (character != Queue.GetCurrentCharacter())
            return;

        speed = showSpeed;
        idImage.gameObject.SetActive(true);
        isReturn = false;
        target = endPos;
    }
}
