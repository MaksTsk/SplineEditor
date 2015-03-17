using System;
using System.IO;
using System.Xml.Serialization;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public static class SplineXmlSerializer
    {
        /// <summary>
        /// Сереализует сплайн в xml-файл
        /// </summary>
        /// <param name="spline">сплайн для серилазации</param>
        /// <param name="fileName">имя файла</param>
        public static void SerializeSpline(SerializableSpline spline, string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var sr = new XmlSerializer(typeof(SerializableSpline));
                    sr.Serialize(stream, spline);
                }
                catch (Exception ex)
                {
                    Debug.LogError(string.Concat("Ошибка при сериализации сплайна: ", ex.Message));
                }
            }
        }

        /// <summary>
        /// Десериализует сплайн из файла
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <returns>десериализованный сплайн</returns>
        public static SerializableSpline DeSerializeSpline(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (var filestream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                {
                    try
                    {
                        var sr = new XmlSerializer(typeof (SerializableSpline));

                        var serializableSpline = (SerializableSpline) sr.Deserialize(filestream);

                        return serializableSpline;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(string.Concat("Ошибка при десериализации плана: ", ex.Message));
                        return null;
                    }
                }
            }
            else
            {
                Debug.LogError("Не найден загружаемый сплайн");
                return null;
            }
        }
    }
}
