using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace VioletIoc
{
    internal class LazyDelegate
    {
        private readonly Type _targetType;
        private readonly Container _locator;

        public LazyDelegate(Type targetType, Container locator)
        {
            _targetType = targetType.ThrowIfNull(nameof(targetType));
            _locator = locator.ThrowIfNull(nameof(locator));
        }

        private Type[] InvokerArgumentTypes => new[] { _targetType };

        private Type DelegateType => Expression.GetFuncType(InvokerArgumentTypes);

        public Delegate CreateDelegate()
        {
            var invokerMethod = typeof(LazyDelegate)
                .GetRuntimeMethods()
                .Single(m => m.Name == nameof(Invoke));

            return invokerMethod
                .MakeGenericMethod(InvokerArgumentTypes)
                .CreateDelegate(DelegateType, this);
        }

        public TReturn Invoke<TReturn>()
            where TReturn : class
        => _locator.Resolve<TReturn>();
    }
}