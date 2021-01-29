using UnityEngine;


namespace TMProSample
{
	/// <summary>
	/// 子階層にあるメッシュを1つにまとめる
	/// </summary>
	public class BatchingChildren : MonoBehaviour
	{
		/// <summary>
		/// Override Unity Function
		/// </summary>
		void Awake()
		{
		StaticBatchingUtility.Combine(this.gameObject);
		}
	}
}
