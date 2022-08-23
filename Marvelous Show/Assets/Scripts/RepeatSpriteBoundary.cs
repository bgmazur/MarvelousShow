 using UnityEngine;
 using System.Collections;
 
 // @NOTE the attached sprite's position should be "top left" or the children will not align properly
 // Strech out the image as you need in the sprite render, the following script will auto-correct it when rendered in the game
 [RequireComponent (typeof (SpriteRenderer))]
 
 // Generates a nice set of repeated sprites inside a streched sprite renderer
 // @NOTE Vertical only, you can easily expand this to horizontal with a little tweaking
 public class RepeatSpriteBoundary : MonoBehaviour {
     SpriteRenderer sprite;
 
     void Awake () {
         // Get the current sprite with an unscaled size
         sprite = GetComponent<SpriteRenderer>();
         Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);
 
         // Generate a child prefab of the sprite renderer
         GameObject childPrefab = new GameObject();
         SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
         childPrefab.transform.position = transform.position;
         childSprite.sprite = sprite.sprite;
 
         // Loop through and spit out repeated tiles
         GameObject child;
         for (int i = 1, l = (int)Mathf.Round(sprite.bounds.size.y); i < l; i++) {
             child = Instantiate(childPrefab) as GameObject;
             child.transform.position = transform.position - (new Vector3(0, spriteSize.y, 0) * i);
             child.transform.parent = transform;
         }
 
         // Set the parent last on the prefab to prevent transform displacement
         childPrefab.transform.parent = transform;
 
         // Disable the currently existing sprite component since its now a repeated image
         sprite.enabled = false;
     }
 }