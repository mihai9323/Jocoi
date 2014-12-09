Shader "Custom/MixTextures" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Pattern1("Pattern1", 2D) = "black" {}
		_Pattern2("Pattern2", 2D) = "black" {}
		_Pattern3("Pattern3", 2D) = "black" {}
		_Pattern4("Pattern4", 2D) = "black" {}
		
		_DefaultColor("Default Color", Color) = (1,1,1)
		_P1Color("Pattern1 Color",Color) = (1,1,1)
		_P2Color("Pattern2 Color",Color) = (1,1,1)
		_P3Color("Pattern3 Color", Color) = (1,1,1)
		_P4Color("Pattern4 Color", Color) = (1,1,1)
		
	}
	SubShader {
		pass{
			Tags { "RenderType"="Opaque" }
			
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
	
			uniform sampler2D _MainTex,_Pattern1,_Pattern2,_Pattern3,_Pattern4;
			uniform float4 _MainTex_ST,_Pattern1_ST,_Pattern2_ST,_Pattern3_ST,_Pattern4_ST;
			
			uniform fixed4 _DefaultColor, _P1Color, _P2Color, _P3Color, _P4Color;
			
			struct vertexInput {
				float4 vertex: POSITION; //position (in object coordinates)
				float4 texcoord: TEXCOORD0; //refers to 0th texture (UV) and is between 0 and 1
			};
			
			struct fragmentInput{
				float4 pos: SV_POSITION;
				
				half2 uv: TEXCOORD0;
			
			};
			
			fragmentInput vert(vertexInput i){
				fragmentInput o;
				o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
				o.uv = i.texcoord; //Transfrom tex takes care of the tiling and offset of the texture
				
				
				return o;
			}
			
			half4 frag(fragmentInput i):COLOR
			{
				fixed4 t1 =  tex2D(_MainTex,i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				fixed p1 =tex2D(_Pattern1,i.uv.xy * _Pattern1_ST.xy + _Pattern1_ST.zw);
				fixed p2 = tex2D(_Pattern2,i.uv.xy * _Pattern2_ST.xy + _Pattern2_ST.zw);
				fixed p3 = tex2D(_Pattern3,i.uv.xy * _Pattern3_ST.xy + _Pattern3_ST.zw);
				fixed p4 =tex2D(_Pattern4,i.uv.xy * _Pattern4_ST.xy + _Pattern4_ST.zw);
				fixed4 mainTexColor = t1 * _DefaultColor;
				fixed4 p1TexColor = p1 * _P1Color;
				fixed4 p2TexColor = p2 * _P2Color;
				fixed4 p3TexColor = p3 * _P3Color;
				fixed4 p4TexColor = p4 * _P4Color;
				
				fixed4 finalColor ;
				finalColor = lerp(mainTexColor,p1TexColor,p1);
				finalColor = lerp(finalColor,  p2TexColor,p2);
				finalColor = lerp(finalColor,  p3TexColor,p3);
				finalColor = lerp(finalColor,  p4TexColor,p4);
				
				//finalColor = mainTexColor * lerp((1,1,1),p1TexColor,p1) * lerp((1,1,1),p2TexColor,p2) * lerp((1,1,1),p3TexColor,p3) * lerp((1,1,1),p4TexColor,p4);
				return finalColor;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
