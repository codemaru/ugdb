using UnityEngine;
using System.Linq;


namespace TMProSample
{
	/// <summary>
	/// 「リッチテキスト」ページのデータ
	/// </summary>
	[System.Serializable]
	public struct RichTextPageData
	{
		[SerializeField]
		public string tag;

		[SerializeField]
		public string[] richtext;

		[SerializeField]
		public bool isPage;
	}


	/// <summary>
	/// 「リッチテキスト」ぺ―ジのリスト制御システム
	/// </summary>
	public class RichTextPageScrollData : PageScrollData<RichTextPageData, RichTextPageScrollList>
	{
		/// <summary>
		/// リンクページ(Popup)
		/// </summary>
		[SerializeField]
		private PopupPage linkPage = default;

		/// <summary>
		/// サイドバー ガイドテキスト
		/// </summary>
		[SerializeField]
		private TMPro.TextMeshProUGUI guideText = default;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Awake()
		{
			var handler = this.GetComponentInChildren<TextMeshProEventHandler>(true);
			handler.onLinkSelection.AddListener(OnClickLink);
		}

		/// <summary>
		/// Override PageScrollData Function
		/// </summary>
		protected override void OnStart()
		{
			guideText.text = string.Concat(this.data.Select((d, idx) =>
			{
				string suffix = (idx < this.data.Length - 1) ? "\n" : "";
				return d.tag + suffix;
			}));
		}

		/// <summary>
		/// リンククリック時
		/// </summary>
		/// <note>
		/// RichTextPage
		/// ┗ ScrollView
		///     ┗ List - Origin
		///         ┗ Msg-Background
		///             ┗  Msg-Text  ←  OnWordSelectionに対して設定
		/// </note>
		public void OnClickLink(string linkID, string linkText, int linkIndex)
		{
			if (string.IsNullOrWhiteSpace(linkID))
				return;

			var pageInstance = linkPage.OpenPage();
			var textObject = pageInstance.transform.Find("Msg-Text");
			var textComponent = textObject.GetComponent<TMPro.TextMeshProUGUI>();
			textComponent.text = linkID;
		}
	}
}
