using UnityEngine;
using UnityEngine.UI;

public class CharacterSprites : MonoBehaviour
{
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

    SpriteManager spriteManager;
    
    public void GenerateSprites(Character character)
    {
        if(spriteManager == null)
            spriteManager = SpriteManager.instance;


        head.sprite = spriteManager.GetRandomSpritePart(SpriteParts.head);

        // if male 20% long hair, if female 20% short hair
        hair.sprite = spriteManager.GetRandomSpritePart(SpriteParts.hair);
        ears.sprite = spriteManager.GetRandomSpritePart(SpriteParts.ears);
        eyebrows.sprite = spriteManager.GetRandomSpritePart(SpriteParts.eyebrows);
        eyes.sprite = spriteManager.GetRandomSpritePart(SpriteParts.eyes);
        nose.sprite = spriteManager.GetRandomSpritePart(SpriteParts.nose);
        mouth.sprite = spriteManager.GetRandomSpritePart(SpriteParts.mouth);
        body.sprite = spriteManager.GetRandomSpritePart(SpriteParts.body);

        if (character.Sex == Sex.Male && Random.Range(0, 100) > 60) // 60% chance for males to have facial hair
        {
            facialHair.enabled = true;
            facialHair.sprite = spriteManager.GetRandomSpritePart(SpriteParts.facialhair);
        }
        else
            facialHair.enabled = false;

        if (Random.Range(0, 100) > 80)    // 20% chance for scars
        {
            scars.enabled = true;
            scars.sprite = spriteManager.GetRandomSpritePart(SpriteParts.scars);
        }
        else
            scars.enabled = false;
    }
}
