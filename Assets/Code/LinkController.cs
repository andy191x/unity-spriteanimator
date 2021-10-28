using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkController : MonoBehaviour
{
	private SpriteAnimator animator = null;

    // Start is called before the first frame update
    void Start()
    {
		// Add animator
        animator = this.gameObject.AddComponent<SpriteAnimator>();

		// Create animations
		Sprite[] spriteArray = Resources.LoadAll<Sprite>("Link");
		SpriteAnimation next = null;

		next = new SpriteAnimation();
		next.name = "walkLeft";
		next.AddFrame(spriteArray[30], 150);
		next.AddFrame(spriteArray[31], 150);
		next.loop = true;
		animator.AddAnimation(next);

		next = new SpriteAnimation();
		next.name = "walkUp";
		next.AddFrame(spriteArray[0], 150);
		next.AddFrame(spriteArray[1], 150);
		next.loop = true;
		animator.AddAnimation(next);

		next = new SpriteAnimation();
		next.name = "walkRight";
		next.AddFrame(spriteArray[10], 150);
		next.AddFrame(spriteArray[11], 150);
		next.loop = true;
		animator.AddAnimation(next);

		next = new SpriteAnimation();
		next.name = "walkDown";
		next.AddFrame(spriteArray[20], 150);
		next.AddFrame(spriteArray[21], 150);
		next.loop = true;
		animator.AddAnimation(next);

		// Start animation
		animator.SetAnimation("walkDown");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			animator.SetAnimation("walkLeft");
		}
        else if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			animator.SetAnimation("walkUp");
		}
        else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			animator.SetAnimation("walkRight");
		}
        else if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			animator.SetAnimation("walkDown");
		}
    }
}
