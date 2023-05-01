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

    bool initialized = false;

    private void Start()
    {
        Queue.OnNext += Queue_OnNext;
        Queue.OnReachedTarget += Queue_OnReachedTarget;
        Queue.OnPairAdded += Queue_OnPairAdded;
    }

    private void Queue_OnPairAdded(QueuePair newPair)
    {
        if (initialized)
            return;

        initialized = true;
        GenerateCard(Queue.GetCurrentCharacter());
    }

    private void OnDestroy()
    {
        Queue.OnNext -= Queue_OnNext;
        Queue.OnReachedTarget -= Queue_OnReachedTarget;
    }

    private void GenerateCard(Character character)
    {
        // Copy character sprites to ID
        IdPhoto._head = character.UI.sprites._head;
        IdPhoto._hairFront = character.UI.sprites._hairFront;
        IdPhoto._hairBack = character.UI.sprites._hairBack;
        IdPhoto._ears = character.UI.sprites._ears;
        IdPhoto._eyebrows = character.UI.sprites._eyebrows;
        IdPhoto._eyes = character.UI.sprites._eyes;
        IdPhoto._nose = character.UI.sprites._nose;
        IdPhoto._mouth = character.UI.sprites._mouth;
        IdPhoto._scars = character.UI.sprites._scars;
        IdPhoto._facialHair = character.UI.sprites._facialHair;
        IdPhoto._body = character.UI.sprites._body;
        IdPhoto._clothes = character.UI.sprites._clothes;
        IdPhoto._hairColor = character.UI.sprites._hairColor;
        IdPhoto._bodyColor = character.UI.sprites._bodyColor;


        // Change some hair or clothes randomly but leave important genetical features
        int randSwaps = Random.Range(0, 3);
        IdPhoto.SwapVisualStuff(character, randSwaps);

        if (character.FakeId)
        {
            // change some genetical features
            randSwaps = Random.Range(0, 2);
            IdPhoto.SwapGeneticStuff(character, randSwaps);
        }
        else

        IdPhoto.SetSprites();

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
    }

    public void PreviewCard()
    {
        IdPhoto.SetSprites();
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
            ResetCard();
    }

    void ResetCard()
    {
        target = null;
        idImage.transform.position = startPos.position;
        idImage.gameObject.SetActive(false);
    }

    private void Queue_OnNext(QueuePair oldPair, QueuePair newPair, bool approved)
    {
        StopPreviewingCard();
        GenerateCard(newPair.character);
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
