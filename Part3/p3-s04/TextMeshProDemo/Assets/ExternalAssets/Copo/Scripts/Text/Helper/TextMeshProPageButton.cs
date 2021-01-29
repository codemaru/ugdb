using UnityEngine;
using UnityEngine.UI;


namespace TMProSample
{
	/// <summary>
	/// ページ送りをボタンで行う為の設定
	/// </summary>
	public class TextMeshProPageButton : MonoBehaviour
	{
		/// <summary>
		///	次のページに進むボタン
		/// </summary>
		[SerializeField]
		private Button nextButton = default;

		/// <summary>
		/// 前のページに戻るボタン
		/// </summary>
		[SerializeField]
		private Button backButton = default;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Awake()
		{
			var text = GetComponent<TMPro.TextMeshProUGUI>();

			nextButton.onClick.AddListener(() =>
			{
				if (text.textInfo.pageCount - 1 < text.pageToDisplay) { return; }
				text.pageToDisplay++;
			});

			backButton.onClick.AddListener(() =>
			{
				// ページは1から開始.
				if (text.pageToDisplay <= 1) { return; }
				text.pageToDisplay--;
			});
		}
	}
}
