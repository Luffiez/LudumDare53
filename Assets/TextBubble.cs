using System.Collections;
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    [SerializeField] float bubbleDisplayDuration = 5f;
    [SerializeField] float bubbleScaleDuration = 0.5f;
    [SerializeField] GameObject bubble;
    [SerializeField] TMP_Text bubbleText;
    public static TextBubble Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);  
    }

    private void Start()
    {
        Queue.OnReachedTarget += Queue_OnReachedTarget;
        Queue.OnNext += Queue_OnNext;
    }

    private void OnDestroy()
    {
        Queue.OnReachedTarget -= Queue_OnReachedTarget;
        Queue.OnNext -= Queue_OnNext;
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        //if (bubble.activeSelf)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(Hide(bubbleDisplayDuration/2));
        //}
    }

    private void Queue_OnReachedTarget(Character character)
    {
        if (character == Queue.GetCurrentCharacter())
            Display("My package id is: " + character.PackageId);
    }

    public void Display(string text)
    {
        StopAllCoroutines();
        bubbleText.text = text;
        bubble.SetActive(true);
        StartCoroutine(Scale(Vector3.zero, Vector3.one, bubbleScaleDuration));
        StartCoroutine(Hide(bubbleDisplayDuration));
    }

    IEnumerator Scale(Vector3 from, Vector3 to, float duration)
    {
        bubble.transform.localScale = from;

        float elapsed = 0;
        while (elapsed < duration)
        {
            bubble.transform.localScale = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        bubble.transform.localScale = to;
    }

    IEnumerator Hide(float duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(Scale(Vector3.one, Vector3.zero, bubbleScaleDuration));
        yield return new WaitForSeconds(bubbleScaleDuration);
        bubble.SetActive(false);
    }
}
