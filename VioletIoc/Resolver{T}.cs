namespace VioletIoc
{
    internal class Resolver<T> : IResolver<T>
        where T : class
    {
        private readonly IContainer _container;

        internal Resolver(IContainer container)
        {
            _container = container.ThrowIfNull(nameof(container));
        }

        public T Resolve()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<TParam>(TParam param)
        {
            return _container.Resolve<T>(new ParameterOverride<TParam>(param));
        }

        public T Resolve<TParam1, TParam2>(TParam1 param1, TParam2 param2)
        {
            return _container.Resolve<T>(
                new ParameterOverride<TParam1>(param1),
                new ParameterOverride<TParam2>(param2));
        }

        public T Resolve<TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            return _container.Resolve<T>(
                new ParameterOverride<TParam1>(param1),
                new ParameterOverride<TParam2>(param2),
                new ParameterOverride<TParam3>(param3));
        }

        public T Resolve<TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            return _container.Resolve<T>(
                new ParameterOverride<TParam1>(param1),
                new ParameterOverride<TParam2>(param2),
                new ParameterOverride<TParam3>(param3),
                new ParameterOverride<TParam4>(param4));
        }

        public T Resolve<TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            return _container.Resolve<T>(
                new ParameterOverride<TParam1>(param1),
                new ParameterOverride<TParam2>(param2),
                new ParameterOverride<TParam3>(param3),
                new ParameterOverride<TParam4>(param4),
                new ParameterOverride<TParam5>(param5));
        }
    }
}
