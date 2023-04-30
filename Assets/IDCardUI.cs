using TMPro;
using UnityEngine;

public class IDCardUI : MonoBehaviour
{
    [SerializeField] float showSpeed = 400f;
    [SerializeField] float returnSpeed = 800f;
    [SerializeField] Transform idImage;
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] TMP_Text cardPreviewText;
    [SerializeField] CharacterSprites IdPhoto;

    Transform target;
    bool isReturn;
    float speed;

    private void Start()
    {
        Queue.OnNext += Queue_OnNext;
        Queue.OnReachedTarget += Queue_OnReachedTarget;
    }

    public void PreviewCard()
    {
        var character = Queue.GetCurrentCharacter();

        // TODO: Generate false sprites if fakeId? Generate difference in hair/facial, clothes sprites?
        IdPhoto.GenerateSprites(character);

        cardPreviewText.text = "<b>Name</b>\n";
        cardPreviewText.text += character.GivenName + " " + character.SurName;

        cardPreviewText.text += "\n\n<b>Date of Birth</b>\n";
        cardPreviewText.text += $"{character.BirthDay} / {GetMonthAbbr(character.BirthMonth)} / {character.BirthYear}";

        cardPreviewText.text += "\n\n<b>Personal ID</b>\n";
        cardPreviewText.text += character.PersonIdString;

        cardPreviewText.text += "\n\n<b>Sex</b>\n";
        cardPreviewText.text += character.Sex.ToString();

        cardPreviewText.text += "\n\n<b>Expiration Date</b>\n";
        cardPreviewText.text += character.ExpireMonth + " / " + character.ExpireYear;

        cardPreviewText.transform.parent.gameObject.SetActive(true);
    }

    public void StopPreviewingCard()
    {
        cardPreviewText.transform.parent.gameObject.SetActive(false);
    }

    string GetMonthAbbr(int monthNumber)
    {
        switch (monthNumber)
        {
            case 1: return "JAN";
            case 2: return "FEB";
            case 3: return "MAR";
            case 4: return "APR";
            case 5: return "MAY";
            case 6: return "JUN";
            case 7: return "JUL";
            case 8: return "AUG";
            case 9: return "SEP";
            case 10: return "OKT";
            case 11: return "NOV";
            case 12: return "DEC";

            default: return "NULL";
        }
    }

    private void Update()
    {
        if(target == null)
        {
            idImage.gameObject.SetActive(false);
            return;
        }

        if (Vector2.Distance(idImage.transform.position, target.position) > 5f)
        {
            idImage.transform.position = Vector2.MoveTowards(idImage.transform.position, target.position, Time.deltaTime * speed);
            return;
        }

        if (isReturn)
        {
            target = null;
            idImage.transform.position = startPos.position;
            idImage.gameObject.SetActive(false);
        }
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        speed = returnSpeed;
        isReturn = true;
        target = oldPair.character.UI.transform;
    }

    private void Queue_OnReachedTarget(Character character)
    {
        if (character != Queue.GetCurrentCharacter())
            return;

        speed = showSpeed;
        idImage.gameObject.SetActive(true);
        isReturn = false;
        target = endPos;
    }
}
