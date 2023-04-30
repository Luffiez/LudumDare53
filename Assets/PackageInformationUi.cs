using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PackageInformationUi : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI PersonIdText;
    [SerializeField]
    TextMeshProUGUI PackageIdText;
    [SerializeField]
    TextMeshProUGUI StatusText;
    [SerializeField]
    GameObject Button;
    public string PackageId { get; set; }

    // Start is called before the first frame update
    public void SetInformation(string personId, string packageId, string status)
    {
        StatusText.text = status;
        PersonIdText.text = personId;
        PackageIdText.text = packageId;
        PackageId = packageId;
    }

    public void DisableButton()
    {
        Button.SetActive(false);
    }
}
