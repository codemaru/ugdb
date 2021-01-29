// ===================================
//
// Copyright(c) 2020 Copocopo All rights reserved.
// https://github.com/coposuke/UnityAssetToPNG
//
// ===================================

using UnityEngine;
using UnityEngine.U2D;
using UnityEditor;
using System.IO;
using System.Linq;

/// <summary>
/// 使い方
/// ①SpriteAtlasを作成
/// ②SpriteAtlasを各所設定しPackする
/// ③ゲームを実行する
/// ④SpriteAtlasを右クリックし"Generate PNG"を選択する
/// ⑤洗濯したSpriteAtlasと同階層にPackされたPNGが出力される
/// </summary>
public class GeneratePNGFromSpriteAtlas
{
	[MenuItem("Assets/Generate PNG(from SpriteAtlas)", false)]
	static void Execute()
	{
		int instanceID = Selection.activeInstanceID;
		string path = AssetDatabase.GetAssetPath(instanceID);
		string outputPath = Directory.GetParent(path).FullName;

		var spriteAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(path);
		if (spriteAtlas == null)
			return;

		Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
		spriteAtlas.GetSprites(sprites);

		if (sprites.Length <= 0)
			return;

		var texture = sprites[0].texture;

		Debug.Log(
			"output texture!!\n"+
			"source: "+ texture + "\n"+
			"result: "+outputPath);

		outputPath = Path.Combine(outputPath, texture.name);
		outputPath = Path.ChangeExtension(outputPath, "png");
		File.WriteAllBytes(outputPath, texture.EncodeToPNG());
		AssetDatabase.Refresh();
	}
}

public class GeneratePNGFromTexture2D
{
	[MenuItem("Assets/Generate PNG(from Texture2D)", false)]
	static void Execute()
	{
		int instanceID = Selection.activeInstanceID;
		string path = AssetDatabase.GetAssetPath(instanceID);
		string outputPath = Directory.GetParent(path).FullName;

		var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

		if (texture == null)
			return;

		Debug.Log(
			"output texture!!\n" +
			"source: " + texture + "\n" +
			"result: " + outputPath);

		outputPath = Path.Combine(outputPath, texture.name);
		outputPath = Path.ChangeExtension(outputPath, "png");
		File.WriteAllBytes(outputPath, texture.EncodeToPNG());
		AssetDatabase.Refresh();
	}
}

public class GeneratePNGFromSprites
{
	[MenuItem("Assets/Generate PNG(from Sprites)", false)]
	static void Execute()
	{
		int instanceID = Selection.activeInstanceID;
		string path = AssetDatabase.GetAssetPath(instanceID);
		string outputPath = Directory.GetParent(path).FullName;

		var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
		if (sprites == null || sprites.Length <= 0)
			return;

		int widthPerOne = Mathf.CeilToInt(sprites.Max(s => s.rect.width));
		int heightPerOne = Mathf.CeilToInt(sprites.Max(s => s.rect.height));
		int row = 0;

		for (int i = 1; i < 30; i++)
		{
			if (sprites.Length < i * i)
			{
				row = i;
				break;
			}
		}

		var atlas = sprites[0].texture;
		var texture = new Texture2D(
			Mathf.CeilToInt(row * widthPerOne),
			Mathf.CeilToInt(row * heightPerOne),
			TextureFormat.RGBA32, false);

		{
			for (int x = 0; x < texture.width; ++x)
				for (int y = 0; y < texture.height; ++y)
					texture.SetPixel(x, y, Color.clear);
		}

		for (int rowY = 0; rowY < row; ++rowY)
		{
			for (int rowX = 0; rowX < row; ++rowX)
			{
				int i = rowY * row + rowX;

				if (sprites.Length <= i)
					break;

				var sprite = sprites[i];
				int spriteWidth = Mathf.FloorToInt(sprite.rect.width);
				int spriteHeight = Mathf.FloorToInt(sprite.rect.height);
				var atlasRect = sprite.rect;
				int startX = (widthPerOne - spriteWidth) / 2;
				int startY = (heightPerOne - spriteHeight) / 2;
				int textureStartX = rowX * widthPerOne;
				int textureStartY = texture.height - rowY * heightPerOne;

				for (int x = 0; x < spriteWidth; ++x)
				{
					for (int y = 0; y < spriteHeight; ++y)
					{
						var color = atlas.GetPixel(Mathf.FloorToInt(atlasRect.x + x), Mathf.FloorToInt(atlasRect.y + spriteHeight - y));
						texture.SetPixel(textureStartX + startX + x, textureStartY - startY - y, color);
					}
				}
			}
		}

		Debug.Log(
			"output texture!!\n" +
			"source: " + atlas + "\n" +
			"result: " + outputPath);

		outputPath = Path.Combine(outputPath, texture.name);
		outputPath = Path.ChangeExtension(outputPath, "png");
		File.WriteAllBytes(outputPath, texture.EncodeToPNG());
		AssetDatabase.Refresh();
	}
}
