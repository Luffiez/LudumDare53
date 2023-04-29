using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdData : MonoBehaviour
{
    public bool FakeIdentification { get; set; }
    public string PersonId { get; set; }
    public string SurName { get; set; }
    public string GivenName { get; set; }
    public int BirthDay { get; set; }

    public int BirthMonth { get; set; }

    public int BirthYear { get; set; }

    public int ExpireYear;
    public int ExpireMonth;

    public string PackageId { get; set; }

    public int lastSixNumbers { get; set; }

    // Start is called before the first frame update
}
