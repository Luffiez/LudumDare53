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
    [SerializeField] float squishStrength = 1f;

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

    private void Start()
    {
        spriteManager = SpriteManager.instance;
        GenerateRandomNew();

        squishCycle = Random.Range(0, 100);
        squishSpeed *= Random.Range(0.9f, 1.1f);

        wobbleCycle = Random.Range(0, 100);
        wobbleSpeed *= Random.Range(0.9f, 1.1f);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    GenerateRandomNew();

        if (targetPoint != null && Vector2.Distance(transform.position, targetPoint.position) > 0.1f)
        {
            transform.localScale = Vector3.one;
            wobbleCycle += Time.deltaTime * wobbleSpeed;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(wobbleCycle) * wobbleStrength);

            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * moveSpeed);
        }
        else if (destroyOnReachedTarget)
        {
            Destroy(gameObject);
        }
        else
        {
            squishCycle += Time.deltaTime * squishSpeed;
            float sin = Mathf.Sin(squishCycle) * squishStrength;
            transform.localScale = new Vector3(1+ sin, 1- sin, 1); // TODO
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void GenerateRandomNew()
    {
        head.sprite = spriteManager.GetRandomSprite(SpriteParts.head);
        hair.sprite = spriteManager.GetRandomSprite(SpriteParts.hair);
        ears.sprite = spriteManager.GetRandomSprite(SpriteParts.ears);
        eyebrows.sprite = spriteManager.GetRandomSprite(SpriteParts.eyebrows);
        eyes.sprite = spriteManager.GetRandomSprite(SpriteParts.eyes);
        nose.sprite = spriteManager.GetRandomSprite(SpriteParts.nose);
        mouth.sprite = spriteManager.GetRandomSprite(SpriteParts.mouth);
        facialHair.sprite = spriteManager.GetRandomSprite(SpriteParts.facialhair);
        body.sprite = spriteManager.GetRandomSprite(SpriteParts.body);

        if (Random.Range(0, 100) > 80)    // 20% chans att få ärr
        {
            scars.enabled = true;
            scars.sprite = spriteManager.GetRandomSprite(SpriteParts.scars);
        }
        else
            scars.enabled = false;
    }
}
