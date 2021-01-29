// warning CS0649: Field 'PageScrollData<TDataType, TListType>.JsonData.data' is never assigned to, and will always have its default value null 
#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;


namespace TMProSample
{
	/// <summary>
	/// UnityEngine.UI.ScrollRect と連動したリスト制御システム
	/// </summary>
	/// <typeparam name="TDataType">データの型</typeparam>
	/// <typeparam name="TListType">リストの型</typeparam>
	public class PageScrollData<TDataType, TListType> : MonoBehaviour
		where TDataType : struct
		where TListType : IPageScrollList<TDataType>
	{
		/// <summary>
		/// JSONデータの受け皿（配列を受け取る為のもの）
		/// </summary>
		[System.Serializable]
		private struct JsonData
		{
			[SerializeField]
			public TDataType[] data;
		};

		/// <summary>
		/// UnityEngine.UI.ScrollRect
		/// </summary>
		[SerializeField]
		protected ScrollRect scrollRect = default;

		/// <summary>
		/// JSONデータ
		/// </summary>
		[SerializeField]
		private TextAsset json = default;

		/// <summary>
		/// リストのプレハブ
		/// </summary>
		[SerializeField]
		private GameObject listPrefab = default;

		/// <summary>
		/// リスト数（表示される数）
		/// </summary>
		[SerializeField]
		private int listCount = 2;

		/// <summary>
		/// リストの開始位置オフセット
		/// </summary>
		[SerializeField]
		private float scrollStartOffset = 0.0f;

		/// <summary>
		/// リストの終了位置オフセット
		/// </summary>
		[SerializeField]
		private float scrollEndOffset = 0.0f;

		/// <summary>
		/// 読み込んだデータ群
		/// </summary>
		protected TDataType[] data;

		/// <summary>
		/// 表示しているリスト群
		/// </summary>
		private TListType[] list;

		/// <summary>
		/// 表示しているリスト群に連動するデータのインデックス
		/// </summary>
		private int[] dataIndices;

		/// <summary>
		/// 読み込んだデータ数
		/// </summary>
		private int dataCount;

		/// <summary>
		/// 表示しているリストが周回する回数
		/// </summary>
		private float loopCount;

		/// <summary>
		/// リスト間のインターバル(0.0～1.0)
		/// </summary>
		private float listInterval;

		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Start()
		{
			this.data = JsonUtility.FromJson<JsonData>(json.text).data;

			this.dataCount = this.data.Length;
			this.loopCount = Mathf.Max(this.dataCount / (float)this.listCount, 1.0f);
			this.listInterval = this.loopCount / (float)this.dataCount;

			this.listCount = Mathf.Min(this.listCount, this.dataCount);
			this.listPrefab.SetActive(false);
			this.list = new TListType[this.dataCount];
			for (int i = 0; i < listCount; ++i)
			{
				var listObject = Instantiate(this.listPrefab, this.transform);
				listObject.SetActive(true);
				list[i] = listObject.GetComponent<TListType>();
			}

			this.dataIndices = new int[this.listCount];
			for (int i = 0; i < listCount; ++i)
				this.dataIndices[i] = -1;

			this.scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
			OnScrollValueChanged(new Vector2(this.scrollRect.horizontalNormalizedPosition, this.scrollRect.verticalNormalizedPosition));

			OnStart();
		}

		/// <summary>
		/// Callback On Start
		/// </summary>
		protected virtual void OnStart()
		{
		}

		/// <summary>
		/// スクロール位置が変わる度にコールされる
		/// </summary>
		/// <param name="value"></param>
		protected void OnScrollValueChanged(Vector2 value)
		{
			// スクロール位置(0.0～1.0) を オフセット(開始～1+終了)に補正
			value.y = 1.0f - value.y;
			value.y = Mathf.Lerp(this.scrollStartOffset / this.loopCount, 1.0f + (this.scrollEndOffset / this.loopCount), value.y);

			// リストの位置及びデータ更新
			for (int i = 0; i < this.listCount; ++i)
			{
				float progress = value.y * this.loopCount - this.listInterval * i;
				progress = Mathf.Max(progress, 0);

				int increaseIndex = Mathf.FloorToInt(progress) * this.listCount;
				int dataIndex = i + increaseIndex;
				float dataRatio = (progress % 1.0f);

				if (dataIndex >= this.dataCount)
				{
					dataIndex = this.dataCount - 1;
					dataRatio = 1.0f;
				}

				if (this.dataIndices[i] != dataIndex)
				{
					list[i].OnChangeList(data[dataIndex], dataIndex, dataRatio);
					this.dataIndices[i] = dataIndex;
				}

				list[i].OnUpdateList(data[dataIndex], dataIndex, dataRatio);
			}
		}
	}
}
