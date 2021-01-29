using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Imageで円を描くコンポーネント
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
[ExecuteInEditMode]
public class TextMeshProUguiRing : MonoBehaviour
{
	[SerializeField, Range(-Mathf.PI, Mathf.PI)]
	private float rotateOffset = default;
	[SerializeField, Range(-Mathf.PI, Mathf.PI)]
	private float rotateInterval = default;

	private TextMeshProUGUI tmp;
	private RectTransform tmpTransform;
	private RectTransform canvasTransform;
	private Material material;

	private int circleCenterNameID = 0;
	private int circleOffsetNameID = 0;
	private int circleIntervalNameID = 0;


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
		this.tmp = GetComponent<TextMeshProUGUI>();
		this.tmpTransform = this.tmp.rectTransform;
		this.material = this.tmp.fontSharedMaterial;
		this.canvasTransform = GetComponentInParent<CanvasScaler>().GetComponent<RectTransform>();

		this.circleCenterNameID = Shader.PropertyToID("_RotateCenter");
		this.circleOffsetNameID = Shader.PropertyToID("_RotateOffset");
		this.circleIntervalNameID = Shader.PropertyToID("_RotateInterval");
	}

	private void Update()
	{
		var canvasScale = this.canvasTransform.lossyScale;
		var worldPosDiff = this.tmpTransform.position - this.canvasTransform.transform.position;
		worldPosDiff.x /= canvasScale.x;
		worldPosDiff.y /= canvasScale.y;
		worldPosDiff.z /= canvasScale.z;

		this.material.SetVector(this.circleCenterNameID, worldPosDiff);
		this.material.SetFloat(this.circleIntervalNameID, this.rotateInterval);
		this.material.SetFloat(this.circleOffsetNameID, this.rotateOffset);
	}
}
