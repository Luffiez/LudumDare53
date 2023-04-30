using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class InteractableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public UnityEvent OnClickEvent;

    Outline outline;
    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    private void OnDisable()
    {
        outline.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
    }
}
