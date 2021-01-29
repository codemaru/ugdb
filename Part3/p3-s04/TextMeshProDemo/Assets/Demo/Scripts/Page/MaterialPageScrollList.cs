using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace TMProSample
{
	/// <summary>
	/// 「マテリアル」ページのリスト表示制御	
	/// </summary>
	public class MaterialPageScrollList : MonoBehaviour, IPageScrollList<MaterialPageData>
	{
		/// <summary>
		/// 英語タイトル
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI titleText = default;

		/// <summary>
		/// 日本語タイトル
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI japaneseText = default;

		/// <summary>
		/// UI.Button
		/// </summary>
		[SerializeField]
		private Button button = default;

		/// <summary>
		/// 説明ページ(Popup)
		/// </summary>
		[SerializeField]
		private PopupPage descriptionPage = default;

		/// <summary>
		/// Y座標の位置カーブ
		/// </summary>
		[SerializeField]
		private AnimationCurve positionYCurve = default;

		/// <summary>
		/// 表示しているデータ
		/// </summary>
		private MaterialPageData data;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Start()
		{
			this.button.onClick.RemoveAllListeners();
			this.button.onClick.AddListener(OnListClick);
		}

		/// <summary>
		/// UI.Button(リスト本体)のクリック時
		/// </summary>
		private void OnListClick()
		{
			this.descriptionPage.CreateAndOpenPage(this.data.prefab);
		}

		/// <summary>
		/// Override IPageScrollList Function
		/// </summary>
		public void OnUpdateList(MaterialPageData data, int index, float ratio)
		{
			Vector3 pos = transform.localPosition;
			pos.y = this.positionYCurve.Evaluate(ratio);
			transform.localPosition = pos;
		}

		/// <summary>
		/// Override IPageScrollList Function
		/// </summary>
		public void OnChangeList(MaterialPageData data, int index, float ratio)
		{
			this.data = data;
			this.titleText.text = data.text;
			this.japaneseText.text = data.textJapanese;
		}
	}
}