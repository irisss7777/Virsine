namespace Contracts.Signals.Input
{
    public struct ForkInputSignal
    {
        public ForkInputSignal(float forkDirection)
        {
            ForkDirection = forkDirection;
        }

        public float ForkDirection { get; private set;}
    }
}