using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class MonoBehaviourExtension
    {
        /// <summary>
        /// Получает компонент по имени объекта
        /// </summary>
        /// <typeparam name="T">тип компонента</typeparam>
        /// <param name="behaviour">исходный компонент</param>
        /// <param name="objectName">имя объекта</param>
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

        /// <summary>
        /// Получает компонент определенного типа. Логгирует неудачные попытки.
        /// </summary>
        /// <typeparam name="T">тип компонента</typeparam>
        /// <param name="behaviour">исходный компонент</param>
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
