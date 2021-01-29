using UnityEngine;


namespace TMProSample
{
	/// <summary>
	/// カメラの最背面に背景を表示する
	/// </summary>
	[ExecuteInEditMode]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class BackgroundQuad : MonoBehaviour
	{
		private static int MaterialAnimationTimeID = Shader.PropertyToID("_AnimationTime");
		private static int MaterialAspectMatchID = Shader.PropertyToID("_AspectMatch");

		/// <summary>
		///	適応するカメラ
		/// </summary>
		[SerializeField]
		private Camera targetCamera = default;

		/// <summary>
		/// 背景マテリアル
		/// </summary>
		[SerializeField]
		private Material material = default;

		/// <summary>
		/// カメラの子オブジェクトかどうか
		/// </summary>
		[SerializeField]
		private bool isCameraChild = true;

		/// <summary>
		/// 背景の縦横合わせ
		/// </summary>
		[SerializeField, Range(0.0f, 1.0f)]
		private float screenMatchWidthOrHeight = default;

		/// <summary>
		/// アニメーション時間
		/// </summary>
		[SerializeField, Range(0.0f, 1.0f)]
		private float animationTime = default;

		/// <summary>
		/// 背景のMesh
		/// </summary>
		private Mesh mesh;
		/// <summary>
		/// 背景のMeshFilter
		/// </summary>
		private MeshFilter meshFilter;
		/// <summary>
		/// 背景のMeshRenderer
		/// </summary>
		private MeshRenderer meshRenderer;
		/// <summary>
		/// Setupフラグ(OnValidateでScreen情報を取得すると異常な数値が返る為、苦肉の策)
		/// </summary>
		private bool isSetup;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Start()
		{
			Cleanup();
			Setup();
		}

		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void OnValidate()
		{
			Cleanup();
			Setup();
		}

		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Update()
		{
			if (this.isSetup)
			{
				this.isSetup = false;

				if (this.targetCamera != null)
				{
					this.mesh = CreateMesh(this.targetCamera);
					this.meshFilter.mesh = this.mesh;
				}
			}

			if (this.material != null)
			{
				var aspectMatch = Vector2.Lerp(
					new Vector2(1.0f, (float)Screen.height / (float)Screen.width),  // 横合わせ
					new Vector2((float)Screen.width / (float)Screen.height, 1.0f),  // 縦合わせ
					this.screenMatchWidthOrHeight
				);

				this.material.SetFloat(MaterialAnimationTimeID, this.animationTime);
				this.material.SetVector(MaterialAspectMatchID, aspectMatch);
			}
		}

		/// <summary>
		/// Cleanup
		/// </summary>
		private void Cleanup()
		{
			if (this.mesh != null)
				DestroyImmediate(this.mesh);
		}

		/// <summary>
		/// Setup
		/// </summary>
		private void Setup()
		{
			this.meshFilter = GetComponent<MeshFilter>();
			this.meshRenderer = GetComponent<MeshRenderer>();
			this.meshRenderer.sharedMaterial = material;
			isSetup = true;
		}

		/// <summary>
		///	背景メッシュの生成
		/// </summary>
		private Mesh CreateMesh(Camera camera)
		{
			float farZ = camera.farClipPlane - 1.0e-3f;
			var point0 = new Vector3(0f, 0f, farZ);
			var point1 = new Vector3(0f, 1f, farZ);
			var point2 = new Vector3(1f, 0f, farZ);
			var point3 = new Vector3(1f, 1f, farZ);
			point0 = camera.ViewportToWorldPoint(point0);
			point1 = camera.ViewportToWorldPoint(point1);
			point2 = camera.ViewportToWorldPoint(point2);
			point3 = camera.ViewportToWorldPoint(point3);

			Matrix4x4 worldToLocalMatrix;
			if (this.isCameraChild)
				worldToLocalMatrix = camera.transform.worldToLocalMatrix;
			else
				worldToLocalMatrix = this.transform.worldToLocalMatrix;

			point0 = worldToLocalMatrix.MultiplyPoint(point0);
			point1 = worldToLocalMatrix.MultiplyPoint(point1);
			point2 = worldToLocalMatrix.MultiplyPoint(point2);
			point3 = worldToLocalMatrix.MultiplyPoint(point3);

			var mesh = new Mesh();

			mesh.vertices = new Vector3[6]
			{
				point0, point1, point2,
				point3, point2, point1,
			};

			mesh.normals = new Vector3[6]
			{
				Vector3.back, Vector3.back, Vector3.back,
				Vector3.back, Vector3.back, Vector3.back,
			};

			mesh.triangles = new int[6]
			{
				0, 1, 2, 3, 4, 5,
			};

			mesh.uv = new Vector2[6]
			{
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 0f),
				new Vector2(1f, 1f),
				new Vector2(1f, 0f),
				new Vector2(0f, 1f),
			};

			return mesh;
		}
	}
}

