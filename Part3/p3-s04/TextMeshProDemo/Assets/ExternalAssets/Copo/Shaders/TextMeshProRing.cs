using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Imageで円を描くコンポーネント
/// </summary>
[RequireComponent(typeof(TextMeshPro))]
[ExecuteInEditMode]
public class TextMeshProRing : MonoBehaviour
{
	[SerializeField, Range(-Mathf.PI, Mathf.PI)]
	private float rotateOffset = default;
	[SerializeField, Range(-Mathf.PI, Mathf.PI)]
	private float rotateInterval = default;
	[SerializeField]
	private bool animRotation = false;

	private RectTransform tmpTransform;
	private Material material;

	private int circleCenterNameID = 0;
	private int circleOffsetNameID = 0;
	private int circleIntervalNameID = 0;
	private float time;


	private void Awake()
	{
		Setup();
	}

	private void OnValidate()
	{
		Setup();
	}

	private void Setup()
	{
		var tmp = GetComponent<TextMeshPro>();
		this.tmpTransform = tmp.rectTransform;
		this.material = tmp.fontSharedMaterial;

		this.circleCenterNameID = Shader.PropertyToID("_RotateCenter");
		this.circleOffsetNameID = Shader.PropertyToID("_RotateOffset");
		this.circleIntervalNameID = Shader.PropertyToID("_RotateInterval");
	}

	private void Update()
	{
		if (animRotation)
			this.time += Time.deltaTime;

		this.material.SetVector(this.circleCenterNameID, this.tmpTransform.pivot);
		this.material.SetFloat(this.circleIntervalNameID, this.rotateInterval);
		this.material.SetFloat(this.circleOffsetNameID, this.rotateOffset + this.time);
	}
}
