using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class LoopThroughSprites : MonoBehaviour
{
    private Image image;
    public List<Sprite> sprites;
    public float animSpeed = 1;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(StartAnim());
    }

    
    public IEnumerator StartAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(animSpeed); // Wait for the specified time
            index++;

            // Reset the index if it goes beyond the last sprite
            if (index >= sprites.Count)
                index = 0;

            // Always update the sprite
            image.sprite = sprites[index];
        }
    }
}
