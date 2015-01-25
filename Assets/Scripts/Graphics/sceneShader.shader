Shader "Hope/sceneShader" {
	Properties {
	   _MainTex ("MainTex", 2D) = "Diffuse" {}
	   _VolumetricLight ("VolumetricLight", 2D) = "Bump1" {}
	   _Lightmap ("Lightmap", 2D) = "Bump2" {}
	   _AmbientColor ("AmbientColor", Color) = (0.1,0.8,0.1,1)
	   _LightColor ("LightColor", Color) = (0.1,0.8,0.1,1)
	}


SubShader {
   Tags { "RenderType"="Opaque" }
   LOD 10
   
   Pass {
   		Lighting On
   		CGPROGRAM
                #pragma glsl
                #pragma target 3.0
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
                
                float4 _MainTex_ST;
                float4 _VolumetricLight_ST;
                float4 _Lightmap_ST;
               
           		sampler2D _MainTex;
                sampler2D _VolumetricLight;
                sampler2D _Lightmap;
                float4 _AmbientColor;
                float4 _LightColor;
           
                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float4 color : TEXCOORD2;
                };
           
                v2f vert(appdata_tan v) {
                    v2f o;
                    
				    o.color = _AmbientColor;
                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
                    
                    return o;
                }
           
                
           
                float4 frag(v2f IN) : COLOR {
                    half4 diffuseTex = tex2D (_MainTex, IN.uv);
                    half4 diffuseTex2 = tex2D (_MainTex, IN.uv + 0.01f);
                    half4 volumetricLight = tex2D (_VolumetricLight, IN.uv);
                    half4 lightMapTex = tex2D (_Lightmap, IN.uv);
                    half4 fragColor;
                    diffuseTex2.a = 0.3f;
                    diffuseTex2.b = 1.0f;
                    //diffuseTex.b += diffuseTex2.b;
                    if(volumetricLight.a > 0.1) {
                    	if(lightMapTex.a > 0) {
                    		//fragColor = diffuseTex/2 + half4(0.4,0.4,0,1f);//diffuseTex + half4(0.5,0.5,0.5,0.5);
                    		fragColor = ((diffuseTex/2) * _AmbientColor) * _LightColor;//diffuseTex + half4(0.5,0.5,0.5,0.5);
                    	} else {
                    		fragColor = ((diffuseTex/4) * _AmbientColor) * _LightColor;//diffuseTex + half4(0.5,0.5,0.5,0.5);
                    	}
                    } else {
                    	fragColor = ((diffuseTex/4) * _AmbientColor);// / (1,1,1,1); //half4(1,0,0,0.5);//diffuseTex;
                    }
                    
                    
                   //return diffuseTex + volumetricLight;
                   return fragColor;
                }
            ENDCG 
        }
     
   }
}

