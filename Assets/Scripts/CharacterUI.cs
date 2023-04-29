using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
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

    Character character;

    SpriteManager spriteManager;

    private void Start()
    {
        spriteManager = SpriteManager.instance;
        GenerateRandomNew();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateRandomNew();
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
