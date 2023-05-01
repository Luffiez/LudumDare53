using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;
    public Texture2D[] characterSpriteSheets;
    public Sprite[] packageSprites;

    public Color[] hairColors;
    public Color[] bodyColors;

    List<Sprite> hairFront = new List<Sprite>();
    List<Sprite> hairShortFront = new List<Sprite>();
    List<Sprite> hairLongFront = new List<Sprite>();
    List<Sprite> hairBack = new List<Sprite>();
    List<Sprite> eyebrows = new List<Sprite>();
    List<Sprite> eyes = new List<Sprite>();
    List<Sprite> ears = new List<Sprite>();
    List<Sprite> nose = new List<Sprite>();
    List<Sprite> mouth = new List<Sprite>();
    List<Sprite> scars = new List<Sprite>();
    List<Sprite> facialhair = new List<Sprite>();
    List<Sprite> head = new List<Sprite>();
    List<Sprite> bodyMale = new List<Sprite>();
    List<Sprite> bodyFemale = new List<Sprite>();
    List<Sprite> clothes = new List<Sprite>();
    List<Sprite> clothesMale = new List<Sprite>();
    List<Sprite> clothesFemale = new List<Sprite>();


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
                case "bodyMale": bodyMale.Add(sprite); break;
                case "bodyFemale": bodyFemale.Add(sprite); break;
                case "clothes": clothes.Add(sprite); break;
                case "clothesMale": clothesMale.Add(sprite); break;
                case "clothesFemale": clothesFemale.Add(sprite); break;
                case "head": head.Add(sprite); break;
                case "facialhair": facialhair.Add(sprite); break;
                case "scar": scars.Add(sprite); break;
                case "mouth": mouth.Add(sprite); break;
                case "nose": nose.Add(sprite); break;
                case "ears": ears.Add(sprite); break;
                case "eyes": eyes.Add(sprite); break;
                case "eyebrows": eyebrows.Add(sprite); break;
                case "hairFront": hairFront.Add(sprite); break;
                case "hairBack": hairBack.Add(sprite); break;
                case "hairShortFront": hairShortFront.Add(sprite); break;
                case "hairLongFront": hairLongFront.Add(sprite); break;
                default: break;
            }
        }
    }

    public Sprite GetRandomSpritePart(SpriteParts part, Sex sex)
    {
        List<Sprite> sprites = null;
        int rand;
        switch (part)
        {
            case SpriteParts.body:
                sprites = (sex == Sex.Male) ? bodyMale : bodyFemale;
                break;
            case SpriteParts.head: sprites = head; break;
            case SpriteParts.facialhair:
                if (sex == Sex.Male && Random.Range(1, 101) > 40)
                    sprites = facialhair;
                else
                    return null;
                break; 

            case SpriteParts.hairFront: 
                if (sex == Sex.Male)
                {
                    rand = Random.Range(0, 10);
                    if (rand < 2) // 20% long
                        sprites = hairLongFront;
                    else
                        sprites = hairShortFront;
                }
                else
                {
                    rand = Random.Range(0, 10);
                    if (rand < 2) // 20% short
                        sprites = hairShortFront;
                    else
                        sprites = hairLongFront;
                }
                break;
            case SpriteParts.hairBack:

                if (sex == Sex.Male)
                {
                    rand = Random.Range(0, 10);
                    if (rand < 1) // 20% back
                        return null;
                    sprites = hairBack;
                }
                else
                {
                    rand = Random.Range(0, 10);
                    if (rand < 2) // 80% back
                        return null;
                    sprites = hairBack;
                }
                break;
            case SpriteParts.scars:
                if (Random.Range(1, 101) > 80)
                    sprites = scars;
                else
                    return null;
               break;

            case SpriteParts.clothes:
                rand = Random.Range(0, 10);
                if (rand < 5) // 50% chance for gender specific clothes
                {
                    if (sex == Sex.Male)
                        sprites = clothesMale;
                    else
                        sprites = clothesFemale;
                    break;
                }
                sprites = clothes;
                break;

            case SpriteParts.mouth: sprites = mouth; break;
            case SpriteParts.nose: sprites = nose; break;
            case SpriteParts.ears: sprites = ears;break;
            case SpriteParts.eyes: sprites = eyes; break;
            case SpriteParts.eyebrows: sprites = eyebrows; break;
            default: throw new System.NotImplementedException();
        }
     
        rand = Random.Range(0, sprites.Count);
        Sprite sprite;
        try
        {
            sprite = sprites[rand];
            return sprite;

        }
        catch (System.Exception e)
        {
            throw new System.IndexOutOfRangeException(part + " : " + sex);
        }
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
    hairFront,
    hairBack,
    clothes
}
