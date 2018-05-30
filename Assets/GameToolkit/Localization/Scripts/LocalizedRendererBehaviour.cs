﻿// Copyright (c) H. Ibrahim Penekli. All rights reserved.
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
                var materials = GetMaterials();
                if (MaterialIndex < materials.Length)
                {
                    materials[MaterialIndex].SetTexture(PropertyName, LocalizedTexture.Value);
                }
                else
                {
                    Debug.LogWarning(name + ": Material index out of bounds!");
                }
            }
        }

        private void OnValidate()
        {
            MaterialIndex = Mathf.Max(0, MaterialIndex);
        }

        private Material[] GetMaterials()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return GetComponent<Renderer>().sharedMaterials;
            }
            else
            {
#endif
                return GetComponent<Renderer>().materials;
#if UNITY_EDITOR
            }
#endif
        }
    }
}
