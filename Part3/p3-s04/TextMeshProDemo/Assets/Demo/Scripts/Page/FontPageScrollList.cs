using UnityEngine;


namespace TMProSample
{
	/// <summary>
	/// 「フォント」ページのリスト表示制御	
	/// </summary>
	public class FontPageScrollList : MonoBehaviour, IPageScrollList<FontPageData>
	{
		/// <summary>
		/// 制御するアニメーション名（固定）
		/// </summary>
		static private int AnimatorStateID = Animator.StringToHash("Animation");

		/// <summary>
		///	3Dを扱う親オブジェクト
		/// </summary>
		[SerializeField]
		private GameObject font3DRoot = default;

		/// <summary>
		/// 3Dページのアニメーター
		/// </summary>
		private Animator page3DAnimator = default;

		/// <summary>
		/// 3Dページのインスタンス
		/// </summary>
		private GameObject page3DInstance = default;


		/// <summary>
		/// Override IPageScrollList Function
		/// </summary>
		public void OnUpdateList(FontPageData data, int index, float ratio)
		{
			if (this.page3DAnimator != null)
				this.page3DAnimator.Play(AnimatorStateID, 0, ratio);
		}

		/// <summary>
		/// Override IPageScrollList Function
		/// </summary>
		public void OnChangeList(FontPageData data, int index, float ratio)
		{
			if (this.page3DInstance != null)
				DestroyImmediate(this.page3DInstance);

			var prefab = Resources.Load<GameObject>(data.prefab);
			if (prefab != null)
			{
				this.page3DInstance = Instantiate(prefab, this.font3DRoot.transform);
				this.page3DInstance.transform.localPosition = Vector3.zero;
				this.page3DInstance.transform.localRotation = Quaternion.identity;
				this.page3DAnimator = this.page3DInstance.GetComponent<Animator>();
				this.page3DAnimator.Play(AnimatorStateID, 0, 0.0f);
			}

			Resources.UnloadUnusedAssets();
		}
	}
}