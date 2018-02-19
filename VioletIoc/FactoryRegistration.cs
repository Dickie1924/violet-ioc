using System;

namespace VioletIoc
{
    internal abstract class FactoryRegistration<T> : Registration
        where T : class
    {
        private readonly bool _isSingletonFactory;

        protected FactoryRegistration(bool singletonFactory)
        {
            _isSingletonFactory = singletonFactory;
        }

        public static FactoryRegistration<T> Create(Func<IContainer, ResolutionContext, T> factory, bool singletonFactory)
        {
            return new WithContext(factory, singletonFactory);
        }

        public static FactoryRegistration<T> Create(Func<IContainer, T> factory, bool singletonFactory)
        {
            return new WithoutContext(factory, singletonFactory);
        }

        public override object GetObject(ResolutionTracer tracer, ResolutionContext context, Container locator, params IParameterOverride[] overrides)
        {
            if (Instance == null)
            {
                var obj = CreateObject(context, locator);
                tracer?.Add($"Factory created {obj}");
                if (_isSingletonFactory)
                {
                    tracer?.Add($"Caching result {obj} as singleton");
                    Resolve(obj);
                }

                return obj;
            }
            else
            {
                return base.GetObject(tracer, context, locator);
            }
        }

        protected abstract T CreateObject(ResolutionContext context, Container locator);

        private class WithContext : FactoryRegistration<T>
        {
            private readonly Func<IContainer, ResolutionContext, T> _factory;

            public WithContext(Func<IContainer, ResolutionContext, T> factory, bool singletonFactory)
                : base(singletonFactory)
            {
                _factory = factory.ThrowIfNull(nameof(factory));
            }

            protected override T CreateObject(ResolutionContext context, Container locator)
            {
                return _factory(locator, context);
            }
        }

        private class WithoutContext : FactoryRegistration<T>
        {
            private readonly Func<IContainer, T> _factory;

            public WithoutContext(Func<IContainer, T> factory, bool singletonFactory)
                : base(singletonFactory)
            {
                _factory = factory.ThrowIfNull(nameof(factory));
            }

            protected override T CreateObject(ResolutionContext context, Container locator)
            {
                return _factory(locator);
            }
        }
    }
}
