using UnityEngine;
using System.Linq;


namespace TMProSample
{
	/// <summary>
	/// 行間をピッタリ合わせる
	/// Alignment の Flush の縦軸版
	/// </summary>
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
	public class TextMeshProLineSpaceFitter : MonoBehaviour
	{
		/// <summary>
		/// 本体
		/// </summary>
		private TMPro.TextMeshProUGUI text = default;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Start()
		{
			this.text = GetComponent<TMPro.TextMeshProUGUI>();

			if (this.text == null)
				return;

			// 行数を取得
			int lineCount = this.text.text.Split('\n').Count();
			if (lineCount <= 1)
				return;

			// 文字サイズの算出
			var faceInfo = this.text.font.faceInfo;
			float faceSize = text.fontSize / faceInfo.pointSize;

			// テキストの高さを算出
			float lineHeight = faceInfo.lineHeight * faceSize;
			float textHeight = lineCount * lineHeight;

			// 空いたスペースを算出し、行間に割り当てる
			var rectTransform = this.GetComponent<RectTransform>();
			float space = rectTransform.rect.height - textHeight;
			float lineSpace = space / (lineCount - 1);

			this.text.lineSpacing = lineSpace / faceSize;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Update()
		{
			Start();
		}
#endif
	}
}
