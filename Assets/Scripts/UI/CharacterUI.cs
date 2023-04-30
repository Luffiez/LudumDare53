using UnityEngine;

[RequireComponent(typeof(CharacterSprites))]
public class CharacterUI : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    [Header("Wobble Settings")]
    [SerializeField] float wobbleSpeed = 2f;
    [SerializeField] float wobbleStrength = 1f;

    [Header("Squish Settings")]
    [SerializeField] float squishSpeed = 2f;
    [SerializeField] float squishStrengthX = 1f;
    [SerializeField] float squishStrengthY = 1f;

    [HideInInspector] public Transform targetPoint;
    [HideInInspector] public bool destroyOnReachedTarget;
    [HideInInspector] public Character Character;
    
    CharacterSprites sprites;

    float wobbleCycle = 0;
    float squishCycle;

    Vector3 scale;

    bool reachedTarget = false;
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
        if (!IsAtTarget())
            MoveTowardsTarget();
        else
            Idle();
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
            Destroy(gameObject);

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
}
