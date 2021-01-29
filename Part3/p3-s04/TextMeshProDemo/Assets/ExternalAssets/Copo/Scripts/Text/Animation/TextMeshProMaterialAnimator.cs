using UnityEngine;
using System;
using System.Linq;
using TMPro;


namespace TMProSample
{
	/// <summary>
	///	「マテリアル」ページの例として表示するためのアニメーション
	/// </summary>
	public class TextMeshProMaterialAnimator : MonoBehaviour
	{
		/// <summary>
		/// TextMeshProのシェーダプロパティを格納するための属性
		/// </summary>
		public class TMProMaterialAttribute : Attribute
		{
			public int propertyID0 { get; private set; }
			public int propertyID1 { get; private set; }
			public TMProMaterialAttribute(string propertyName) { this.propertyID0 = Shader.PropertyToID(propertyName); }
			public TMProMaterialAttribute(string propertyName0, string propertyName1) { this.propertyID0 = Shader.PropertyToID(propertyName0); this.propertyID1 = Shader.PropertyToID(propertyName1); }
		}

		/// <summary>
		/// マテリアルのパラメータ
		/// </summary>
		public enum Type
		{
			[TMProMaterial("_FaceColor")]
			FACE_COLOR,
			[TMProMaterial("_ScaleRatioA", "_GradientScale")]
			FACE_SOFTNESS,
			[TMProMaterial("_FaceDilate")]
			FACE_DILATE,
			[TMProMaterial("_OutlineColor")]
			OUTLINE_COLOR,
			[TMProMaterial("_OutlineWidth")]
			OUTLINE_THICKNESS,
			[TMProMaterial("_UnderlayDummy")]
			UNDERLAY_TYPE,
			[TMProMaterial("_UnderlayColor")]
			UNDERLAY_COLOR,
			[TMProMaterial("_UnderlayOffsetX", "_UnderlayOffsetY")]
			UNDERLAY_OFFSET,
			[TMProMaterial("_UnderlayDilate", "_ScaleRatioC")]
			UNDERLAY_DILATE,
			[TMProMaterial("_UnderlaySoftness")]
			UNDERLAY_SOFTNESS,
			[TMProMaterial("_ShaderFlags")]
			BEVEL_TYPE,
			[TMProMaterial("_Bevel")]
			BEVEL_AMOUNT,
			[TMProMaterial("_BevelOffset", "_BevelWidth")]
			BEVEL_OFFSET,
			[TMProMaterial("_BevelWidth")]
			BEVEL_WIDTH,
			[TMProMaterial("_BevelRoundness", "_BevelOffset")]
			BEVEL_ROUNDNESS,
			[TMProMaterial("_BevelClamp")]
			BEVEL_CLMAP,
			[TMProMaterial("_LightAngle")]
			LIGHT_ANGLE,
			[TMProMaterial("_SpecularColor")]
			LIGHT_SPECULAR_COLOR,
			[TMProMaterial("_SpecularPower")]
			LIGHT_SPECULAR_POWER,
			[TMProMaterial("_Reflectivity")]
			LIGHT_REFLECTIVITY_POWER,
			[TMProMaterial("_Diffuse")]
			LIGHT_DIFFUSE_SHADOW,
			[TMProMaterial("_Ambient")]
			LIGHT_AMBIENT_SHADOW,
			[TMProMaterial("_BumpFace")]
			BUMPMAP_FACE,
			[TMProMaterial("_BumpOutline")]
			BUMPMAP_OUTLINE,
			[TMProMaterial("_ReflectFaceColor")]
			ENVIRONMENTMAP_FACE_COLOR,
			[TMProMaterial("_ReflectOutlineColor")]
			ENVIRONMENTMAP_OUTLINE_COLOR,
			[TMProMaterial("_EnvMatrix" /*"_EnvMatrixRotation"*/)]
			ENVIRONMENTMAP_ROTATION,
			[TMProMaterial("_GlowColor")]
			GLOW_COLOR,
			[TMProMaterial("_GlowOffset", "_ScaleRatioB")]
			GLOW_OFFSET,
			[TMProMaterial("_GlowInner")]
			GLOW_INNER,
			[TMProMaterial("_GlowOuter")]
			GLOW_OUTER,
			[TMProMaterial("_GlowPower")]
			GLOW_POWER,
			[TMProMaterial("_MaskSoftnessX", "_MaskSoftnessY")]
			DEBUG,
		}

		/// <summary>
		/// マテリアルのパラメータ
		/// </summary>
		[SerializeField]
		private Type type = Type.FACE_COLOR;

		/// <summary>
		/// アニメーション速度
		/// </summary>
		[SerializeField]
		private float speed = 1.0f;

		/// <summary>
		/// 反復する数値の範囲
		/// </summary>
		[SerializeField, Range(0.0f, 0.5f)]
		private float generalParam0 = 0.5f;

		/// <summary>
		/// マテリアルのパラメータID
		/// </summary>
		private int typePropertyID = 0;
		/// <summary>
		/// マテリアルのパラメータID(2つ目)
		/// </summary>
		private int typePropertyIDSub = 0;

		/// <summary>
		/// マテリアル
		/// </summary>
		private Material material = null;

		/// <summary>
		/// アニメーション時間
		/// </summary>
		private float time = 0.0f;


		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Start()
		{
			var text = GetComponent<TextMeshProUGUI>();
			if (text == null)
				return;

			this.material = text.fontMaterial;

			var typeField = this.type.GetType().GetField(this.type.ToString());

			this.typePropertyID = typeField
				.GetCustomAttributes(typeof(TMProMaterialAttribute), true)
				.Cast<TMProMaterialAttribute>()
				.First()
				.propertyID0;

			this.typePropertyIDSub = typeField
				.GetCustomAttributes(typeof(TMProMaterialAttribute), true)
				.Cast<TMProMaterialAttribute>()
				.First()
				.propertyID1;
		}

		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void OnDestroy()
		{
			if (this.material != null)
				DestroyImmediate(this.material);
		}

