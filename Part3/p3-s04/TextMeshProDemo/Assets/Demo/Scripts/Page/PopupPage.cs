using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace TMProSample
{
	/// <summary>
	/// ポップアップ系ページ
	/// ・説明ページ(DescriptionPage)やリンクページ(LinkPage)
	/// ・背景をタップすると子オブジェクトを破棄する
	/// </summary>
	public class PopupPage : MonoBehaviour
	{
		/// <summary>
		/// 閉じた時のイベント
		/// </summary>
		public UnityAction onClose { private get; set; }

		/// <summary>
		/// ポップアップページの親
		/// </summary>
		[SerializeField]
		private GameObject rootObject = default;

		/// <summary>
		/// ポップアップページのグレー背景(閉じる用)
		/// </summary>
		[SerializeField]
		private GameObject backgroundObject = default;

		/// <summary>
		/// 最後に開いたページ
		/// </summary>
		private GameObject lastPageInstance = default;

		/// <summary>
		///	閉じる用クリックハンドラ
		/// </summary>
		private class ClickHandler : MonoBehaviour, IPointerClickHandler
		{
			public System.Action<PointerEventData> onPointerClick;

			public void OnPointerClick(PointerEventData eventData)
			{
				onPointerClick?.Invoke(eventData);
			}
		}

		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Awake()
		{
			this.backgroundObject.AddComponent<ClickHandler>().onPointerClick = (e) =>
			{
				this.gameObject.SetActive(false);
				rootObject.SetActive(false);

				if (this.lastPageInstance != null)
				{
					Destroy(this.lastPageInstance);
					this.lastPageInstance = null;
				}

				onClose?.Invoke();
			};
		}

		/// <summary>
		/// ページを開く
		/// </summary>
		/// <param name="prefabPath">ページプレハブのResourcesPath</param>
		/// <returns></returns>
		public GameObject CreateAndOpenPage(string prefabPath)
		{
			this.gameObject.SetActive(true);
			this.rootObject.SetActive(true);

			if (this.lastPageInstance != null)
				DestroyImmediate(this.lastPageInstance);

			var pagePrefab = Resources.Load<GameObject>(prefabPath);
			if (pagePrefab != null)
			{
				var pageInstance = this.lastPageInstance = Instantiate(pagePrefab);
				var pageTransform = pageInstance.GetComponent<RectTransform>();
				if (pageTransform != null)
				{
					pageTransform.SetParent(this.transform);
					pageTransform.localPosition = Vector3.zero;
					pageTransform.localScale = Vector3.one;
				}

				return pageInstance;
			}

			return null;
		}

		/// <summary>
		/// ページを開く
		/// </summary>
		/// <returns></returns>
		public GameObject OpenPage()
		{
			this.gameObject.SetActive(true);
			this.rootObject.SetActive(true);
			return rootObject.transform.Find("Content")?.gameObject;
		}
	}
}
