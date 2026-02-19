using Contracts.Repositories.Loader;
using Utils.Reactive;

namespace Domain.Loader
{
    public class LoaderModel
    {
        public readonly ReactiveProperty<float> CurrentFuel;
        public readonly ReactiveProperty<bool> EngineActive;
        public float ForkSpeed => _loaderConfig.ForkLoaderSpeed;
        public readonly float MaxFuelValue;

        private float MoveFuelCost => _loaderConfig.MoveFuelCost;
        private readonly ILoaderConfig _loaderConfig;

        public LoaderModel(ILoaderConfig loaderConfig)
        {
            _loaderConfig = loaderConfig;

            MaxFuelValue = loaderConfig.MaxFuelValue;
            CurrentFuel = new ReactiveProperty<float>(loaderConfig.MaxFuelValue);
            EngineActive = new ReactiveProperty<bool>(false);
        }

        public float GetMoveSpeed()
        {
            if (CurrentFuel.Value <= MaxFuelValue / 2)
                return _loaderConfig.MoveLoaderSpeed / 2;

            return _loaderConfig.MoveLoaderSpeed;
        }
        
        public float GetRotateSpeed()
        {
            if (CurrentFuel.Value <= MaxFuelValue / 2)
                return _loaderConfig.RotationLoaderSpeed / 2;

            return _loaderConfig.RotationLoaderSpeed;
        }

        public bool UseFuel()
        {
            if (!EngineActive.Value)
                return false;
            
            if (CurrentFuel.Value > 0)
                CurrentFuel.SetValue(CurrentFuel.Value - MoveFuelCost);

            SetActiveEngineByFuel();

            return CurrentFuel.Value > 0;
        }

        public void SetActiveEngine() =>
            EngineActive.SetValue(!EngineActive.Value);

        public void ResetFuel() => 
            CurrentFuel.SetValue(MaxFuelValue);

        private void SetActiveEngineByFuel() =>
            EngineActive.SetValue(CurrentFuel.Value > 0);
    }
}