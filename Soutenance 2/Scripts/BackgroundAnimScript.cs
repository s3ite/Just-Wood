using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundAnimScript : MonoBehaviour
{
    [SerializeField] private Texture[] textures;
    private Sprite[] sprites;
    
    public int framesPerSecond = 10;
    private int max;
    private Image image;
    
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        max = textures.Length;

        sprites = new Sprite[max];

        for (int j = 0; j < max; j++)
            sprites[j] = toSprite(textures[j]);
    }
    
    void Update()
    {
        int index = (int) (Time.time * framesPerSecond) % max;
        image.sprite = sprites[index];
    }

    private Sprite toSprite(Texture tex)
    {
        Texture2D tex2d = (Texture2D) tex;
        Sprite sprite = Sprite.Create(tex2d, new Rect(0, 0, tex.width, tex.height), new Vector2(tex2d.width / 2, tex2d.height / 2));
        return sprite;
    }
}
