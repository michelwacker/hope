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
           
           		
                float4 frag(v2f IN) : COLOR {
                   half4 sum = half4(0.0);

				    //our original texcoord for this fragment
				    float2 tc = IN.uv;

				    //the amount to blur, i.e. how far off center to sample from 
				    //1.0 -> blur by one pixel
				    //2.0 -> blur by two pixels, etc.
				    float blur = 5;//radius/resolution; 

				    //the direction of our blur
				    //(1.0, 0.0) -> x-axis blur
				    //(0.0, 1.0) -> y-axis blur
				    float hstep = 1.0;//dir.x;
				    float vstep = 0.0;//dir.y;

				    //apply blurring, using a 9-tap filter with predefined gaussian weights

				    sum += tex2D(_MainTex, float2(tc.x - 4.0*blur*hstep, tc.y - 4.0*blur*vstep)) * 0.0162162162;
				    sum += tex2D(_MainTex, float2(tc.x - 3.0*blur*hstep, tc.y - 3.0*blur*vstep)) * 0.0540540541;
				    sum += tex2D(_MainTex, float2(tc.x - 2.0*blur*hstep, tc.y - 2.0*blur*vstep)) * 0.1216216216;
				    sum += tex2D(_MainTex, float2(tc.x - 1.0*blur*hstep, tc.y - 1.0*blur*vstep)) * 0.1945945946;

				    sum += tex2D(_MainTex, float2(tc.x, tc.y)) * 0.2270270270;

				    sum += tex2D(_MainTex, float2(tc.x + 1.0*blur*hstep, tc.y + 1.0*blur*vstep)) * 0.1945945946;
				    sum += tex2D(_MainTex, float2(tc.x + 2.0*blur*hstep, tc.y + 2.0*blur*vstep)) * 0.1216216216;
				    sum += tex2D(_MainTex, float2(tc.x + 3.0*blur*hstep, tc.y + 3.0*blur*vstep)) * 0.0540540541;
				    sum += tex2D(_MainTex, float2(tc.x + 4.0*blur*hstep, tc.y + 4.0*blur*vstep)) * 0.0162162162;

				    //discard alpha for our simple demo, multiply by vertex color and return
                   return sum;
                }
            ENDCG 
        }
     
   }
}
