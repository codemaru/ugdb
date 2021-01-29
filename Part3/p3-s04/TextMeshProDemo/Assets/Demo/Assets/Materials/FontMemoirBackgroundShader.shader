Shader "Copo/FontMemoirBackgroundShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

			// Copyright © 2018 Inigo Quilez
			float opSmoothUnion(float d1, float d2, float k)
			{
				float h = max(k - abs(d1 - d2), 0.0);
				return min(d1, d2) - h * h*0.25 / k;
			}

			// Copyright © 2018 Inigo Quilez
			float sdStar5(in float2 p, in float r, in float rf)
			{
				const float2 k1 = float2(0.809016994375, -0.587785252292);
				const float2 k2 = float2(-k1.x, k1.y);
				p.x = abs(p.x);
				p -= 2.0*max(dot(k1, p), 0.0)*k1;
				p -= 2.0*max(dot(k2, p), 0.0)*k2;
				p.x = abs(p.x);
				p.y -= r;
				float2 ba = rf * float2(-k1.y, k1.x) - float2(0, 1);
				float h = clamp(dot(p, ba) / dot(ba, ba), 0.0, r);
				return length(p - ba * h) * sign(p.y*ba.x - p.x*ba.y);
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f IN) : SV_Target
            {
				fixed4 col = {0,0,0,1};
				float2 uv = (IN.uv * 2.0 - 1.0) * _AspectMatch;
				float time = _Time.y;
				float dist = 1e+4;
				float paddingTopBottom = 0.3 * (1.0 - _AnimationTime);

				// wave (top & bottom)
				{
					float d = abs(uv.y) + sin(uv.x * UNITY_PI - time) * 0.05 - 0.6 + paddingTopBottom;
					dist = min(dist, -d);
				}

				// wave ball (top & bottom)
				[unroll]
				for (int i = 1; i <= 10; ++i)
				{
					float2 rand = hash(i * 2.02, i * 0.53);
					float ballSpeed = lerp(0.05, 0.2, rand.x);
					float2 ballPos = rand * 2.0 - 1.0;
					ballPos.x += frac(time * ballSpeed) * 5.0 - 2.5;
					ballPos.y = 0.7 + 0.1 * ballPos.y + paddingTopBottom;

					ballPos.x = uv.x - (ballPos.x - 0.5 * step(0.0, uv.y));
					ballPos.y = uv.y - (ballPos.y - 1.1 * step(uv.y, 0.0) - paddingTopBottom);

					float size = hash(float2(i, 3.0)) * 0.1;
					dist = opSmoothUnion(dist, length(ballPos) - size, 0.3);
				}

				// balls
				[unroll]
				for (int k = 1; k <= 5; ++k)
				{
					float2 rand = hash(k * 6.72, k * 4.93);
					float ballSpeed = lerp(0.15, 0.4, rand.x);
					float2 ballPos = rand * 0.5 - 0.25;
					ballPos.x += frac(time * ballSpeed) * 4.0 - 2.0;

					float size = hash(float2(k, 6.0)) * 0.15 + 0.05;
					dist = opSmoothUnion(dist, length(uv - ballPos) - size, 0.2);
				}

				uv += float2(time, 0.0);
				float2 uvLocal = frac(uv * 10.0) * 2.0 - 1.0;
				float starHash = hash(floor(uv * 10.0));
				float starRot = starHash + sin(time + starHash);
				float starRotS = sin(starRot), starRotC = cos(starRot);
				uvLocal = mul(float2x2(starRotC, -starRotS, starRotS, starRotC), uvLocal);

				fixed4 bgCol = { 0,0,0,1 };
				float bgStripe = sin(uv.y * UNITY_PI * 8.0 + uv.y * _AnimationTime * 20.0);
				float bgStart = sdStar5(uvLocal, 0.5, 0.5) - sin(time + starHash) * 0.05;
				bgCol.rgb = lerp(fixed3(1, 1, 1), fixed3(0.95, 0.96, 0.93), smoothstep(bgStripe - 0.1, bgStripe, 0.0));
				bgCol.rgb = lerp(bgCol.rgb, fixed3(0.9, 0.8, 1), smoothstep(bgStart - 0.01, bgStart, 0.1) * step(sin(uv.x * 3.0) + uv.y * 4.0, -0.3));

				const float SMOOTH = 0.001;
				dist = smoothstep(dist - SMOOTH, dist + SMOOTH, 0.01);
				col.rgb = lerp(bgCol, fixed3(1, 0.85, 0.94), dist);

				// degamma
				col.rgb = pow(col.rgb, 1.0 / 2.2);
				return col;
            }
            ENDCG
        }
    }
}
