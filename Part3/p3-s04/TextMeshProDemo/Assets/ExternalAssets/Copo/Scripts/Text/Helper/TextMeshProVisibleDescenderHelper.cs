using UnityEngine;
using TMPro;


namespace TMProSample
{
	/// <summary>
	/// 「インスペクター」ページのVisibleDescenderの使用例用
	/// </summary>
	public class TextMeshProVisibleDescenderHelper : MonoBehaviour
	{
		/// <summary>
		/// トグル（チェックボックス）切り替え時
		/// </summary>
		/// <param name="value"></param>
		public void OnChangeToggle(bool value)
		{
			var text = GetComponent<TextMeshProUGUI>();

			if (value)
			{
				text.useMaxVisibleDescender = true;
				text.alignment = TextAlignmentOptions.Bottom;
			}
			else
			{
				text.useMaxVisibleDescender = false;
				text.alignment = TextAlignmentOptions.Top;
			}
		}
	}
}
