using System.Collections;
using TMPro;
using UnityEngine;

public class AttempsText : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    [SerializeField] TMP_Text text;

    void Start()
    {
        Game.Instance.OnHealthUpdated += Instance_OnHealthUpdated;        
    }

    private void Instance_OnHealthUpdated(int current)
    {
        StopAllCoroutines();
        bubble.SetActive(true);
        text.text = $"Manager: {current} more failures and \nYOU'RE FIRED!";
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(3);
        bubble.SetActive(false);
    }
}
