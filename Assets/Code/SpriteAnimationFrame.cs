using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Internal data structure, instantiated by SpriteAnimation

public class SpriteAnimationFrame
{
	//
	// Public constants
	//

	public const int MinTimeMS = (1000 / 30);

	//
	// Public properties
	//
		
	public Sprite sprite { get; set; }
	public int timeMS { get; set; }
		
	//
	// Public methods
	//
		
	public SpriteAnimationFrame()
	{
		sprite = null;
		timeMS = 0;
	}

	public SpriteAnimationFrame(SpriteAnimationFrame copy)
	{
		sprite = copy.sprite;
		timeMS = copy.timeMS;
	}

	// ...
}
