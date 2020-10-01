# FrameLord

FrameLord is a set of classes that extends Unity functionality.

# Compatibility

Unity 2019.2 or above

# How to use

1. Add the following line to the file Packages/manifest.json inside the dependencies definition

`"com.dedalord.framelord": "https://github.com/ruizdiego/framelord.git#upm"`

Example of the whole file:

```javascript
{
  "dependencies": {
    "com.unity.ext.nunit": "1.0.0",
    "com.unity.ide.rider": "1.1.0",

    // ... other packages ...

    "com.unity.modules.vr": "1.0.0",
    "com.unity.modules.wind": "1.0.0",
    "com.unity.modules.xr": "1.0.0",

    // add this
    "com.dedalord.framelord": "https://github.com/ruizdiego/framelord.git#upm"
}
```

2. Open the Unity project. The package will be pulled from the repo by IDE.

3. Keep it updated as any other package

# Documentation

Download the repo and look into the "Documention" folder.
