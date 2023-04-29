using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;
    public Texture2D[] characterSpriteSheets;
    public Sprite[] packageSprites;

    List<Sprite> hair = new List<Sprite>();
    List<Sprite> eyebrows = new List<Sprite>();
    List<Sprite> eyes = new List<Sprite>();
    List<Sprite> ears = new List<Sprite>();
    List<Sprite> nose = new List<Sprite>();
    List<Sprite> mouth = new List<Sprite>();
    List<Sprite> scars = new List<Sprite>();
    List<Sprite> facialhair = new List<Sprite>();
    List<Sprite> head = new List<Sprite>();
    List<Sprite> body = new List<Sprite>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        for (int i = 0; i < characterSpriteSheets.Length; i++)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>(Path.Combine("SpriteSheets", characterSpriteSheets[i].name));
            AddSprites(sprites);
        }
    }

    void AddSprites(Sprite[] sprites)
    {
        foreach (var sprite in sprites)
        {
            switch (sprite.name)
            {
                case "body": body.Add(sprite); break;
                case "head": head.Add(sprite); break;
                case "facialhair": facialhair.Add(sprite); break;
                case "scar": scars.Add(sprite); break;
                case "mouth": mouth.Add(sprite); break;
                case "nose": nose.Add(sprite); break;
                case "ears": ears.Add(sprite); break;
                case "eyes": eyes.Add(sprite); break;
                case "eyebrows": eyebrows.Add(sprite); break;
                case "hair": hair.Add(sprite); break;
                default: break;
            }
        }
    }

    public Sprite GetRandomSpritePart(SpriteParts part)
    {
        List<Sprite> sprites;

        switch (part)
        {
            case SpriteParts.body: sprites = body; break;
            case SpriteParts.head: sprites = head; break;
            case SpriteParts.facialhair: sprites = facialhair; break; 
            case SpriteParts.scars: sprites = scars; break;
            case SpriteParts.mouth: sprites = mouth; break;
            case SpriteParts.nose: sprites = nose; break;
            case SpriteParts.ears: sprites = ears;break;
            case SpriteParts.eyes: sprites = eyes; break;
            case SpriteParts.eyebrows: sprites = eyebrows; break;
            case SpriteParts.hair: sprites = hair; break;
            default: throw new System.NotImplementedException();
        }

        int rand = Random.Range(0, sprites.Count);
        return sprites[rand];
    }

    public Sprite GetRandomPackageSprite()
    {
        int rand = Random.Range(0, packageSprites.Length);
        return packageSprites[rand];
    }
}

public enum SpriteParts
{
    body,
    head,
    facialhair,
    scars,
    mouth,
    nose,
    ears,
    eyes,
    eyebrows,
    hair
}
