Shader "Hope/sceneShader" {
	Properties {
	   _MainTex ("MainTex", 2D) = "Diffuse" {}
	   _VolumetricLight ("VolumetricLight", 2D) = "Bump1" {}
	   _Lightmap ("Lightmap", 2D) = "Bump2" {}
	   _Scenemap ("Scenemap", 2D) = "Bump3" {}
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
                sampler2D _Scenemap;
                float4 _AmbientColor;
                float4 _LightColor;
                float4 _InsanityVector;
                float4 _InsanityVector2;
                int _Day;
                float _DayRatio;
           
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
					half4 volumetricLight = tex2D (_VolumetricLight, IN.uv);
                    //half4 lightMapTex = tex2D (_Lightmap, IN.uv);
                    half4 sceneMapTex = tex2D (_Scenemap, IN.uv);
                    half4 fragColor;
                    if(sceneMapTex.a == 0) {
	                    if(volumetricLight.a > 0.1 && _Day == 1) {
	                    	//if(lightMapTex.a > 0) {
	                    		//fragColor = diffuseTex * _LightColor ;
	                    	//} else {
	                    		//fragColor = diffuseTex * _LightColor/2;
	                    	//}
	                    	fragColor = diffuseTex * _LightColor;
	                    } else {
	                    	fragColor = diffuseTex * _AmbientColor;
	                    }
	               	} else {
	               		fragColor = diffuseTex;
	               	}
                    
                   return fragColor;
                }
                
            ENDCG 
        }
     
   }
}

