template
========

This directory contains helpful code for any Unity3d project.

# Getting Started

Copy the Assets directory from this template into your new project and merge the directories
together. In C# source files where you want to use these extensions, add `using Jolly;` to the
top of the file.

# JollyDebug

The JollyDebug class makes debugging Unity projects much less painful. Unless explicitly
noted otherwise, methods are only compiled in debug and editor releases.

## JollyDebug.Watch

The Watch method allows you to view the value of expressions in real-time by examining the
JollyDebug object that gets created in the root of your scene. Values can update either by
push or pull--you can assign into them directly, or pass a delegate function that gets
invoked each frame to update the value. Watched values are grouped by owner object and
automatically get removed if that object is deleted from the scene.

Example of a 'pull'-type watch:

```
void Start ()
{
	JollyDebug.Watch (this, "Location", delegate ()
	{
		return this.gameObject.transform.position.ToString();
	});
}
```

Example of a 'push'-type watch:

```
void Update ()
{
	JollyDebug.Watch (this, "DeltaTime", Time.deltaTime);
}
```

Expression types can be `bool`, `float`, or `string`. Other types can easily be added by editing
the source code, but for most purposes it is sufficient to simply use ToString on the input.

## JollyDebug.Assert

This method raises an exception if the condition input is not true.

```
void Start ()
{
	JollyDebug.Assert(null != this.ReferencedGameObject);
}
```

### JollyDebugInspector

The inspector is an extension that customizes the rendering of the JollyDebug
class in the Unity editor.

# Vector3Ext

This is a static container class for adding extension methods to the `Vector3` class. Methods include:
 * `SetX`/`SetY`/`SetZ` - Return a new `Vector3` with the given member set to a new value
 * `SetXY` - Return a new `Vector3` with both X and Y set to new values
 * `xy` - Return a new `Vector2` from just the X and Y components

# Vector2Ext

Similarly to `Vector3Ext`, this is a container class for extension methods of `Vector2`. It adds just
one method at the moment: `xyz`, which returns a new `Vector3` with a given Z component value.


