using UnityEngine;

public class Package
{
    public Sprite sprite;
    public bool fake { get; set; }
    public string PersonId { get; set; }
    public string PackageId { get; set; }
    public PackageStatus Status { get; set; }
}

public enum PackageStatus 
{ 
    Delivered, 
    NotRetreived, 
    ReadyToPickUp
}
