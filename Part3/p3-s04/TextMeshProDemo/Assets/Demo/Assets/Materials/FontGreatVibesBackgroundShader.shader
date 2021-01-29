
Shader "Copo/FontGreatVibesBackgroundShader"
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

				float waveOffset = UNITY_PI * 5.0 * _AnimationTime + _Time.z;
				float waveHeight = _AnimationTime * 2.0 - 1.0;

				fixed4 col = fixed4(0.8, 0.78, 0.5, 1);

				float wave = 0.0;
				wave = sin(UNITY_PI * uv.x + waveOffset) * 0.1 + uv.y;
				wave = smoothstep(waveHeight + 0.01, waveHeight, wave);
				col = lerp(col, fixed4(0.8, 0.7, 0.9, 1), wave);

				wave = sin(UNITY_PI * uv.x + waveOffset + 0.25 * (waveOffset + 10.8)) * 0.1 + uv.y;
				wave = smoothstep(waveHeight + 0.01, waveHeight, wave);
				col = lerp(col, fixed4(0.5, 0.4, 0.7, 1), wave);

				wave = sin(UNITY_PI * uv.x + waveOffset + 0.45 * (waveOffset + 10.5)) * 0.1 + uv.y;
				wave = smoothstep(waveHeight + 0.01, waveHeight, wave);
				col = lerp(col, lerp(fixed4(0.3, 0.2, 0.5, 1), fixed4(0.5, 0.6, 0.9, 1), lerp(0.0, saturate(0.5 - uv.y), _AnimationTime)), wave);

				return col;
            }
            ENDCG
        }
    }
}
