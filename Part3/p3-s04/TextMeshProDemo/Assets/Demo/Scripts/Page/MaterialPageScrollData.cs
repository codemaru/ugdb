using UnityEngine;


namespace TMProSample
{
	/// <summary>
	/// 「マテリアル」ぺ―ジのデータ
	/// </summary>
	[System.Serializable]
	public struct MaterialPageData
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
	}


	/// <summary>
	/// 「マテリアル」ぺ―ジのリスト制御システム
	/// </summary>
	public class MaterialPageScrollData : PageScrollData<MaterialPageData, MaterialPageScrollList>
	{
	}
}
