using System;

namespace Utils.Reactive
{
    public class ReactiveProperty<T>
    {
        public ReactiveProperty(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
        public event Action<T> OnValueChanged;

        public void SetValue(T neValue)
        {
            Value = neValue;
            OnValueChanged?.Invoke(Value);
        }
    }
}