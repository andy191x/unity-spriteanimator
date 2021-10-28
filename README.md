
# unity-spriteanimator

Simple drop-in sprite animation classes for Unity 2D. This is meant to be adapted to any project that needs quick 2D sprite animations without Mechanim.

Example scene in `Assets/Scenes`. Built using Unity 2020.3.

## Setup animation

	// Add component to sprite game object (or poll from existing object with "GetComponent")
	SpriteAnimator animator = someGameObject.AddComponent<SpriteAnimator>();

	// Create animations
	Sprite[] spriteArray = Resources.LoadAll<Sprite>("Path/To/Your/SpriteSheet");
	SpriteAnimation next = null;

	next = new SpriteAnimation();
	next.name = "walkLeft";
	next.AddFrame(spriteArray[0], 250);
	next.AddFrame(spriteArray[1], 250);
	next.AddFrame(spriteArray[2], 250);
	next.loop = true;
	animator.AddAnimation(next);

	next = new SpriteAnimation();
	next.name = "walkRight";
	next.AddFrame(spriteArray[3], 250);
	next.AddFrame(spriteArray[4], 250);
	next.AddFrame(spriteArray[5], 250);
	next.loop = true;
	animator.AddAnimation(next);

## Change animation

	animator.SetAnimation("walkLeft");

## Pause/unpause animation

	animator.SetPause(true);
