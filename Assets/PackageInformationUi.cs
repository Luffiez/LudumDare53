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

    Package package;

    // Start is called before the first frame update
    public void SetInformation(Package package)
    {
        this.package = package;
        StatusText.text = package.Status.ToString();
        PersonIdText.text = package.PersonId;
        PackageIdText.text = package.PackageId;
        PackageId = package.PackageId;
    }

    public void DisableButton()
    {
        Button.SetActive(false);
    }

    public void FetchPackage()
    {
        PackageUI.Instance.SetPackage(Queue.GetCurrentCharacter(), package);
        PackageHandler.Instance.DisablePackageView();
    }
}
