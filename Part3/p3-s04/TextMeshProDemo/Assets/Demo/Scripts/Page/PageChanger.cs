using UnityEngine;
using UnityEngine.UI;


namespace TMProSample
{
	/// <summary>
	/// ページ切り替え
	/// </summary>
	[RequireComponent(typeof(Animation))]
	public class PageChanger : MonoBehaviour
	{
		/// <summary>
		/// ボタン／アニメーション関連
		/// </summary>
		[System.Serializable]
		public struct Page
		{
			[SerializeField]
			public Button button;
			[SerializeField]
			public AnimationClip sequence;
		}

		/// <summary>
		/// ボタン／アニメーション関連
		/// </summary>
		[SerializeField]
		private Page[] pages = default;

		/// <summary>
		/// アニメーションシステム
		/// </summary>
		private Animation animationPlayer;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Start()
		{
			this.animationPlayer = GetComponent<Animation>();

			for (int i = 0; i < pages.Length; ++i)
			{
				var page = pages[i];
				if (page.button != null)
				{
					page.button.onClick.AddListener(() =>
					{
						if (this.animationPlayer.isPlaying) return;
						this.animationPlayer.AddClip(page.sequence, page.sequence.name);
						this.animationPlayer.Play(page.sequence.name);
					});
				}
			}
		}
	}
}
