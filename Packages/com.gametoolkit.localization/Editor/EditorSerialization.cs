using System;
using System.Globalization;
using System.IO;
using GameToolkit.Localization.Editor.Serialization;
using UnityEditor;
using UnityEngine;

namespace GameToolkit.Localization.Editor
{
    public static class EditorSerialization
    {
        public static void Import()
        {
            var path = EditorUtility.OpenFilePanel("Import CSV file", "", "csv");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            
            try
            {
                using (var stream = File.OpenRead(path))
                {
                    var serialization = new CsvLocalizationSerialization();
                    serialization.Deserialize(stream);
                }

                Debug.Log("CSV file has been imported.");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void Export()
        {
            var fileName = Application.productName + "-"
                                                   + DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            var path = EditorUtility.SaveFilePanel("Export CSV file", "", fileName, "csv");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            try
            {
                using (var stream = File.OpenWrite(path))
                {
                    var serialization = new CsvLocalizationSerialization();
                    serialization.Serialize(stream);
                }

                Debug.Log("CSV file has been exported to " + path);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}