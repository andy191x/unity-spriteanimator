using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animation definition: Instantiate these directly and add them to your SpriteAnimator component

public class SpriteAnimation
{
	//
	// Private data
	//

	private List<SpriteAnimationFrame> frameList;

	//
	// Public properties
	//

	public string name { get; set; }
	public bool loop { get; set; }
		
	//
	// Public methods
	//
		
	public SpriteAnimation()
	{
		frameList = new List<SpriteAnimationFrame>();

		name = "";
		loop = true;
	}

	public SpriteAnimation(SpriteAnimation copy)
	{			
		frameList = new List<SpriteAnimationFrame>();
		foreach (SpriteAnimationFrame copyframe in copy.frameList)
		{
			SpriteAnimationFrame frame = new SpriteAnimationFrame(copyframe);
			frameList.Add(frame);
		}

		name = copy.name;
		loop = copy.loop;
	}
		
	public void Clear()
	{
		frameList.Clear();
	}

	public void AddFrame(Sprite sprite, int timeMS)
	{
		SpriteAnimationFrame frame = new SpriteAnimationFrame();
		frame.sprite = sprite;
		frame.timeMS = timeMS;
		frameList.Add(frame);
	}

	public void AddFrame(SpriteAnimationFrame frame)
	{
		frameList.Add(frame);
	}

	public List<SpriteAnimationFrame> GetFrameList()
	{
		return frameList;
	}

	public SpriteAnimationFrame GetFrame(int index)
    {
		if (index < frameList.Count)
		{
			return frameList[index];
		}

		return null;
    }

	public int GetFrameCount()
	{
		return frameList.Count;
	}
		
    // ...
}
