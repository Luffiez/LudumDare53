using UnityEngine;
using TMPro;
public class ComputerDateSetter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dateText;

    void Start()
    {
        dateText.text = "Date: " + Game.Instance.CurrentDate.ToString("d");
    }

}