		/// <summary>
		/// Override Unity Function
		/// </summary>
		private void Update()
		{
			this.time += Time.deltaTime * this.speed;
			this.time = this.time - Mathf.Floor(this.time);

			float timePI = this.time * Mathf.PI * 2.0f;
			Color color = new Color(Cos01(timePI), Cos01(timePI + 2.09f), Cos01(timePI + 4.19f), 1.0f);

			switch (this.type)
			{
				case Type.FACE_COLOR:
					material.SetColor(this.typePropertyID, color);
					break;
				case Type.FACE_SOFTNESS:
					material.SetFloat(this.typePropertyID,    Mathf.Lerp(0.0f, 0.6f, Cos01(timePI)));
					material.SetFloat(this.typePropertyIDSub, Mathf.Lerp(0.0f, 6.0f, Cos01(timePI)));
					break;
				case Type.FACE_DILATE:
					material.SetFloat(this.typePropertyID, Mathf.Cos(timePI));
					break;
				case Type.OUTLINE_COLOR:
					material.SetColor(this.typePropertyID, color);
					break;
				case Type.OUTLINE_THICKNESS:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.UNDERLAY_COLOR:
					material.SetColor(this.typePropertyID, color);
					break;
				case Type.UNDERLAY_TYPE:
					material.DisableKeyword(this.time < 0.5f ? "UNDERLAY_ON" : "UNDERLAY_INNER");
					material.EnableKeyword(this.time < 0.5f ? "UNDERLAY_INNER" : "UNDERLAY_ON");
					break;
				case Type.UNDERLAY_OFFSET:
					material.SetFloat(this.typePropertyID,    Mathf.Sin(timePI));
					material.SetFloat(this.typePropertyIDSub, Mathf.Cos(timePI));
					break;

				case Type.UNDERLAY_DILATE:
					material.SetFloat(this.typePropertyID,    Mathf.Cos(timePI));
					material.SetFloat(this.typePropertyIDSub, Mathf.Lerp(0.382f, 0.191f, Cos01(timePI)));
					break;
				case Type.UNDERLAY_SOFTNESS:
					material.SetFloat(this.typePropertyID,    Cos01(timePI));
					material.SetFloat(this.typePropertyIDSub, Mathf.Lerp(0.191f, 0.382f, Cos01(timePI)));
					break;
				case Type.BEVEL_TYPE:
					material.SetFloat(this.typePropertyID, Mathf.Cos(timePI) * 0.1f + 1.0f);
					break;
				case Type.BEVEL_AMOUNT:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.BEVEL_OFFSET:
					material.SetFloat(this.typePropertyID, Mathf.Cos(timePI) * 0.5f);
					material.SetFloat(this.typePropertyIDSub, 0.0f);
					break;
				case Type.BEVEL_WIDTH:
					material.SetFloat(this.typePropertyID, Mathf.Cos(timePI) * 0.5f);
					break;
				case Type.BEVEL_ROUNDNESS:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					material.SetFloat(this.typePropertyIDSub, -0.072f);
					break;
				case Type.BEVEL_CLMAP:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.LIGHT_ANGLE:
					material.SetFloat(this.typePropertyID, timePI);
					break;
				case Type.LIGHT_SPECULAR_COLOR:
					material.SetColor(this.typePropertyID, color);
					break;
				case Type.LIGHT_SPECULAR_POWER:
					material.SetFloat(this.typePropertyID, Cos01(timePI) * 4.0f);
					break;
				case Type.LIGHT_REFLECTIVITY_POWER:
					material.SetFloat(this.typePropertyID, Mathf.Lerp(5.0f, 15.0f, Cos01(timePI)));
					break;
				case Type.LIGHT_DIFFUSE_SHADOW:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.LIGHT_AMBIENT_SHADOW:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.BUMPMAP_FACE:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.BUMPMAP_OUTLINE:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.ENVIRONMENTMAP_FACE_COLOR:
					material.SetColor(this.typePropertyID, color);
					break;
				case Type.ENVIRONMENTMAP_OUTLINE_COLOR:
					material.SetColor(this.typePropertyID, color);
					break;
				case Type.ENVIRONMENTMAP_ROTATION:
					material.SetMatrix(this.typePropertyID, Matrix4x4.Rotate(Quaternion.AngleAxis(time * 360.0f, Vector3.up)));
					break;
				case Type.GLOW_COLOR:
					material.SetColor(this.typePropertyID, color);
					break;
				case Type.GLOW_OFFSET:
					material.SetFloat(this.typePropertyID, Mathf.Cos(timePI));
					material.SetFloat(this.typePropertyIDSub, Mathf.Lerp(0.427f, 0.2135f, Cos01(timePI)));
					break;
				case Type.GLOW_INNER:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.GLOW_OUTER:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.GLOW_POWER:
					material.SetFloat(this.typePropertyID, Cos01(timePI));
					break;
				case Type.DEBUG:
					material.SetFloat(this.typePropertyID, Cos01(timePI) * 1000.0f);
					material.SetFloat(this.typePropertyIDSub, Cos01(timePI) * 1000.0f);
					break;
			}
		}

		private float Cos01(float value)
		{
			return Mathf.Cos(value) * generalParam0 + (1.0f - generalParam0);
		}
	}
}