

namespace TMProSample
{
	/// <summary>
	/// 各ページのリスト関連イベントインターフェース
	/// </summary>
	/// <typeparam name="TDataType"></typeparam>
	public interface IPageScrollList<TDataType>
	{
		/// <summary>
		/// リスト位置更新
		/// </summary>
		/// <param name="data">データ</param>
		/// <param name="index">データ群のインデックス</param>
		/// <param name="ratio">リスト表示位置レシオ</param>
		void OnUpdateList(TDataType data, int index, float ratio);

		/// <summary>
		/// リスト情報変更
		/// </summary>
		/// <param name="data">データ</param>
		/// <param name="index">データ群のインデックス</param>
		/// <param name="ratio">リスト表示位置レシオ</param>
		void OnChangeList(TDataType data, int index, float ratio);
	}
}
