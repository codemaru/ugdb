using UnityEngine;


namespace TMProSample
{
	/// <summary>
	/// 「インスペクター」ページのデータ
	/// </summary>
	[System.Serializable]
	public struct InspectorPageData
	{
		[SerializeField]
		public string prefab;

		[SerializeField]
		public string iconTexture;

		[SerializeField]
		public string iconSprite;

		[SerializeField]
		public string text;

		[SerializeField]
		public string textJapanese;

		[SerializeField]
		public bool showVolumetric;
	}


	/// <summary>
	/// 「インスペクター」ぺ―ジのリスト制御システム
	/// </summary>
	public class InspectorPageScrollData : PageScrollData<InspectorPageData, InspectorPageScrollList>
	{
	}
}
