using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Component class: Add this to your GameObject

public class SpriteAnimator : MonoBehaviour
{
	//
	// Public types
	//

	public enum Clock
	{
		Update,
		FixedUpdate,
		Manual
	}

	//
	// Private types
	//

	private enum Mode
	{
		Sprite,
		Animation
	}

	//
	// Private data
	//
		
	private Dictionary<string, SpriteAnimation> animationMap;
	private Sprite spriteDefault;
	private Mode mode;
	private SpriteAnimation aniCurrent;
	private int aniFrame;
	private float aniFrameElapsed;
	private bool pause;
	private float speed;
	private Clock clock;
		
	//
	// Public Unity events
	//
		
	public void Awake()
	{
		animationMap = new Dictionary<string, SpriteAnimation>();
		spriteDefault = null;
		mode = Mode.Sprite;
		aniCurrent = null;
		aniFrame = 0;
		aniFrameElapsed = 0.0f;
		pause = false;
		speed = 1.0f;
		clock = Clock.FixedUpdate;

		SpriteRenderer sr = this.GetRenderer();
		if (sr != null)
		{
			spriteDefault = sr.sprite;
		}
	}

	public void Update()
	{
		if (clock == Clock.Update)
		{
			NextFrame(Time.deltaTime);
		}
	}

	public void FixedUpdate()
	{
		if (clock == Clock.FixedUpdate)
		{
			NextFrame(Time.deltaTime);
		}
	}

	public void NextFrame(float elapsedSec = 0.02f)
	{
		if (mode == Mode.Animation)
		{
			if (pause)
			{
				return;
			}

			aniFrameElapsed += elapsedSec;
			int elapsedMS = (int)(aniFrameElapsed * 1000.0f);
				
			SpriteAnimationFrame frameCur = aniCurrent.GetFrame(aniFrame);
				
			int frameMS = Mathf.Max(SpriteAnimationFrame.MinTimeMS, frameCur.timeMS);
			if (!Mathf.Approximately(speed, 1.0f))
			{
				if (speed < 1.0f)
				{
					frameMS = Mathf.RoundToInt((float)frameMS * (1.0f / speed));
				}
				else
				{
					frameMS = Mathf.RoundToInt((float)frameMS / speed);
				}
			}

			if (elapsedMS >= frameMS)
			{
				aniFrame++;

				if (aniFrame >= aniCurrent.GetFrameCount())
                {
                    if (aniCurrent.loop)
                    {
						// Loop back to first frame in animation
						aniFrame = 0;
						aniFrameElapsed = 0.0f;
						ShowSpriteOrDefault(aniCurrent.GetFrame(aniFrame).sprite);
                    }
                    else
                    {
						// Stop
						mode = Mode.Sprite;
                    }
				}
                else
				{
					// Next frame in animation
					aniFrameElapsed = 0.0f;
					ShowSpriteOrDefault(aniCurrent.GetFrame(aniFrame).sprite);
				}
			}
		}
	}
		
	//
	// Public methods
	//

	public bool AddAnimation(SpriteAnimation animation)
	{
		if (animation == null)
		{
			return false;
		}

		string name = animation.name;

		if (name.Length == 0)
        {
			return false;
        }

		animationMap[name] = animation;

		if (mode == Mode.Animation && aniCurrent.name == animation.name)
		{
			SetAnimation(animation.name);
		}

		return true;
	}

	public bool UpdateAnimation(SpriteAnimation animation)
	{
		return AddAnimation(animation);
	}

	public void ClearAnimationList()
	{
		animationMap.Clear();

		mode = Mode.Sprite;
		ShowSprite(spriteDefault);
	}
		
	public void SetDefaultSprite(Sprite sprite)
	{
		spriteDefault = sprite;
	}

	public void SetSprite(Sprite sprite)
	{
		mode = Mode.Sprite;

		if (sprite != null)
		{
			ShowSprite(sprite);
		}
		else
		{
			ShowSprite(spriteDefault);
		}
	}

	public void SetAnimation(string name)
	{
		// Check if animation exists
		if (!animationMap.ContainsKey(name))
		{
			mode = Mode.Sprite;
			ShowSprite(spriteDefault);
			return;
		}

		// Check how many animation frames are valid
		List<SpriteAnimationFrame> frameList = animationMap[name].GetFrameList();

		if (frameList.Count == 0)
		{
			mode = Mode.Sprite;
			ShowSprite(spriteDefault);
			return;
		}
		else if (frameList.Count == 1)
		{
			mode = Mode.Sprite;
			ShowSpriteOrDefault(frameList[0].sprite);
			return;
		}

		// Start animation
		mode = Mode.Animation;
		aniCurrent = animationMap[name];
		aniFrame = 0;
		aniFrameElapsed = 0.0f;
		ShowSpriteOrDefault(aniCurrent.GetFrame(aniFrame).sprite);
	}

	SpriteAnimation GetAnimation()
    {
		return aniCurrent;
    }

	public void SetPause(bool val)
	{
		pause = val;
	}

	public void SetSpeed(float val)
	{
		speed = val;
	}

	public void SetClock(Clock val)
	{
		clock = val;
	}

	//
	// Private methods
	//

	private SpriteRenderer GetRenderer()
	{
		return this.gameObject.GetComponent<SpriteRenderer>();
	}

	private void ShowSprite(Sprite sprite)
	{
		if (sprite != null)
		{
			SpriteRenderer sr = this.GetRenderer();
			if (sr != null)
			{
				sr.sprite = sprite;
			}
		}
	}

	private void ShowSpriteOrDefault(Sprite sprite)
	{
		if (sprite != null)
		{
			ShowSprite(sprite);
		}
		else
		{
			ShowSprite(spriteDefault);
		}
	}

    // ...
}
