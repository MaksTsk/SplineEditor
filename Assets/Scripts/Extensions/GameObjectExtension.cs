using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class GameObjectExtension
    {
        public static T GetComponentByObjectName<T>(string objectName)
            where T : class
        {
            var obj = GameObject.Find(objectName);

            if (obj == null)
            {
                Debug.LogError(string.Concat("Не найден объект ", objectName));
                return null;
            }

            var component = obj.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError(string.Format("Не задан компонент искомый компонент у объекта {0}", objectName));
            }

            return component;
        }

    }
}
