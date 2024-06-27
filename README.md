# Unique GameObjects

Using Game Creator 2, adds a new component giving a GameObject a unique ID as well as a Getter to get GameObject's
by ID either through the inspector or via code.

# How to use?:

- Add the `InstanceGuid` component to any GameObject. It will automatically be tracked and available anywhere inside of
  Game Creator 2. GameObject that have an empty Guid will not be added and an error will be shown as well as printed in
  the Debug console at runtime.

- You can use one of the several getters to get a unique GameObjects or their ID:
    - (GameObject) `Game Objects/Unique Game Object`
    - (GameObject) `Game Objects/Unique Game Object by ID`
    - (String) `Game Objects/Unique Game Object ID`

Use the API to get a Game Object through code using either a string or IdString instance.

```csharp
public GameObject GetGameObject(string uniqueIdString)
{
    return UniqueGameObjectsManager.GetByID(uniqueIdString);
}

public string GetGameObjectName(IdString uniqueId)
{
    return UniqueGameObjectsManager.GetByID(uniqueId).name;
}

public string GetGameObjectUniqueID(GameObject gameObject)
{
    return gameObject.GetUniqueID();
}
```

### Dependencies

This package requires Game Creator 2 to work and should be compatible with all Unity versions that Game Creator 2
supports.

### How to Contribute:

Feel free to contribute to this development by forking this repository or just create issues!

### How to Install:

Use the Package Manager to install this package using the following git URL:

`https://github.com/Legi428/Unique-GameObjects.git`
