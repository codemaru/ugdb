Shader "Copo/UI/ImagePatternShader"
{
    Properties
    {
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Smoothness("smoothness", Range(0.0001, 1.0)) = 0.0001

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		_Rotation("Rotation", Float) = 0.0
		_Scale("Scale", Float) = 1.0
		_Offset("Offset", Vector) = (0, 0, 0, 0)
		_Color1("Color 1", Color) = (1,1,1,1)
		_Color2("Color 2", Color) = (0,0,0,1)
		[Toggle(MIRROR)] _Mirror("Mirror", Float) = 0.0
    }

    SubShader
    {
        Tags
		{	
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

        Pass
        {
			Name "Circle"

		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile __ MIRROR

			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 uv  : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Smoothness;
			float _Rotation;
			float _Scale;
			float4 _Offset;
			float4 _Color1;
			float4 _Color2;
			float _Mirror;

			v2f vert(appdata_t v)
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.vertex = UnityObjectToClipPos(v.vertex);
				OUT.uv = (v.vertex.xy + _Offset.xy) * _Scale;
				OUT.uv = TRANSFORM_TEX(OUT.uv, _MainTex);
				OUT.color = v.color;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				float s = sin(_Rotation), c = cos(_Rotation);
				float2x2 rotation = float2x2(c, s, -s, c);
				fixed4 texColor = tex2D(_MainTex, mul(rotation, IN.uv));
#ifdef MIRROR
				texColor = min(texColor, tex2D(_MainTex, mul(rotation, float2(-IN.uv.x, IN.uv.y))));
#endif
				return IN.color * lerp(_Color1, _Color2, texColor.r);
			}
		ENDCG
        }
    }
}
