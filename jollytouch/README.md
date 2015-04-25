JollyTouch
==========

![JollyTouch](./jollytouchpreview.png?raw=true)

This project demonstrates a simple touch-based 2d game. It includes a main menu and
a progression of several levels. The goal of each level is to move the large circles
so that they bounce streams of small white balls into the sun to power it up.

Demonstrated Features:

 * Reading touch input and dispatching to objects in 2D scene
 * Faking touch input with mouse for testing
 * Progressively revealing sprite using clipping area (progress bar)
 * Basic 2D physics
 * Creating and destroying game objects

# Where to start

Load up the project in the Unity editor and open the Assets/Library/mainmenu.scene and hit play
to test the game.
Load up the Assets/Library/level1.scene and browse the objects in the scene, specifically paying attention
to draggable player object, the sun, spawn point and level manager and their respective components.
Look at the Bouncer and Player prefabs, which act as the template for those objects when they need to
be created.


# Architecture

## Scenes

```
mainmenu -> level1
         -> level2
         -> level3
         -> level4
         -> mainmenu
```

The main menu scene starts the game when the user taps the screen. Each level plays out
sequentially, ending when the sun or suns have been fully powered.

In each level, the game only has two states: "running" and "complete". As long as the
level is not complete, the stream emitters run and the game is interactible. Once all
the suns in a level have been powered, the level is complete and the emitters stop.
After a delay in the Complete state, the game progresses to the next level.

## Major Components

### TouchInputDispatcher

This component is attached to a camera to allow it to project touch input (or fake touch
input from the mouse) into 2D space. Each frame, it runs through each touch input and
uses Unity's physics engine to pick a 2D collider. A function unique to the current
touch phase is called on the collider's GameObject using
`GameObject::SendMessage`:

* `void OnTouchBegin(Vector2 point)`
* `void OnTouchEnd(Vector2 point)`
* `void OnTouch(Vector2 point)` - The touch is down and either moved or stayed stationary
* `void OnTouchExit(Vector2 point)` - The touch left the screen

Remember that only objects with a Collider component will receive these messages. For
example, to receive touches for general actions, the main menu uses the TouchInputDispatcher
and an empty game object with a full-screen BoxCollider2D to receive the touch message that
begins the game.

*Notes*

One problem you will run into with this demo and the TouchInputDispatcher is that a quick
movement with your finger will cause you to lose contact with the object you were dragging.
Once the finger has left the collider area it no longer sends messages to the game object,
so it doesn't update its position. I'll leave this up to you to solve. :)

### LevelManager

The `LevelManager` coordinates progression through each level in the game. It is instantiated
once on its own empty game object in each scene. By querying `LevelManager.Instance.LevelIsComplete`,
other components can understand the state of the current level. For example, the
`SpawnsObjectStream` component will create a stream of objects until this flag is `false`.

The `LevelManager` determines whether the level is complete by quering `IsFullyPowered` on
all of the `Sun` components on objects with the "Sun" tag. If all flags are true, the level
is complete.

Finally, the next level to load is entered as a string into this class.

### Sun

Each sun game object is managed by a `Sun` component that has a number of responsibilities:

* Track the current power level of the sun based on incoming streams
* Fill the sun's disc from bottom to top and change its color depending on power level
* Pulse the size of the sun if it is currently receiving power

The easiest way to understand how it performs each of these is to read the code! Briefly,
the sun has a circle collider that acts as a trigger, which invokes `OnTriggerEnter2D`
whenever a ball overlaps the sun's area. This destroys the ball and adds a record to
an internal array of recent inputs. This array is read each frame to determine the
rate of input over the last second and compared to the expected input rate to determine
the absolute power level. The displayed power level is interpolated toward the absolute
power level smoothly to make the effect nice. This displayed power level is then used
to create a new sprite from the full-disc source image with only some portion revealed.
This new sprite is assigned to the DiscToFill game object so that it is rendered.

*Notes*

`FillDisc` is more complicated than it needs to be because I chose to make all the sprites
center-pivoted instead of bottom-pivoted. This is why there is additional computation to
set the y position of `DiscToFill`. A cleaner version of this code would use bottom-pivoted
sprites instead.


## Other Components

The rest of the components should be fairly self-explanatory. For documentation on the
common components (`JollyDebug`, `Vector3`, `Vector2`) please see the `../template/`
directory.





