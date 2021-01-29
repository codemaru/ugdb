using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;


/// <summary>
/// TextMeshPro のテキストのタップイベント
/// TextMesh Pro/Examples/Script/TMP_TextEventHandler.csをスマホ用に改造したもの
/// </summary>
public class TextMeshProEventHandler : MonoBehaviour
{
	[Serializable]
	public class CharacterSelectionEvent : UnityEvent<char, int> { }

	[Serializable]
	public class WordSelectionEvent : UnityEvent<string, int, int> { }

	[Serializable]
	public class LineSelectionEvent : UnityEvent<string, int, int> { }

	[Serializable]
	public class LinkSelectionEvent : UnityEvent<string, string, int> { }


	/// <summary>
	/// Event delegate triggered when pointer is over a character.
	/// </summary>
	[SerializeField]
	public CharacterSelectionEvent onCharacterSelection = new CharacterSelectionEvent();

	/// <summary>
	/// Event delegate triggered when pointer is over a word.
	/// </summary>
	[SerializeField]
	public WordSelectionEvent onWordSelection = new WordSelectionEvent();

	/// <summary>
	/// Event delegate triggered when pointer is over a line.
	/// </summary>
	[SerializeField]
	public LineSelectionEvent onLineSelection = new LineSelectionEvent();

	/// <summary>
	/// Event delegate triggered when pointer is over a link.
	/// </summary>
	[SerializeField]
	public LinkSelectionEvent onLinkSelection = new LinkSelectionEvent();

	/// <summary>
	/// 本体
	/// </summary>
	private TMP_Text textComponent;

	/// <summary>
	/// カメラ
	/// </summary>
	private Camera cachedCamera;

	/// <summary>
	/// 最後に選択したリンク
	/// </summary>
	private int lastLinkIndex = -1;

	/// <summary>
	/// 最後に選択した文字
	/// </summary>
	private int lastCharIndex = -1;

	/// <summary>
	/// 最後に選択した単語
	/// </summary>
	private int lastWordIndex = -1;

	/// <summary>
	/// 最後に選択した行
	/// </summary>
	private int lastLineIndex = -1;


	/// <summary>
	/// Override Unity Function
	/// </summary>
	void Awake()
	{
		textComponent = GetComponent<TMP_Text>();

		if (textComponent.GetType() == typeof(TextMeshProUGUI))
		{
			var canvas = GetComponentInParent<Canvas>();
			if (canvas != null)
			{
				if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
					cachedCamera = null;
				else
					cachedCamera = canvas.worldCamera;
			}
		}
		else
		{
			cachedCamera = Camera.main;
		}
	}

	/// <summary>
	/// Override Unity Function
	/// </summary>
	void LateUpdate()
	{
		// タッチ座標とマウス座標の両方で機能させる
		var touchPosition = Input.touchCount <= 0 ?
			Input.mousePosition : (Vector3)Input.GetTouch(0).position;

		var touchDown = Input.touchCount <= 0 ? Input.GetMouseButtonDown(0) : true;

		//	本体の矩形内をタップしたかどうか
		if (TMP_TextUtilities.IsIntersectingRectTransform(textComponent.rectTransform, touchPosition, cachedCamera))
		{
			//	文字のタップ検索
			int charIndex = TMP_TextUtilities.FindIntersectingCharacter(textComponent, touchPosition, cachedCamera, true);
			if (charIndex != -1 && charIndex != lastCharIndex)
			{
				lastCharIndex = charIndex;

				TMP_CharacterInfo info = textComponent.textInfo.characterInfo[charIndex];
				this.onCharacterSelection?.Invoke(info.character, charIndex);
			}

			//	単語のタップ検索
			int wordIndex = TMP_TextUtilities.FindIntersectingWord(textComponent, touchPosition, cachedCamera);
			if (wordIndex != -1 && wordIndex != lastWordIndex)
			{
				lastWordIndex = wordIndex;

				TMP_WordInfo info = textComponent.textInfo.wordInfo[wordIndex];
				this.onWordSelection?.Invoke(info.GetWord(), info.firstCharacterIndex, info.characterCount);
			}

			// 行のタップ検索
			int lineIndex = TMP_TextUtilities.FindIntersectingLine(textComponent, touchPosition, cachedCamera);
			if (lineIndex != -1 && lineIndex != lastLineIndex)
			{
				lastLineIndex = lineIndex;

				TMP_LineInfo lineInfo = textComponent.textInfo.lineInfo[lineIndex];

				// Send the event to any listeners.
				char[] buffer = new char[lineInfo.characterCount];
				for (int i = 0; i < lineInfo.characterCount && i < textComponent.textInfo.characterInfo.Length; i++)
				{
					buffer[i] = textComponent.textInfo.characterInfo[i + lineInfo.firstCharacterIndex].character;
				}

				string lineText = new string(buffer);
				this.onLineSelection?.Invoke(lineText, lineInfo.firstCharacterIndex, lineInfo.characterCount);
			}

			// リンクのタップ検索（入力があった時のみ）
			if (touchDown)
			{
				int linkIndex = TMP_TextUtilities.FindIntersectingLink(textComponent, touchPosition, cachedCamera);

				// 空振り や 別のリンク をタップした時は、選択解除を通知します。
				if ((linkIndex == -1 && lastLinkIndex != -1) || linkIndex != lastLinkIndex)
				{
					lastLinkIndex = -1;

					this.onLinkSelection?.Invoke(string.Empty, string.Empty, linkIndex);
				}

				// 新しいリンクをタップした時は、選択を通知します
				if (linkIndex != -1 && linkIndex != lastLinkIndex)
				{
					lastLinkIndex = linkIndex;

					TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[linkIndex];
					this.onLinkSelection?.Invoke(linkInfo.GetLinkID(), linkInfo.GetLinkText(), linkIndex);
				}
			}
		}
		else
		{
			// リンクの選択解除（範囲外をタップした時は、選択解除を通知します）
			if (touchDown)
			{
				if (lastLinkIndex != -1)
				{
					lastLinkIndex = -1;
					this.onLinkSelection?.Invoke(string.Empty, string.Empty, lastLinkIndex);
				}
			}
		}
	}
}
