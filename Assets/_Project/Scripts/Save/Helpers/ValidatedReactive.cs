using System;
using R3;

public class ValidatedReactive<T> : ReactiveProperty<T>
{
    Func<T,T> ValidateFunc;

    public ValidatedReactive(T initialValue, Func<T,T> validateFunc) : base(initialValue)
    {
        ValidateFunc = validateFunc;
    }
    public override T Value { get => base.Value; set => base.Value = ValidateFunc.Invoke(value); }
}