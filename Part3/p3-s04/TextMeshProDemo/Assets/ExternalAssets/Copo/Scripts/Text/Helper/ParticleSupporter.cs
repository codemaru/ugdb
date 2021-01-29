using UnityEngine;


namespace TMProSample
{
	/// <summary>
	/// パーティクル（マテリアル）にパラメータを渡す
	/// </summary>
	[RequireComponent(typeof(ParticleSystem))]
	[ExecuteInEditMode]
	public class ParticleSupporter : MonoBehaviour
	{
		/// <summary>
		/// ParticleSystemRenderer
		/// </summary>
		private new ParticleSystemRenderer renderer;

		/// <summary>
		/// Cached Transform
		/// </summary>
		private Transform cachedTransform;

		/// <summary>
		/// ParticleSystemRendererで使用しているマテリアル
		/// </summary>
		private Material material;

		/// <summary>
		/// "_Center"のID
		/// </summary>
		private int centerShaderNameID;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Awake()
		{
			Setup();
		}

#if UNITY_EDITOR
		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void OnValidate()
		{
			Setup();
		}
#endif

		/// <summary>
		/// セットアップ
		/// </summary>
		private void Setup()
		{
			this.renderer = GetComponent<ParticleSystemRenderer>();
			this.material = this.renderer.sharedMaterial;
			this.cachedTransform = transform;
			this.centerShaderNameID = Shader.PropertyToID("_Center");
		}

		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Update()
		{
			if (this.material != null && this.cachedTransform != null)
				this.material.SetVector(this.centerShaderNameID, this.cachedTransform.position);
		}
	}
}
