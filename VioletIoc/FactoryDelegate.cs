using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace VioletIoc
{
    internal class FactoryDelegate
    {
        private readonly Type _targetType;
        private readonly Type[] _inputTypes;
        private readonly Container _locator;

        public FactoryDelegate(Type targetType, Type[] inputTypes, Container locator)
        {
            _targetType = targetType.ThrowIfNull(nameof(targetType));
            _inputTypes = inputTypes.ThrowIfNull(nameof(inputTypes));
            _locator = locator.ThrowIfNull(nameof(locator));
        }

        private Type[] InvokerArgumentTypes => _inputTypes.Union(new[] { _targetType })
                                                         .ToArray();

        private Type DelegateType => Expression.GetFuncType(InvokerArgumentTypes);

        public Delegate CreateDelegate()
        {
            var invokerMethod = typeof(FactoryDelegate)
                .GetRuntimeMethods()
                .Single(m => m.Name == nameof(Invoke) && m.GetParameters().Count() == _inputTypes.Count());

            return invokerMethod
                .MakeGenericMethod(InvokerArgumentTypes)
                .CreateDelegate(DelegateType, this);
        }

        public TReturn Invoke<TReturn>()
            where TReturn : class
        => _locator.Resolve<TReturn>();

        public TReturn Invoke<T1, TReturn>(T1 arg1)
            where TReturn : class
        => _locator.Resolve<TReturn>(C(arg1));

        public TReturn Invoke<T1, T2, TReturn>(T1 arg1, T2 arg2)
            where TReturn : class
        => _locator.Resolve<TReturn>(C(arg1), C(arg2));

        public TReturn Invoke<T1, T2, T3, TReturn>(T1 arg1, T2 arg2, T3 arg3)
            where TReturn : class
        => _locator.Resolve<TReturn>(C(arg1), C(arg2), C(arg3));

        public TReturn Invoke<T1, T2, T3, T4, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TReturn : class
        => _locator.Resolve<TReturn>(C(arg1), C(arg2), C(arg3), C(arg4));

        public TReturn Invoke<T1, T2, T3, T4, T5, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TReturn : class
        => _locator.Resolve<TReturn>(C(arg1), C(arg2), C(arg3), C(arg4), C(arg5));

        public TReturn Invoke<T1, T2, T3, T4, T5, T6, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            where TReturn : class
        => _locator.Resolve<TReturn>(C(arg1), C(arg2), C(arg3), C(arg4), C(arg5), C(arg6));

        public TReturn Invoke<T1, T2, T3, T4, T5, T6, T7, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
            where TReturn : class
        => _locator.Resolve<TReturn>(C(arg1), C(arg2), C(arg3), C(arg4), C(arg5), C(arg6), C(arg7));

        public TReturn Invoke<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
            where TReturn : class
        => _locator.Resolve<TReturn>(C(arg1), C(arg2), C(arg3), C(arg4), C(arg5), C(arg6), C(arg7), C(arg8));

        private ParameterOverride<T> C<T>(T value) => new ParameterOverride<T>(value);
    }
}