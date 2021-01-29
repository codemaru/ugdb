using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;


/// <summary>
/// Imageで円を描くコンポーネント
/// </summary>
[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class ImageCircle : MonoBehaviour
{
	[System.Serializable]
	private struct Circle
	{
		[SerializeField, ColorUsage(true)]
		Color color;
		[SerializeField, Range(-1.0f, 1.0f)]
		float radiusMin;
		[SerializeField, Range(-1.0f, 1.0f)]
		float radiusMax;
		[SerializeField, Range(-Mathf.PI, Mathf.PI)]
		float radianMin;
		[SerializeField, Range(-Mathf.PI, Mathf.PI)]
		float radianMax;
	};

	[SerializeField]
	private Circle[] circles = default;
	private ComputeBuffer circleBuffer;
	private int circleNumShaderNameID;
	private int circleBufferShaderNameID;
	private int circleCenterShaderNameID;

	private Image image;
	private RectTransform imageTransform;
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
		this.image = GetComponent<Image>();
		this.imageTransform = this.image.rectTransform;
		this.material = image.material;
		this.circleNumShaderNameID = Shader.PropertyToID("_CircleNum");
		this.circleBufferShaderNameID = Shader.PropertyToID("_Circles");
		this.circleCenterShaderNameID = Shader.PropertyToID("_Center");

		if (0 < this.circles.Length)
		{
			this.material.SetFloat(circleNumShaderNameID, this.circles.Length);
			this.circleBuffer = new ComputeBuffer(this.circles.Length, Marshal.SizeOf(typeof(Circle)));
			this.circleBuffer.SetData(circles);
		}
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
		if (this.circleBuffer != null)
			this.circleBuffer.SetData(circles);

		this.material.SetVector(this.circleCenterShaderNameID, this.imageTransform.pivot);
		this.material.SetBuffer(circleBufferShaderNameID, this.circleBuffer);
	}
}
