using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComputerDateSetter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dateText;
    // Start is called before the first frame update
    void Start()
    {
        dateText.text = Game.Instance.CurrentDate.ToString("d");
    }

}
