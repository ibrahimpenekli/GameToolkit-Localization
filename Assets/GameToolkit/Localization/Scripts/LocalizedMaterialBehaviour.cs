using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameToolkit.Localization
{
    public class LocalizedMaterialBehaviour : LocalizedAssetBehaviour
    {
		[SerializeField]
		private Material m_Material;

		[SerializeField, Tooltip("Texture property name.")]
		private string m_PropertyName = "_MainTex";

		[SerializeField]
        private LocalizedTexture m_LocalizedAsset;

		protected override bool IsAssetExist()
        {
            return m_LocalizedAsset;
        }

		protected override void UpdateComponentValue()
        {
			if (m_Material)
			{
				m_Material.SetTexture(m_PropertyName, m_LocalizedAsset.Value);
			}
        }
    }
}
