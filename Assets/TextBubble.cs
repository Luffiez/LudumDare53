using System.Collections;
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    [SerializeField] float bubbleDisplayDuration = 5f;
    [SerializeField] float bubbleScaleDuration = 0.5f;
    [SerializeField] GameObject bubble;
    [SerializeField] TMP_Text bubbleText;

    private void Start()
    {
        Queue.OnReachedTarget += Queue_OnReachedTarget;
        Queue.OnNext += Queue_OnNext;
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        if (bubble.activeSelf)
        {
            StopAllCoroutines();
            bubble.SetActive(false);
        }
    }

    private void Queue_OnReachedTarget(Character character)
    {
        if (character == Queue.GetCurrentCharacter())
            Display("My packade id is: " + character.PackageId);
    }

    public void Display(string text)
    {
        bubbleText.text = text;
        bubble.SetActive(true);
        StopAllCoroutines();
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
        Scale(Vector3.one, Vector3.zero, bubbleScaleDuration);
        yield return new WaitForSeconds(bubbleScaleDuration);
        bubble.SetActive(false);
    }
}
