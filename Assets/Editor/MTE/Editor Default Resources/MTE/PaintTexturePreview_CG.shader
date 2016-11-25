// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'

Shader "Hidden/MTE/PaintTexturePreview_CG"
{
	Properties
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MaskTex ("Mask (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader
	{
		Tags { "Queue"="Transparent" }

		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			struct v2f
			{
				float4 pos : POSITION;
				float4 tex0 : TEXCOORD0;
				float4 tex1 : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _MaskTex;

			float4x4 unity_Projector;
			float4 _MainTex_ST;

			v2f vert(appdata_base v)
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.tex1 = mul(unity_Projector, v.vertex);

				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float4 col = tex2D(_MainTex, i.tex0.xy);
				float4 mask = tex2Dproj(_MaskTex, i.tex1);

				float4 o;
				o.rgb = col;
				o.a = mask.a;

				return o;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
