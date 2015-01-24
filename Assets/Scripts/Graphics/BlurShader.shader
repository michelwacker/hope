Shader "Hope/BlurShader" {
	Properties {
		_MainTex ("MainTex", 2D) = "Diffuse" {}
	   _Color ("Color", Color) = (0.1,0.8,0.1,1)
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
                //float4 _Lightmap_ST;
               
           		sampler2D _MainTex;
                sampler2D _VolumetricLight;
                //sampler2D _Lightmap;
                float4 _Color;
           
                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float4 color : TEXCOORD2;
                };
           
                v2f vert(appdata_tan v) {
                    v2f o;
                    
				    o.color = _Color;
                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
                    
                    return o;
                }
           
                
           		const float blurSize = 1.0/256.0;
           		
                float4 frag(v2f IN) : COLOR {
                   half4 sum = half4(0.0);
				   sum += tex2D(_MainTex, half2(IN.uv.x - 4.0*blurSize, IN.uv.y)) * 0.05;
				   sum += tex2D(_MainTex, half2(IN.uv.x - 3.0*blurSize, IN.uv.y)) * 0.09;
				   sum += tex2D(_MainTex, half2(IN.uv.x - 2.0*blurSize, IN.uv.y)) * 0.12;
				   sum += tex2D(_MainTex, half2(IN.uv.x - blurSize, IN.uv.y)) * 0.15;
				   sum += tex2D(_MainTex, half2(IN.uv.x, IN.uv.y)) * 0.16;
				   sum += tex2D(_MainTex, half2(IN.uv.x + blurSize, IN.uv.y)) * 0.15;
				   sum += tex2D(_MainTex, half2(IN.uv.x + 2.0*blurSize, IN.uv.y)) * 0.12;
				   sum += tex2D(_MainTex, half2(IN.uv.x + 3.0*blurSize, IN.uv.y)) * 0.09;
				   sum += tex2D(_MainTex, half2(IN.uv.x + 4.0*blurSize, IN.uv.y)) * 0.05;
 
                   return sum;
                }
            ENDCG 
        }
     
   }
}
