
Shader "Copo/FontHonobonoPopBackgroundShader"
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

			// Copyright © 2018 Inigo Quilez
			float opSmoothUnion(float d1, float d2, float k)
			{
				float h = max(k - abs(d1 - d2), 0.0);
				return min(d1, d2) - h * h*0.25 / k;
			}

			fixed4 polka_dot(float2 uv, float radius, float smooth, float separate)
			{
				float2 uvGrid = floor(uv);
				float2 uvLocal = frac(uv + float2(0.0, uvGrid.x * 0.5)) * 2.0 - 1.0;
				uvGrid = floor(uv + float2(0.0, uvGrid.x * 0.5));
				float sphere = 1.0e+4;

				float angle = (uvGrid.x % 2 == 0) ? UNITY_PI * 0.25 : -UNITY_PI * 0.25;
				for(int i=-1 ; i<=1 ; i+=2)
					sphere = opSmoothUnion(sphere, length(uvLocal + mul(rotate(angle), float2(0.75 * separate, 0.0) * i)), 0.2);

				return
					smoothstep(-radius - smooth, -radius, sphere) *
					smoothstep(radius + smooth, radius, sphere);
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

				float rot = (1.0 - _AnimationTime);
				rot = 1.0 - rot * rot;
				uv = mul(rotate(3 * rot), uv);
				uv = uv * (6.0 - 4.5 * _AnimationTime);

				float separate = smoothstep(0.6, 0.9, _AnimationTime);
				float dotSize = lerp(-0.05, 0.7, _AnimationTime) + _AnimationTime * (sin(_Time.w) * 0.01 + 0.01) - separate * 0.4;
				float dotSmooth = lerp(0.05, 0.02, _AnimationTime);
				float dot = polka_dot(uv, dotSize, dotSmooth, separate);

				fixed4 bgCol = lerp(saturate(fixed4(0.3, 0.4, 0.5, 1) * 3.0), fixed4(0.3, 0.4, 0.5, 1), _AnimationTime);
				fixed4 col = lerp(bgCol, fixed4(1,1,1,1), dot);
				return col;
            }
            ENDCG
        }
    }
}
