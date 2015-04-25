UnityKickstart
==============

This is a collection of simple Unity project templates for cooperative video games.

 * [JollyPlatformer](./jollyplatformer) - A side-scrolling 2D platformer game with 2 characters, like New Super Mario Bros.
 * [JollyTopdown](./jollytopdown) - Top-down game where you roll and bounce around a snowy landscape collecting coins
 * [JollyTouch](./jollytouch) - A physics/puzzle game meant for mobile devices

Each project is functional but not feature-complete. They are meant as starting points for your projects so that you
can create something in Unity without having to start from scratch. There are also general code components that we have found
useful in the [template](./template) directory.

All of the projects source code is written in C#. We will be able to give you the most help if you also use this language.
However, if you are more comfortable with Unityscript, go ahead and use that instead--but we suggest you avoid Boo.

The best way to get started is to open each project directory in the Unity Editor, open the main scene and browse the objects,
paying special attention to the components that make them up. Additionally, inspect the prefab objects and what components
make them up.

# Unity Tips

## If you're brand new to Unity

* Unity is a super powerful way to make games, and is designed to enable you to do a ton in the editor.
* A game object in Unity is an empty container of components that give it behavior. For example, a player object might have
  a component to draw a mesh on screen, a component that allows it to collide with other objects, a component that allows
  it to be controlled by a keyboard, and a component that keeps track of its data (health, etc).
* All logic in your game will exist on a game object of some sort, so you might have an invisible `Arena` object that
  contains information about the state of the game.
* Objects can be easily found in code by their name or the tag associated it.
* Leverage the Unity Asset Store as much as possible. There's tons of other people's work you can use or learn from, lots for free!
* Prefabs are the cornerstone of object creation in Unity. You can think of them as template objects that you create new objects from.
  So when you spawn a player in game, that new player is created by cloning a prefab that represents the player.
* Prefabs tend to be created by creating an object in your scene, then dragging it to the prefabs directory in the bottom pane.
  To update the prefab, you can drag it into your scene, make the necessary changes, and click apply on the object to update the prefab.

## General

* Make sure you [Configure the Unity editor and .gitignore properly](http://stackoverflow.com/questions/18225126/how-to-use-git-for-unity3d-source-control)
* Try to make every scene in your game so the game can be started from that scene. It makes design iteration a lot easier.
  For example, instead of having 20 levels in 1 scene, making 20 scenes each with 1 level makes each one easily editable.
* Push as much design into the Unity editor as possible so that it's easy to edit (and as a bonus, you can tinker with
  it while the game is running!)
* The Input Manager is where you configure controls for your game and map them to values you can grab from script. [Input Manager](http://docs.unity3d.com/Manual/class-InputManager.html)

## Where can I get some art?

* [2D Art List](http://getprismatic.com/story/1414065792475)
* [Open Game Art](http://opengameart.org/)

## Programmers

Here's a quick rundown of things you absolutely need to know when getting started with Unity that you might not find in
usual tutorials:

* `Prefab` is to `GameObject` as class is to instance. Mostly. A `Prefab` actually IS a `GameObject` when
  referenced in code, but `Prefab`s should be used as object templates for spawning new objects. Also, `GameObject`s can
  exist without a prefab if you only ever need one of them and that object should be created when the scene loads.
* Empty `GameObject`s are quite common. Use them to execute code in the scene or create world-location references such
  as spawn points or character attachment points.
* Making a setting configurable in the Unity editor just requires it being a public variable. For the most part, you should
  avoid having constants in code and use this instead.
* Make isolated, reusable components whenever possible. Doing so makes it easy to build new functionality by just
  dragging components on to an object and hooking up references.
* All physics-related code should be in `FixedUpdate`
* Your camera is jittery because it should only be moved in `OnPreCull`
* The built-in Unity GUI is redrawn every frame using code. There is no GUI editor like in Xcode or Visual Studio.
* MonoBehaviour is spelled with a 'u'. If you get errors about `MonoBehavior` not being defined, that's why.
* It's best to spawn objects as children of another game object to keep them organized in the scene hierarchy and make
  debugging using the editor easier. To do this, set the `GameObject.transform.parent` to another `GameObject.transform`. It
  is perfectly fine to use top-level empty game object containers to do this.
* Don't do any creation work (such as playing a sound) in `OnDestroy`
* If you want to load a static text file, don't reference it by name--use `public TextAsset MyTextAsset;`, hook up the
  reference in Unity, and access its contents with `this.MyTextAsset.text`.
* It's very useful to understand the event functions Unity gives you. [Event Functions of a Unity object](http://docs.unity3d.com/Manual/ExecutionOrder.html)
* Coroutines are very useful for creating complex behaviors. It's more advanced, but good to educate yourself on. [Unity Coroutines](http://docs.unity3d.com/Manual/Coroutines.html)

### Style Suggestions

* Use "this.MemberName" to reference any member
* If a static class function references an instance of the class itself, call the variable 'self'
* If a variable is public, use `UpperCamelCase` for the variable name
* If a variable is private or protected, use `lowerCamelCase` for the variable name
* If a variable has accessors and is only a private backing variable, use `_lowerCamelCase` for the variable name. e.g.:
```
private int _someValue;
public SomeValue
{
  get { return this._someValue; }
  set { this._someValue = value; }
}
```

# Additional resources

## Unity

https://unity3d.com/learn/documentation  
http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/  
http://learnunity2d.com/  
http://www.unity3dstudent.com/  

## Free Art

http://letsmakegames.org/resources/art-assets-for-game-developers/  
http://www.pixelprospector.com/the-big-list-of-royalty-free-graphics/  

## Free Sounds

https://www.makegameswith.us/gamernews/281/top-20-best-free-music-and-sound-effect-resources  
