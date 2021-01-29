Shader "Copo/FontMagicRingBackgroundShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			uniform float _AnimationTime;
			uniform float2 _AspectMatch;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = (i.uv * 2.0 - 1.0) * _AspectMatch;
				float bg = 1.0 - cos(uv.y * UNITY_PI * 0.25);
				fixed4 col = { 1,1,1,1 };
				col.rgb *= pow(bg, 2);
				col.rgb = pow(col.rgb, 1.0 / 2.2);
				return col;
            }
            ENDCG
        }
    }
}
