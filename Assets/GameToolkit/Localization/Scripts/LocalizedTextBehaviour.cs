// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.UI;

namespace GameToolkit.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalizedTextBehaviour : LocalizedGenericAssetBehaviour<LocalizedText, string>
    {
        private void Reset()
        {
            if (!m_Component)
            {
                m_Component = GetComponent<Text>();
                if (m_Component)
                {
                    m_Property = "text";
                }
            }
        }
    }
}