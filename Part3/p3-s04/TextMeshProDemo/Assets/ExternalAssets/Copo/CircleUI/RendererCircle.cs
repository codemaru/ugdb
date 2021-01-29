using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;


/// <summary>
/// Imageで円を描くコンポーネント
/// </summary>
[RequireComponent(typeof(Renderer))]
[ExecuteInEditMode]
public class RendererCircle : MonoBehaviour
{
	[System.Serializable]
	private struct Circle
	{
		[SerializeField, ColorUsage(true)]
		Color color;
		[SerializeField, Range(0.0f, 1.0f)]
		float radiusMin;
		[SerializeField, Range(0.0f, 1.0f)]
		float radiusMax;
		[SerializeField, Range(-Mathf.PI, Mathf.PI)]
		float radianMin;
		[SerializeField, Range(-Mathf.PI, Mathf.PI)]
		float radianMax;
	};

	private const int MaxCircleNum = 4;

	[SerializeField]
	private Vector2 center = default;

	[SerializeField]
	private Color ambient = default;

	[SerializeField, Range(0.0f, 1.0f)]
	private float ambientRatio = default;

	[SerializeField]
	private Circle circle0 = default;

	[SerializeField]
	private Circle circle1 = default;

	[SerializeField]
	private Circle circle2 = default;

	[SerializeField]
	private Circle circle3 = default;

	private Circle[] circles = new Circle[MaxCircleNum];
	private ComputeBuffer circleBuffer;
	private int circleNumShaderNameID;
	private int circleBufferShaderNameID;
	private int circleCenterShaderNameID;
	private int circleAmbientShaderNameID;
	private int circleAmbientRatioShaderNameID;

	private new Renderer renderer;
	private Transform rendererTransform;
	private Material material;

	
	private void Awake()
	{
		Release();
		Setup();
	}

	private void OnValidate()
	{
		if (this.circleBuffer != null)
			this.circleBuffer.SetData(this.circles);
	}

	private void OnDestroy()
	{
		Release();
	}

	private void Setup()
	{
		this.renderer = GetComponent<Renderer>();
		this.rendererTransform = this.renderer.transform;
		this.material = this.renderer.sharedMaterial;
		this.circleNumShaderNameID = Shader.PropertyToID("_CircleNum");
		this.circleBufferShaderNameID = Shader.PropertyToID("_Circles");
		this.circleCenterShaderNameID = Shader.PropertyToID("_Center");
		this.circleAmbientShaderNameID = Shader.PropertyToID("_Ambient");
		this.circleAmbientRatioShaderNameID = Shader.PropertyToID("_AmbientRatio");

		this.circles[0] = circle0;
		this.circles[1] = circle1;
		this.circles[2] = circle2;
		this.circles[3] = circle3;

		this.material.SetFloat(circleNumShaderNameID, MaxCircleNum);
		this.circleBuffer = new ComputeBuffer(MaxCircleNum, Marshal.SizeOf(typeof(Circle)));
		this.circleBuffer.SetData(circles);
	}

	private void Release()
	{
		if (this.circleBuffer != null)
		{
			this.circleBuffer.Release();
			this.circleBuffer = null;
		}
	}

	private void Update()
	{
		this.circles[0] = circle0;
		this.circles[1] = circle1;
		this.circles[2] = circle2;
		this.circles[3] = circle3;

		this.circleBuffer.SetData(this.circles);
		this.material.SetBuffer(this.circleBufferShaderNameID, this.circleBuffer);
		this.material.SetColor(this.circleAmbientShaderNameID, this.ambient);
		this.material.SetFloat(this.circleAmbientRatioShaderNameID, this.ambientRatio);
		this.material.SetVector(this.circleCenterShaderNameID, this.center);
		this.material.SetBuffer(circleBufferShaderNameID, this.circleBuffer);
	}
}
