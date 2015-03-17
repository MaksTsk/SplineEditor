namespace Assets.Scripts.Interfaces
{
    public interface ISelectableObject
    {
        /// <summary>
        /// Выделяет компонент
        /// </summary>
        void Select();

        /// <summary>
        /// Является ли компонент выделенным.
        /// </summary>
        bool IsSelected { get; }
    }
}
