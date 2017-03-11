Shader "Simple Scene Transitions/Simple"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_TransitionTex("Transition Texture", 2D) = "white" {}
		_ScreenTex("Screen Texture", 2D) = "white" {}
		_Color("Screen Color", Color) = (1,1,1,1)
		_Cutoff("Cutoff", Range(0, 1)) = 0
		_Fade("Fade", Range(0, 1)) = 0
	}

		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always
			Fog { Mode off }


			Pass
			{

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma fragmentoption ARB_precision_hint_fastest 

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				float4 _MainTex_TexelSize;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv = v.uv;

					return o;
				}

				sampler2D _TransitionTex;
				sampler2D _ScreenTex;
				sampler2D _MainTex;

				fixed _Cutoff;
				fixed _Fade;
				fixed4 _Color;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed transit = tex2D(_TransitionTex, i.uv).r;
					fixed4 screenTex = tex2D(_ScreenTex, i.uv);

					fixed4 col = tex2D(_MainTex, i.uv);
					fixed lerpVal = step(transit, _Cutoff);
					fixed4 screenCol = _Color*screenTex;

					col = lerp(col, screenCol, _Fade);

					fixed stepp = step(transit, (_Cutoff-0.01)*1.02);
					
					return col * (1 - stepp) + screenCol * stepp;
				}					
				ENDCG
			}
		}
		Fallback off
}
