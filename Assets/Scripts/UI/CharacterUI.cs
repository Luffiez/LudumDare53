using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Sprite Images")]
    [SerializeField] Image head;
    [SerializeField] Image hair;
    [SerializeField] Image ears;
    [SerializeField] Image eyebrows;
    [SerializeField] Image eyes;
    [SerializeField] Image nose;
    [SerializeField] Image mouth;
    [SerializeField] Image scars;
    [SerializeField] Image facialHair;
    [SerializeField] Image body;

    [HideInInspector] public Transform targetPoint;
    [HideInInspector] public bool destroyOnReachedTarget;
    Character character;

    SpriteManager spriteManager;

    float wobbleCycle = 0;
    float squishCycle;

    Vector3 scale;

    public delegate void ReachedTarget();
    public event ReachedTarget OnReachedTarget;

    bool reachedTarget = false;
    private bool IsAtTarget() => targetPoint != null && Vector2.Distance(transform.position, targetPoint.position) > 0.1f;

    private void Start()
    {
        spriteManager = SpriteManager.instance;
        GenerateRandomNew();

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
            OnReachedTarget?.Invoke();
        }

        squishCycle += Time.deltaTime * squishSpeed;
        float sin = Mathf.Sin(squishCycle);
        transform.localScale = new Vector3(scale.x + sin * squishStrengthX, scale.y - sin * squishStrengthY, 1);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void GenerateRandomNew()
    {
        head.sprite = spriteManager.GetRandomSpritePart(SpriteParts.head);
        hair.sprite = spriteManager.GetRandomSpritePart(SpriteParts.hair);
        ears.sprite = spriteManager.GetRandomSpritePart(SpriteParts.ears);
        eyebrows.sprite = spriteManager.GetRandomSpritePart(SpriteParts.eyebrows);
        eyes.sprite = spriteManager.GetRandomSpritePart(SpriteParts.eyes);
        nose.sprite = spriteManager.GetRandomSpritePart(SpriteParts.nose);
        mouth.sprite = spriteManager.GetRandomSpritePart(SpriteParts.mouth);
        facialHair.sprite = spriteManager.GetRandomSpritePart(SpriteParts.facialhair);
        body.sprite = spriteManager.GetRandomSpritePart(SpriteParts.body);

        if (Random.Range(0, 100) > 80)    // 20% chans att få ärr
        {
            scars.enabled = true;
            scars.sprite = spriteManager.GetRandomSpritePart(SpriteParts.scars);
        }
        else
            scars.enabled = false;
    }
}
