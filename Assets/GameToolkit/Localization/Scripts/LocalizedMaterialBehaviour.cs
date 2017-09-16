using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameToolkit.Localization
{
    public class LocalizedMaterialBehaviour : LocalizedAssetBehaviour
    {
		public Material Material;
		public string PropertyName = "_MainTex";
        public LocalizedTexture LocalizedTexture;

		protected override void UpdateComponentValue()
        {
			if (Material && LocalizedTexture)
			{
				Material.SetTexture(PropertyName, LocalizedTexture.Value);
			}
        }
    }
}
