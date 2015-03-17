using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class GameObjectExtension
    {
        public static T GetComponentByObjectName<T>(this MonoBehaviour behaviour, string objectName)
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

        public static T GetComponentEx<T>(this MonoBehaviour behaviour)
            where T : class
        {
            var component = behaviour.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError(string.Format("Не найден компонент {0}", typeof(T)));
            }

            return component;
        }
    }
}
