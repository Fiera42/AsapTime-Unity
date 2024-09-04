# AsapTime
A custom update method called on Update() AND FixedUpdate() 

## About

Unity's FixedUpdate() method is great for having constant game updates, especially during lag spikes; however it lacks the
speed and frequency of Update() so I merged thoses two update methods into a single one : AsapUpdate(). This method
is called after every Update() and FixedUpdate() and use a deltaTime just like Update().

## Installation

Hit the "Add package from git URL..." button in the package manager, and enter https://github.com/Fiera42/AsapTime-Unity.git

In older Unity versions, or if you don't want to open Unity, modify your Packages/Manifest.json to include this package:
```
{
  "dependencies": {
    "com.fiera.asaptime": "https://github.com/Fiera42/AsapTime-Unity.git",
    ...
```

## Quick Use

Once installed, you can use AsapTime by subscribing to it's update event on any object, even non-monobehaviour objects.
You have access to `AsapTime.DeltaTime` and `AsapTime.Time` just like the regular Update() method,
you can also enable a `IsInFixedUpdate` field by uncommenting the related code in the file

```cs
public class SomeClass : MonoBehaviour {
    // Register to the AsapUpdate
    public void OnEnable()
    {
        AsapTime.OnAsapUpdate += SomeMethod;
    }

    // Un-suscribe when disabled
    public void OnDisable()
    {
        AsapTime.OnAsapUpdate -= SomeMethod;
    }

    // Method called at every AsapUpdate
    private void SomeMethod()
    {
        // Usage of AsapTime.DeltaTime similar to Time.DeltaTime
        transform.position += transform.up * AsapTime.DeltaTime;
    }
}
```
