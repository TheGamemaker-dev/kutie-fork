# Kutie

Kutie is a drop-in utilities library for Unity whose purpose is to provide small features that I think
should be in Unity or C#, but aren't.

## Highlights

- Adds "Open in Terminal" to the project pane (this was the reason I started this library lol)
- Adds `LayerMask.Contains` extension to detect if a layermask contains a layer
- Tons of vector extensions
  - Adds pure functions to modify Vector components (eg. `WithZ`) (useful for modifying components of properties like `transform.position` without a temporary varibale)
  - Component-wise operations (`Hammard`, `Div`, `Rem`, `Abs`)
  - Projections (`Vector3.ProjectXZ()`) and swizzling (`Vector3.XZ()`)
- `SingletonMonobehaviour` and `SingletonScriptableObject`
- Spring values for animation/more customizable `SmoothDamp`/`Lerp`
- [PID controller](https://en.wikipedia.org/wiki/Proportional%E2%80%93integral%E2%80%93derivative_controller) for AI
- Sorted `Physics` queries

## Editor

### CommandPromptEditor

(Windows and Linux only right now...)

This will create a Assets context menu for the project pane where you can open the folder in a terminal. On Windows, it checks the built in command prompts in this order:

- Windows Terminal
- Pwsh (PowerShell 7)
- Powershell
- Cmd

On Linux, it will check the `TERM` and `TERMINAL` environment variables, and then check through a list of known terminals in the path using
the `Kutie.OS.Executable.Which` command.

## Extensions

### ColorExtensions

- `string Color.ToHex(bool includeHex = true)`
- `Color Color.WithA(float a)`

### LayerMaskExtensions

- `bool LayerMask.Contains(int layer)`

### MonobehaviourExtensions

- `Coroutine MonoBehaviour.Defer(System.Action callback, YieldInstruction yieldInstruction = null)`: The same as creating a coroutine that does callback after `yield return yieldInstruction`. Useful for making one-shop coroutines.

### VectorExtensions

Projections
- `Vector3 Vector3.ProjectXY(), Vector3.ProjectXZ(), Vector3.ProjectYZ()`: Projects onto plane. Eg. `(1,2,3).ProjectXZ() == (1,0,2)`. Useful in 2D for projecting onto the 2D plane, and useful in 3D for projecting onto the ground, etc.
- `Vector2 Vector3.XY(), Vector3.XZ(), Vector3.YZ()`: Swizzling. Eg. `(1,2,3).XZ() == (1,3)`

Mutators
- `Vector3 Vector3.WithX(), Vector3.WithY(), Vector3.WithZ()`
- `Vector2 Vector2.WithX(), Vector2.WithY()`
- `Vector3 Vector2.WithZ()`

Geomtry
- `float Vector3.Volume(), int Vector3Int.Volume()`
  - Finds the volume of a box with side lengths in the vector (note: NOT half lengths as used by `BoxCollider`)

Component-wise operations
- `Vector3 Vector3.Abs()`: Component-wise absolute value
- Division
  - `Vector2 Vector2.Divide(Vector2 b), Vector2.Divide(Vector2Int b)`
  - `Vector2Int Vector2Int.Divide(Vector2Int b)`
  - `Vector2 Vector2.Divide(Vector2 b), Vector2.Divide(Vector2Int b)`
  - `Vector3 Vector3.Divide(Vector3 b), Vector3.Divide(Vector3Int b)`
  - `Vector3Int Vector3Int.Divide(Vector3Int b)`
  - `Vector3 Vector3Int.Divide(Vector3 b)`
- Multiplication (Hammard product)
  - `Vector2 Vector2.Hammard(Vector2 b), Vector2.Hammard(Vector2Int b)`
  - `Vector2Int Vector2Int.Hammard(Vector2Int b)`
  - `Vector2 Vector2Int.Hammard(Vector2 b)`
  - `Vector3 Vector3.Hammard(Vector3 b), Vector3.Hammard(Vector3Int b)`
  - `Vector3Int Vector3Int.Hammard(Vector3Int b)`
  - `Vector3 Vector3Int.Hammard(Vector3 b)`
- Rem (Modulo)
  - `Vector3Int Vector3Int.Rem(Vector3Int b), Vector3Int.Rem(int b)`
  - `Vector3 Vector3.Rem(Vector3 b), Vector3.Rem(float b)`

## Math

### Math

- `class IntRange`
  - Contains `Min, MaxExclusive`, as well as getters `Max = MaxInclusive`
  - Contains `IntRange.Random`, which will sample the range
- `Vector3 Math.Min(Vector3 v1, Vector3 v2)`: Component-wise min
- `Vector3 Math.Max(Vector3 v1, Vector3 v2)`: Component-wise max
- `float Rem(float a, float b), int Rem(int a, int b)`: Computed euclidean remainder (nonnegative equivalent to `a % b`). You can be sure that `a % c == b % c` if they have the same remainder, regardless of if they are negative.
- `float NormalizeAngle360(float angle)`: normalized angle to `[0, 360)`
- `float NormalizeAngle180(float angle)`: normalized angle to range `[-180, 180)`
- `float ClampAngle(float angle, float min, float max)`: makes sure angle is between `min` and `max`, normalizing all angles

### SpringVector3, SpringTransform

Damped, forced harmonic oscillator solver as provided by [t3ssel8r](https://www.youtube.com/watch?v=KPoeNZZ6H4s). Don't ask me how it works lol, I took 1 differential equations class.

This is useful to smoothly transition from one point to another in real time using delta-time (as opposed to using keyframes and time). This is a more customizable alternative to `Vector3.Lerp` and `Vector3.SmoothDamp`, which both achieve the same thing.

`SpringVector3` is the API:

```c#
SpringVector3 springVector = new(
 initialValue: Vector3.zero,
 omega: 2*Mathf.PI, // angular velocity (frequency)
 zeta: 1, // damping coefficient (0 = undamped, (0,1) = damped, 1 = critically damped, (1,infty) = underdamped)
 r: 0 // responsitivity: positive = velocity starts in right direction, negative values = velocity starts in opposite direction
);

springVector.TargetValue = Vector3.one;
springVector.Update(1.0f/60.0f);

Debug.Log(springVector.CurrentValue);
```

`SpringTransform` does this automatically to the current transform with the `TargetValue` set to the position of the `Target` transform. In addition, it provides an editor to preview the value changing, and allows you to change the values of `SpringVector3` at runtime.

Similarly, there is `SpringFloat` and `SpringFloatValue` (a `MonoBehaviour` container for `SpringFloat`).

## OS

### Executable

- `string Executable.Which(string executable)`: Equivalent of Linux `which` (only tested on Linux)

### Terminal

- `string Terminal.GetSensibleTerminal()`: Gets a sensible terminal absolute path using the `TERM` and `TERMINAL` environment variables and a set of defaults (in opinionated order).

## Physics

- `class Physics.RaycastDistanceComparer : IComparer<RaycastHit>`: Implements `IComparer<RaycastHit>` to compare `RaycastHit` distances. Use this in `Array.Sort` or similar functions to sort an array of `RaycastHit`s by distance.
- `int Physics.RaycastNonAllocSorted(..., IComparer<RaycastHit> comparer = null)`: Equivalent to `Physics.RaycastNonAlloc` but sorts afterwards, using the default comparer of `Kutie.Physics.RaycastDistanceComparer`
- `int Physics.RaycastAllSorted(... IComparer<RaycastHit> comparer)`: Same as above but with `UnityEngine.Physics.RaycastAll`.
- `int Physics.BoxCastNonAllocSorted(... IComparer<RaycastHit> comparer)`: Same as above but with `UnityEngine.Physics.BoxCastNonAlloc`.
- `int Physics.BoxCastAllSorted(... IComparer<RaycastHit> comparer)`: Same as above but with `UnityEngine.Physics.BoxCastAll`.

## Random

- `Vector3 Random.Range(Vector3 min, Vector3 max)`: Component-wise random

## Singleton

### SingletonMonoBehaviour

Provides the class `SingletonMonoBehaviour<T>` that contains a static `Instance` parameter that is set on `Awake`.

Example use:

```c#
public class PlayerMovement : SingletonMonoBehaviour<T>
{
 Camera camera;

 // make sure to override Awake
 protected override void Awake(){
  base.Awake();

  camera = Camera.main;
 }
}
```

It also provides a static `System.Action<T> NewInstanceEvent`. This is useful if one singleton gets destroyed and a new one is created and you want to register callbacks, etc.

It also provides an overridable property `virtual protected bool DestroyNewInstance => true`. Overriding this will change the behavior when multiple `T`s are created. If `DestroyNewInstance` is true, it will destroy the one created last. Otherwise, it will destroy the one created first and set the `Instance` to the new one.

### SingletonScriptableObject

Provides the `SingletonScriptableObject<T>` class that contains a static `Instance` parameter that is set on `OnEnable`. Note that the instance is stored as a weak reference.

WARNING: In build, ScriptableObjects are loaded *when they are first used*. Thus, even if there is an unreferenced ScriptableObject in your project and it works *in the editor*, it will not work in a built project. In addition, if you switch scenes to a scene that no longer references a ScriptableObject, the ScriptableObject Instance will be null.

This class might be very bad practice! Try to find other ways to structure your data.

## Time

### TimeLayer and ScalableTimeLayer

`TimeLayer` is a class that follows `Time.time` but allows pausing. Similarly, `ScalableTimeLayer` does the same but allows time scaling. These are meant to be used somewhat instead of `Time.timeScale`, however they do not affect any Unity systems like `Physics`.

In addition, there are two ScriptableObjects that act as containers for TimeLayers.

## Object

- `class Kutie.ObjectComparer : IComparer<UnityEngine.Object>`: class that implements `IComparer<UnityEngine.Object>` using `Object.GetInstanceID()`. You can use this to, for example, store Objects in a BST (`SortedSet<T>`) instead of a `HashSet<T>`.