using UnityEngine;
using UnityEngine.UI;

public class PackageUI : MonoBehaviour
{
    [SerializeField] Image image;
    private void Start()
    {
        image.enabled = false;
        Queue.OnNext += Queue_OnNextPackage;
        Game.Instance.OnStarted += Game_OnStarted;
    }

    private void Game_OnStarted()
    {
        ShowSprite(Queue.GetCurrentPackage().sprite);
    }

    private void Queue_OnNextPackage(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        ShowSprite(newPair.package.sprite);
    }

    private void ShowSprite(Sprite sprite)
    {
        image.enabled = true;
        image.sprite = sprite;
    }
}
