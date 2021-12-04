# DepthCast

DepthCast allows for raycasting using depth map. It allows up to 256 ray calls per frame within a single pass. It is useful for raycasting on skinned meshes or tesselated surfaces. DepthCast ray can only be shot from the main camera position.

## Installation

Import the built .unitypackage directly in your project.

## Use

To shoot a ray simply use :
``` csharp
Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
DepthCastHit hit;
DepthCast.Cast(mousePos, out hit);
```

DepthCastHit contains informations about hit point and normal.

## Compatibility

Should work with following pipelines :
  - HDRP
  - URP
  - Built-in

Only tested on DirectX, may not work properly with other APIs
