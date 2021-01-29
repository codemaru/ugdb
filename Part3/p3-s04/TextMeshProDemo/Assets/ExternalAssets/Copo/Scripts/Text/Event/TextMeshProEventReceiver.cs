using UnityEngine;


/// <summary>
/// TextEventHandlerの使い方サンプル
/// </summary>
[RequireComponent(typeof(TextMeshProEventHandler))]
public class TextMeshProEventReceiver : MonoBehaviour
{
	/// <summary>
	/// イベントハンドラー
	/// </summary>
	private TextMeshProEventHandler textEventHandler;


	/// <summary>
	/// Override Unity Function
	/// </summary>
	private void Awake()
	{
		textEventHandler = GetComponent<TextMeshProEventHandler>();
	}

	/// <summary>
	/// Override Unity Function
	/// </summary>
	void OnEnable()
	{
		if (textEventHandler != null)
		{
			textEventHandler.onCharacterSelection.AddListener(OnCharacterSelection);
			textEventHandler.onWordSelection.AddListener(OnWordSelection);
			textEventHandler.onLineSelection.AddListener(OnLineSelection);
			textEventHandler.onLinkSelection.AddListener(OnLinkSelection);
		}
	}

	/// <summary>
	/// Override Unity Function
	/// </summary>
	void OnDisable()
	{
		if (textEventHandler != null)
		{
			textEventHandler.onCharacterSelection.RemoveListener(OnCharacterSelection);
			textEventHandler.onWordSelection.RemoveListener(OnWordSelection);
			textEventHandler.onLineSelection.RemoveListener(OnLineSelection);
			textEventHandler.onLinkSelection.RemoveListener(OnLinkSelection);
		}
	}

	/// <summary>
	/// 文字タップ時
	/// </summary>
	/// <param name="c"></param>
	/// <param name="index"></param>
	void OnCharacterSelection(char c, int index)
	{
		Debug.Log("Character [" + c + "] at Index: " + index + " has been selected.");
	}

	/// <summary>
	/// 単語タップ時
	/// </summary>
	/// <param name="word"></param>
	/// <param name="firstCharacterIndex"></param>
	/// <param name="length"></param>
	void OnWordSelection(string word, int firstCharacterIndex, int length)
	{
		Debug.Log("Word [" + word + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
	}

	/// <summary>
	/// 行タップ時
	/// </summary>
	/// <param name="lineText"></param>
	/// <param name="firstCharacterIndex"></param>
	/// <param name="length"></param>
	void OnLineSelection(string lineText, int firstCharacterIndex, int length)
	{
		Debug.Log("Line [" + lineText + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
	}

	/// <summary>
	///	リンクタップ時
	/// </summary>
	/// <param name="linkID"></param>
	/// <param name="linkText"></param>
	/// <param name="linkIndex"></param>
	void OnLinkSelection(string linkID, string linkText, int linkIndex)
	{
		Debug.Log("Link Index: " + linkIndex + " with ID [" + linkID + "] and Text \"" + linkText + "\" has been selected.");
	}

}
