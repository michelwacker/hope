Shader "Hope/menuShader" {
	Properties {
	   _MainTex ("MainTex", 2D) = "Diffuse" {}
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
                float4 _InsanityVector;
                float4 _InsanityVector2;
           
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
                    half2 uv2 = IN.uv;
                    half2 uv3 = IN.uv;
                    
                    uv2.x += _InsanityVector.x;
                    uv2.y += _InsanityVector.y;
                    
                    uv3.x += _InsanityVector2.x;
                    uv3.y += _InsanityVector2.y;
                    
                    half4 diffuseTex2 = tex2D (_MainTex, uv2);
                    half4 diffuseTex3 = tex2D (_MainTex, uv3);

                    diffuseTex2.a = 0.3f;
                    diffuseTex2.b = 1.0f;
                    diffuseTex3.a = 0.3f;
                    diffuseTex3.r = 1.0f;
                    diffuseTex *= diffuseTex2 * diffuseTex3;
                    
                    
                    
                   //return diffuseTex + volumetricLight;
                   return diffuseTex;
                }
            ENDCG 
        }
     
   }
}

