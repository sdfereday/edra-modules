namespace RedPanda.Entities
{
    public interface ICarryable
    {
        void SetInteractible(bool state);
        void SetCarryable(bool state);
        bool CanInteract { get; }
        bool CanThrow { get; }
        bool CanCarry { get; }
        int CurrentWeightValue { get; }
    }
}