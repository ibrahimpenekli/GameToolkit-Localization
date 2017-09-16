// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameToolkit.Localization
{
	[RequireComponent(typeof(Renderer))]
    public class LocalizedRendererBehaviour : LocalizedAssetBehaviour
    {
        public int MaterialIndex = 0;
        public string PropertyName = "_MainTex";
        public LocalizedTexture LocalizedTexture;

        protected override void UpdateComponentValue()
        {
            if (LocalizedTexture)
            {
                GetComponent<Renderer>().materials[MaterialIndex].SetTexture(PropertyName, LocalizedTexture.Value);
            }
        }

		private void OnValidate()
		{
			MaterialIndex = Mathf.Max(0, MaterialIndex);
		}
    }
}
