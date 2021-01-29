Shader "Copo/FontToromanBackgroundShader"
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

			float2x2 rotate(float angle)
			{
				float s = sin(angle), c = cos(angle);
				return float2x2(c, s, -s, c);
			}

			float hash(float2 v)
			{
				return frac(sin(dot(v, float2(742.0, 347.0))) * 6725.2856);
			}

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

				fixed4 col = fixed4(0.4, 0.3, 0.25, 1.0);
				float time = _Time.y;

				// slash
				{
					float2 uvLocal = uv;
					uvLocal.x -= uvLocal.y * 1.01;

					float2 uvID1 = floor(uvLocal * 5.0);
					float2 uvID2 = floor(uvLocal * 5.0 + 0.5);
					uvLocal = frac(uvLocal * 5.0) * 2.0 - 1.0;
					uvLocal.y = uv.y;
					uvID1.y = 0.0;
					uvID2.y = 0.0;

					const float SLASH_WIDTH = 0.4;
					const float SLASH_SMOOTH = 0.2;
					float slashDist = uvLocal.x;
					slashDist =
						smoothstep(-SLASH_WIDTH - SLASH_SMOOTH, -SLASH_WIDTH, slashDist) *
						smoothstep( SLASH_WIDTH + SLASH_SMOOTH,  SLASH_WIDTH, slashDist);

					// green
					float uvHash = hash(uvID1);
					float slide = uv.x + uv.y + uvHash + time * 2.0;
					slide = smoothstep(-0.5, 0.0, sin(slide * 0.5));
					col.rgb = lerp(col.rgb, fixed3(0.675, 0.72, 0.63), slashDist * slide);

					// brown
					uvHash = hash(uvID2);
					slide = uv.x + uv.y + uvHash * 2.0 + time * 4.0;
					slide = smoothstep(0.0, 0.2, sin(slide * 0.5));
					col.rgb = lerp(col.rgb, fixed3(0.97, 0.72, 0.77) * 0.3, (1.0 - slashDist) * slide);
				}

				// fade - square
				{
					float uvBlockSize = 5.0 + 2.0 * _AnimationTime;
					float2 uvBlock = frac(uv * uvBlockSize) * 2.0 - 1.0;
					float2 uvBlockID = floor(uv * uvBlockSize);
					float uvBlockHash = hash(uvBlockID);

					float blockTime = _AnimationTime * 6.0 - distance(uvBlockID, float2(0, 0)) * 0.2;
					blockTime = saturate(blockTime - uvBlockHash - step(3.0, abs(uvBlockID.y + 0.5)) * 3.0);
					uvBlock = mul(rotate(blockTime * blockTime * UNITY_PI * 1.5), uvBlock);

					float uvBlockStep = 1.0 - blockTime * 1.1;
					uvBlockStep = smoothstep(uvBlockStep, uvBlockStep + 0.1, max(abs(uvBlock.x), abs(uvBlock.y)));

					float gradTime = _AnimationTime * 0.8;
					float grad = step(0.9 - gradTime, uvBlockStep);
					col.rgb *= max(grad, gradTime + 0.2);
				}

				return col;
            }
            ENDCG
        }
    }
}
