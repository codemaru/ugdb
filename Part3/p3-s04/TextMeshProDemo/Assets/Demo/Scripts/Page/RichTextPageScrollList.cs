using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;


namespace TMProSample
{
	/// <summary>
	/// 「リッチテキスト」ページのリスト表示制御	
	/// </summary>
	public class RichTextPageScrollList : MonoBehaviour, IPageScrollList<RichTextPageData>, IPointerClickHandler
	{
		/// <summary>
		/// リッチテキストのタグ
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI tagText = default;

		/// <summary>
		/// リッチテキストの例文
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI MsgText = default;

		/// <summary>
		/// リッチテキストの例文（Page機能利用時）
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI MsgPageText = default;

		/// <summary>
		/// Y座標の位置カーブ
		/// </summary>
		[SerializeField]
		private AnimationCurve positionYCurve = default;

		/// <summary>
		/// タップキャンセルフラグ（リンクタグのタップ回避）
		/// </summary>
		private bool cancelTap = false;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Awake()
		{
			var handler = this.GetComponentInChildren<TextMeshProEventHandler>(true);
			handler.onLinkSelection.AddListener((linkID, linkText, linkIndex) =>
			{
				cancelTap = !string.IsNullOrWhiteSpace(linkID);
			});
		}

		/// <summary>
		/// Override IPageScrollList Function
		/// </summary>
		public void OnUpdateList(RichTextPageData data, int index, float ratio)
		{
			Vector3 pos = transform.localPosition;
			pos.y = this.positionYCurve.Evaluate(ratio);
			transform.localPosition = pos;
		}

		/// <summary>
		/// Override IPageScrollList Function
		/// </summary>
		public void OnChangeList(RichTextPageData data, int index, float ratio)
		{
			tagText.text = data.tag;
			MsgText.text = string.Join(Environment.NewLine, data.richtext);
			MsgPageText.text = MsgText.text;

			MsgText.transform.parent.gameObject.SetActive(!data.isPage);
			MsgPageText.transform.parent.gameObject.SetActive(data.isPage);
		}

		/// <summary>
		/// Override IPointerClickHandler Function
		/// </summary>
		public void OnPointerClick(PointerEventData eventData)
		{
			if (cancelTap)
			{
				cancelTap = false;
				return;
			}

			bool flag = !MsgText.enableAutoSizing;
			MsgText.fontSize = 60.0f;
			MsgText.enableAutoSizing = flag;
			MsgText.richText = !flag;

			MsgPageText.fontSize = MsgText.fontSize;
			MsgPageText.enableAutoSizing = MsgText.enableAutoSizing;
			MsgPageText.richText = MsgText.richText;
			MsgPageText.overflowMode = flag ? TextOverflowModes.Overflow : TextOverflowModes.Page;
		}
	}
}