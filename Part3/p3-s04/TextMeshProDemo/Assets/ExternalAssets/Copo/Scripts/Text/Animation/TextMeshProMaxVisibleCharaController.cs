using UnityEngine;
using TMPro;


/// <summary>
/// MaxVisibleCharactersを設定
/// （プロパティであるため、Animator制御用に用意したコンポーネント）
/// </summary>
[ExecuteInEditMode]
public class TextMeshProMaxVisibleCharaController : MonoBehaviour
{
	/// <summary>
	/// 表示数
	/// </summary>
	public float textMaxVisibleCharacters;

	/// <summary>
	/// 本体
	/// </summary>
	private TextMeshPro text;


	/// <summary>
	/// Override Unity Function
	/// </summary>
	private void Update()
	{
		if (this.text == null)
			this.text = GetComponent<TextMeshPro>();

		int visibleCharacters = Mathf.FloorToInt(this.text.textInfo.characterCount * this.textMaxVisibleCharacters);
		if (this.text.maxVisibleCharacters != visibleCharacters)
			this.text.maxVisibleCharacters = visibleCharacters;
	}
}
