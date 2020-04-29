// Unlit color shader. Very simple textured and colored shader.
// - no lighting
// - no lightmap support
// - per-material color

// Change this string to move shader to new location
Shader "Unlit/Texture Colored" {
    Properties {
        // Adds Color field we can modify
        _Color ("Main Color", Color) = (1, 1, 1, 1)        
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }

    Category
     {
         Lighting Off
         ZWrite Off
                 //ZWrite On  // uncomment if you have problems like the sprite disappear in some rotations.
         Cull back
         Blend SrcAlpha OneMinusSrcAlpha
                 //AlphaTest Greater 0.001  // uncomment if you have problems like the sprites or 3d text have white quads instead of alpha pixels.
         Tags {Queue=Transparent}
 
         SubShader
         {
 
              Pass
              {
                         SetTexture [_MainTex]
                         {
                     ConstantColor [_Color]
                    Combine Texture * constant
                 }
             }
         }
     }
}