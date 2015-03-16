namespace Assets.Scripts.Interfaces
{
    public interface ISelectableObject
    {
        void Select();

        bool IsSelected { get; }
    }
}
