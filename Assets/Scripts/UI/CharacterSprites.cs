using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSprites : MonoBehaviour
{
    [Header("Sprite Images")]
    [SerializeField] Image head;
    [SerializeField] Image hairFront;
    [SerializeField] Image hairBack;
    [SerializeField] Image ears;
    [SerializeField] Image eyebrows;
    [SerializeField] Image eyes;
    [SerializeField] Image nose;
    [SerializeField] Image mouth;
    [SerializeField] Image scars;
    [SerializeField] Image facialHair;
    [SerializeField] Image body;
    [SerializeField] Image clothes;

    SpriteManager spriteManager;

    [HideInInspector] public Sprite _head;
    [HideInInspector] public Sprite _hairFront;
    [HideInInspector] public Sprite _hairBack;
    [HideInInspector] public Sprite _ears;
    [HideInInspector] public Sprite _eyebrows;
    [HideInInspector] public Sprite _eyes;
    [HideInInspector] public Sprite _nose;
    [HideInInspector] public Sprite _mouth;
    [HideInInspector] public Sprite _scars;
    [HideInInspector] public Sprite _facialHair;
    [HideInInspector] public Sprite _body;
    [HideInInspector] public Sprite _clothes;
    [HideInInspector] public Color _hairColor;
    [HideInInspector] public Color _bodyColor;

    public void SetSprites()
    {
        head.sprite = _head;
        head.enabled = _head != null;
        head.color = _bodyColor;

        hairFront.sprite = _hairFront;
        hairFront.enabled = _hairFront != null;
        hairFront.color = _hairColor;
        
        hairBack.sprite = _hairBack;
        hairBack.enabled = _hairBack != null;
        hairBack.color = _hairColor;

        ears.sprite = _ears;
        ears.enabled = _ears != null;
        ears.color = _bodyColor;

        eyebrows.sprite = _eyebrows;
        eyebrows.enabled = _eyebrows != null;
        eyebrows.color = _hairColor;

        eyes.sprite = _eyes;
        eyes.enabled = _eyes != null;
        
        nose.sprite = _nose;
        nose.enabled = _nose != null;
        nose.color = _bodyColor;

        mouth.sprite = _mouth;
        mouth.enabled = _mouth != null;
        mouth.color = _bodyColor;

        scars.sprite = _scars;
        scars.enabled = _scars != null;
        scars.color = _bodyColor;

        facialHair.sprite = _facialHair;
        facialHair.enabled = _facialHair != null;
        facialHair.color = _hairColor;

        clothes.sprite = _clothes;
        clothes.enabled = _clothes != null;

        body.sprite = _body;
        body.enabled = _body != null;
        body.color = _bodyColor;
    }

    private void Start()
    {
        if (spriteManager == null)
            spriteManager = SpriteManager.instance;
    }

    public void GenerateSprites(Character character)
    {
        if (spriteManager == null)
            spriteManager = SpriteManager.instance;

        _hairColor = SpriteManager.instance.hairColors[UnityEngine.Random.Range(0, SpriteManager.instance.hairColors.Length)];
        _bodyColor = SpriteManager.instance.bodyColors[UnityEngine.Random.Range(0, SpriteManager.instance.bodyColors.Length)];

        // Genetic
        _head = spriteManager.GetRandomSpritePart(SpriteParts.head, character.Sex);
        _ears = spriteManager.GetRandomSpritePart(SpriteParts.ears, character.Sex);
        _eyes = spriteManager.GetRandomSpritePart(SpriteParts.eyes, character.Sex);
        _nose = spriteManager.GetRandomSpritePart(SpriteParts.nose, character.Sex);
        _mouth = spriteManager.GetRandomSpritePart(SpriteParts.mouth, character.Sex);
        _scars = spriteManager.GetRandomSpritePart(SpriteParts.scars, character.Sex);   
        _body = spriteManager.GetRandomSpritePart(SpriteParts.body, character.Sex);

        // Style
        _clothes = spriteManager.GetRandomSpritePart(SpriteParts.clothes, character.Sex);
        _hairFront = spriteManager.GetRandomSpritePart(SpriteParts.hairFront, character.Sex);
        _hairBack = spriteManager.GetRandomSpritePart(SpriteParts.hairBack, character.Sex);
        _facialHair = spriteManager.GetRandomSpritePart(SpriteParts.facialhair, character.Sex);

        // gray zone
        _eyebrows = spriteManager.GetRandomSpritePart(SpriteParts.eyebrows, character.Sex);
    }

    List<Action> actions = new List<Action>();
    public void SwapVisualStuff(Character character, int swaps)
    {
        if (spriteManager == null)
            spriteManager = SpriteManager.instance;
        actions.Clear();
        actions.Add(() => _hairFront = spriteManager.GetRandomSpritePart(SpriteParts.hairFront, character.Sex));
        actions.Add(() => _hairBack = spriteManager.GetRandomSpritePart(SpriteParts.hairBack, character.Sex));
        actions.Add(() => _clothes = spriteManager.GetRandomSpritePart(SpriteParts.clothes, character.Sex));
        actions.Add(() => _facialHair = spriteManager.GetRandomSpritePart(SpriteParts.facialhair, character.Sex));
        actions.Add(() => _hairColor = SpriteManager.instance.hairColors[UnityEngine.Random.Range(0, SpriteManager.instance.hairColors.Length)]);
        UpdateActions(swaps);
    }

    public void SwapGeneticStuff(Character character, int swaps)
    {
        if (spriteManager == null)
            spriteManager = SpriteManager.instance;
        actions.Clear();
        actions.Add(() => _eyes = spriteManager.GetRandomSpritePart(SpriteParts.eyes, character.Sex));
        actions.Add(() => _ears = spriteManager.GetRandomSpritePart(SpriteParts.ears, character.Sex));
        actions.Add(() => _mouth = spriteManager.GetRandomSpritePart(SpriteParts.mouth, character.Sex));
        actions.Add(() => _nose = spriteManager.GetRandomSpritePart(SpriteParts.nose, character.Sex));
        actions.Add(() => _scars = spriteManager.GetRandomSpritePart(SpriteParts.scars, character.Sex));
        actions.Add(() => _bodyColor = SpriteManager.instance.bodyColors[UnityEngine.Random.Range(0, SpriteManager.instance.bodyColors.Length)]);

        UpdateActions(swaps);
    }

    private void UpdateActions(int swaps)
    {
        if (swaps >= actions.Count)
            swaps = actions.Count;

        for (int i = 0; i < swaps; i++)
        {
            int rand = UnityEngine.Random.Range(0, actions.Count);
            actions[rand]?.Invoke();
            actions.RemoveAt(rand);
        }
    }
}
