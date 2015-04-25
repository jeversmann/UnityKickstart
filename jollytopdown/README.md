JollyTopdown
==========

![JollyTouch](./jollytopdownpreview.png?raw=true)

JollyTopdown is a 3D, top-down direct control open-world game. Players play as
either a red or blue sphere that rolls, bounces and jumps across a snowy
landscape to collect hovering coins.

Demonstrated features include:

 * Terrain world mesh
 * Tree instancing using an imported .3DS scenery mesh
 * Illumination and bump-mapping using normal maps
 * Enabling shadows
 * Automatic camera control
 * Single-shot sounds (picking up coins)
 * Continuous, adjusted sounds (crunching of snow under spheres as they roll)

Controls
--------

Player 1:

 * w/a/s/d for movement
 * 'c' or 'e' to jump

Player 2:
 * arrow keys for movement
 * spacebar or '/' to jump

# Where to start

Load up the project in the Unity editor and open the Assets/Library/ingame.scene and hit play
to test the game.
Browse the objects in the scene, specifically paying attention to the player objects, their components
and their child objects.
Look at the Coin and Hero prefabs, which act as the template for those objects when they need to
be created.
Look at the materials, including a bouncy material, textures and sounds to learn about how a 3D game
with sound is built.


# Architecture

## Scenes

There is only one scene in this project: `ingame`. To see ideas for changing scenes and menus,
check out the other projects.

## Major Components

### Player

A `Player` is responsible for navigating a sphere around the terrain. It reads input from a
`PlayerController` and applys forces to the sphere using Unity's physics engine. Control and
effect are split out this way so that one could easily swap out the PlayerController for another
input type such as an AI player. Note that forces are only applied in the `FixedUpdate` function
rather than `Update`. This is a requirement of Unity's physics simulation.

Each player has a `ContactGroundDelta` referenced object. This is a child object of the player
that determines how close the ground needs to be in order for us to consider the sphere to be
in contact with it. During `Update`, the vertical axis (Y) offset of this component is used
in the terrain raycast. Unlike a similar construction in the `jollyplatformer` project, we
don't use the actual position of the `ContactGroundDelta` game object because the sphere
rotates and this object isn't always below the delta.

** Note **

A `Player` has an `OnCollected(Collectable collectable)` function that is intentionally
stubbed out (empty). Feel free to use this for whatever you like!

### PlayerController

The controller is actually the only place where controls are explicitly differentiated between
one player and another. The attribute `PlayerNumber` is used to determine which set of controls
to use to control the attached `Player`.

### HeadsUpDisplay

This component renders the GUI with player scores and the game reset button. `HeadsUpDisplay`
resides on an empty GameObject specifically for this purpose.

### FollowPlayers

This behavior is attached to the main camera and does very simple camera-following. It smoothly
interpolates the camera to the average planar location of the players. The offset from the
average location is defined by the position of the camera relative to the origin when the
scene starts.

### PlaceCollectablesOnTerrain

When the game starts, this behavior instantiates game objects randomly across the terrain.

### PlayLoopedMovementSound

This behavior is attached to a player and requires that the player has an `AudioSource` attached
to it. It will pitch-modulate and volume fade the looping sound from that `AudioSource` based
on the object's current speed.

### Collectable

A `Collectable` behavior is attached to each of the coin objects in the scene. The coin has
a physics collider with the `Trigger` flag enabled on it, causing any object passing through
the coin to invoke `OnTriggerEnter` on the `Collectable` class. This is where the coin
plays its pickup sound and invokes `OnCollected` for the `Player` that obtained the coin.


## Other Components

For documentation on the common components (`JollyDebug`, `Vector3`, `Vector2`) please see
the `../template/` directory.


