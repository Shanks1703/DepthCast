# DepthCast

DepthCast allows for raycasting using depth map. It is useful for raycasting on skinned meshes or tesselated surfaces. DepthCast ray can only be shot from the main camera position.

## Installation

Import the built .unitypackage directly in your project.

## Use

To shoot a ray simply use :
``` csharp
DepthCastHit hit;
DepthCast.Cast(Input.mousePosition, out hit);
```

DepthCastHit contains informations about hit point.

## Compatibility

Should work with following pipelines :
  - HDRP
  - URP
  - Built-in

Only tested on DirectX, may not work properly with other APIs
