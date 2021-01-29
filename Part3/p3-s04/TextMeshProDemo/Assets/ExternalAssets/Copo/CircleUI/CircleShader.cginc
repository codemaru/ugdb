
struct Circle
{
	float4 color;
	float radiusMin;
	float radiusMax;
	float radianMin;
	float radianMax;
};

float4 circle(float2 uv, float2 center, int num, StructuredBuffer<Circle> circles, float smoothness, float ambient, float4 ambientColor)
{
	float2 diff = uv - center;
	float dist = length(diff);
	float radian = atan2(diff.y, diff.x);
	float4 color = { 0,0,0,0 };

	for (int i = 0; i < num; ++i)
	{
		Circle circle = circles[i];

		float range =
			smoothstep(circle.radiusMin, circle.radiusMin + smoothness, dist) *
			smoothstep(circle.radiusMax, circle.radiusMax - smoothness, dist);

		float ambientRange =
			smoothstep(circle.radiusMin - smoothness, circle.radiusMin + ambient, dist) *
			smoothstep(circle.radiusMax + smoothness, circle.radiusMax - ambient, dist);

		fixed4 circleColor = lerp(ambientColor, circle.color, ambientRange);
		color = lerp(color, circleColor, range);
	}

	return color;
}

float4 circle(float2 uv, float2 center, int num, StructuredBuffer<Circle> circles, float smoothness)
{
	float2 diff = uv - center;
	float dist = length(diff);
	float radian = atan2(diff.y, diff.x);
	float4 color = { 0,0,0,0 };

	for (int i = 0; i < num; ++i)
	{
		Circle circle = circles[i];

		float range =
			smoothstep(circle.radiusMin, circle.radiusMin + smoothness, dist) *
			smoothstep(circle.radiusMax, circle.radiusMax - smoothness, dist);

		color = lerp(color, circle.color, range);
	}

	return color;
}
