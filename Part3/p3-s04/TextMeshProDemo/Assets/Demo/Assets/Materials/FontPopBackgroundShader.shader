Shader "Copo/FontPopBackgroundShader"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
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

			float hash(float2 v)
			{
				return frac(sin(dot(v, float2(742.0, 347.0))) * 6725.2856);
			}

			float2 hash(float a, float b)
			{
				return float2(
					frac(sin(dot(float2(a, 0.0), float2(742.0, 347.0))) * 6725.2856),
					frac(sin(dot(float2(0.0, b), float2(742.0, 347.0))) * 6725.2856)
				);
			}

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col = tex2D(_MainTex, i.uv);
				float2 uv = (i.uv * 2.0 - 1.0) * float2(1.7778, 1.0);
				float time = _Time.y;

				float2 uvLocal = uv;
				uvLocal.x -= uvLocal.y * 0.5;
				float2 uvID = floor(uvLocal * 5.0);
				uvLocal = frac(uvLocal * 5.0) * 2.0 - 1.0;
				uvLocal.y = uv.y;
				uvID.y = 0.0;
				float uvHash = hash(uvID);

				const float SLASH_WIDTH  = 0.4;
				const float SLASH_SMOOTH = 0.2;
				float slashDist = uvLocal.x;
				slashDist =
					smoothstep(-SLASH_WIDTH - SLASH_SMOOTH, -SLASH_WIDTH, slashDist) *
					smoothstep( SLASH_WIDTH + SLASH_SMOOTH,  SLASH_WIDTH, slashDist);

				float slide = uv.x + uv.y + uvHash + time * 2.0;
				slide = smoothstep(-0.5, 0.0, sin(slide * 0.5));

				col.rgb = lerp(col.rgb, fixed3(0.7, 1.0, 0.8), slashDist * slide);

				slide = uv.x + uv.y + time * 4.0;
				slide = smoothstep(0.0, 0.2, sin(slide * 0.5));
				col.rgb = lerp(col.rgb, fixed3(0.97, 0.72, 0.77), (1.0 - slashDist) * slide);

				return col;
            }
            ENDCG
        }
    }
}
