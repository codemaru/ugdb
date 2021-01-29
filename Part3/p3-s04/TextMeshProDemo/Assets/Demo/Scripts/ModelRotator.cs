using UnityEngine;


namespace TMProSample
{
	/// <summary>
	/// モデルY軸回転
	/// </summary>
	public class ModelRotator : MonoBehaviour
	{
		/// <summary>
		///	回転速度
		/// </summary>
		[SerializeField]
		private float speed = 1.0f;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Update()
		{
			var angle = this.transform.localEulerAngles;
			angle.y += speed * Time.deltaTime;
			this.transform.localEulerAngles = angle;
		}
	}
}
