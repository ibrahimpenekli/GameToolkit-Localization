// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.IO;

namespace GameToolkit.Localization.Editor.Serialization
{
    public interface ILocalizationSerialization
    {
        void Serialize(Stream stream);
        void Deserialize(Stream stream);
    }
}