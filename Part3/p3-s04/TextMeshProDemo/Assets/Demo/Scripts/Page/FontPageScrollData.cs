using UnityEngine;


namespace TMProSample
{
	/// <summary>
	/// 「フォント」ページのデータ
	/// </summary>
	[System.Serializable]
	public struct FontPageData
	{
		[SerializeField]
		public string prefab;
	}


	/// <summary>
	/// 「フォント」ぺ―ジのリスト制御システム
	/// </summary>
	public class FontPageScrollData : PageScrollData<FontPageData, FontPageScrollList>
	{
		/// <summary>
		///	3Dを扱う親オブジェクト
		/// </summary>
		[SerializeField]
		private GameObject font3DRoot = default;

		/// <summary>
		/// キャンバス描画のページ背景
		/// </summary>
		[SerializeField]
		private GameObject pageBackgroundImage = default;

		/// <summary>
		/// Unity Override Function
		/// </summary>
		private void OnEnable()
		{
			if (font3DRoot != null)
				font3DRoot.SetActive(true);

			if (pageBackgroundImage != null)
				pageBackgroundImage.SetActive(false);
		}

		/// <summary>
		/// Unity Override Function
		/// </summary>
		private void OnDisable()
		{
			if (font3DRoot != null)
				font3DRoot.SetActive(false);

			if (pageBackgroundImage != null)
				pageBackgroundImage.SetActive(true);
		}
	}
}