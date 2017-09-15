using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameToolkit.Localization
{
	[RequireComponent(typeof(Renderer))]
    public class LocalizedRendererBehaviour : LocalizedAssetBehaviour
    {
		[SerializeField]
        private int m_MaterialIndex = 0;

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
			GetComponent<Renderer>().materials[m_MaterialIndex].SetTexture(m_PropertyName, m_LocalizedAsset.Value);
        }

		private void OnValidate()
		{
			m_MaterialIndex = Mathf.Max(0, m_MaterialIndex);
		}
    }
}
