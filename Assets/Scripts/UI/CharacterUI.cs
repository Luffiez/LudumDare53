using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterSprites))]
public class CharacterUI : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool debugInfo;

    [Header("Wobble Settings")]
    [SerializeField] float wobbleSpeed = 2f;
    [SerializeField] float wobbleStrength = 1f;

    [Header("Squish Settings")]
    [SerializeField] float squishSpeed = 2f;
    [SerializeField] float squishStrengthX = 1f;
    [SerializeField] float squishStrengthY = 1f;

    [Header("Anger Settings")]
    [SerializeField] float angerSpeed = 2f;
    [SerializeField] float angerStrength = 1f;
    [SerializeField] Image angerImage;



    [HideInInspector] public Transform targetPoint;
    [HideInInspector] public bool destroyOnReachedTarget;
    [HideInInspector] public Character Character;
    
    CharacterSprites sprites;

    float wobbleCycle = 0;
    float squishCycle;
    float angerCycle;

    Vector3 scale;

    bool reachedTarget = false;
    bool leavingQueue = false;
    private bool IsAtTarget() => targetPoint != null && Vector2.Distance(transform.position, targetPoint.position) < 1f;

    private void Start()
    {
        sprites = GetComponent<CharacterSprites>();
        sprites.GenerateSprites(Character);

        squishCycle = Random.Range(0, 100);
        squishSpeed *= Random.Range(0.9f, 1.1f);

        wobbleCycle = Random.Range(0, 100);
        wobbleSpeed *= Random.Range(0.9f, 1.1f);

        scale = transform.localScale;
    }

    private void Update()
    {
        UpdateCharacterPatience();

        if (!IsAtTarget())
            MoveTowardsTarget();
        else
            Idle();
    }

    private void UpdateCharacterPatience()
    {
        Character.Patience -= Time.deltaTime;

        if (Character.PatiencePercentage < 0.2f) //< 20%
        {
            angerImage.enabled = true;
            angerCycle += angerSpeed * Time.deltaTime;
            angerImage.transform.localScale = Vector3.one * Mathf.Sin(angerCycle) * angerStrength;

            if (!leavingQueue && Character.Patience <= 0)
                LeaveQueue();
        }
        else if (angerImage.enabled)
            angerImage.enabled = false;
    }

    private void LeaveQueue()
    {
        leavingQueue = true;
        if (Character == Queue.GetCurrentCharacter())
        {
            Queue.Next(false);
            return;
        }

        var pair = Queue.GetPairFor(Character);
        Queue.RemovePair(pair);
    }

    private void MoveTowardsTarget()
    {
        if (reachedTarget)
            reachedTarget = false;
        transform.localScale = scale;
        wobbleCycle += Time.deltaTime * wobbleSpeed;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(wobbleCycle) * wobbleStrength);
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * moveSpeed);
    }

    private void Idle()
    {
        if (destroyOnReachedTarget)
        {
            Destroy(gameObject);
            return;
        }

        if (!reachedTarget)
        {
            reachedTarget = true;
            Character.ReachedTarget();
        }

        squishCycle += Time.deltaTime * squishSpeed;
        float sin = Mathf.Sin(squishCycle);
        transform.localScale = new Vector3(scale.x + sin * squishStrengthX, scale.y - sin * squishStrengthY, 1);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void DisplayPackageIdAgain()
    {
        if (Queue.GetCurrentCharacter() != Character)
            return;
        if (!IsAtTarget())
            return;

        string[] packageLines = new string[5] {"", "It's ", "My package id is still ", "...", "Do I really need to say it again..? It's " };
        Character.Patience -= 10;
        TextBubble.Instance.Display(packageLines[Random.Range(0, packageLines.Length)] + Character.PackageId);
    }

    Rect rect = new Rect(Vector2.zero, new Vector2(200, 50));

    void OnGUI()
    {
        if (!debugInfo)
            return;

        rect.position = new Vector2(transform.position.x, Screen.height - transform.position.y);
        GUI.Label(rect, $"{Character.PatiencePercentage.ToString("#.##")}%");
    }
}
