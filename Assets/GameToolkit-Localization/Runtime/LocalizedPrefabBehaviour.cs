// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace GameToolkit.Localization
{
    [AddComponentMenu(ComponentMenuRoot + "Localized Prefab")]
    public class LocalizedPrefabBehaviour : LocalizedAssetBehaviour
    {
        public LocalizedPrefab LocalizedPrefab;

        private GameObject m_PrefabInstance;

        protected override void UpdateComponentValue()
        {
#if UNITY_EDITOR
            // Disable on editor when not playing.
            if (Application.isPlaying)
            {
#endif
                if (LocalizedPrefab)
                {
                    if (m_PrefabInstance)
                    {
                        Destroy(m_PrefabInstance);
                    }

                    m_PrefabInstance = Instantiate(LocalizedPrefab.Value);
                    m_PrefabInstance.transform.SetParent(transform, false);
                }
#if UNITY_EDITOR
            }
#endif
        }
    }
}
