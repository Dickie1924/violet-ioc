using System;

namespace VioletIoc
{
    internal class Registration
    {
        private readonly bool _isSingleton;
        private readonly Type _concreteType;
        private object _instance;

        public Registration(object instance)
        {
            _instance = instance.ThrowIfNull(nameof(instance));
        }

        public Registration(Type concreteType, bool singleton)
        {
            _concreteType = concreteType.ThrowIfNull(nameof(concreteType));
            _isSingleton = singleton;
        }

        protected Registration()
        {
        }

        protected object Instance => _instance;

        public virtual object GetObject(ResolutionTracer tracer, ResolutionContext context, Container locator, params IParameterOverride[] overrides)
        {
            if (_instance != null)
            {
                tracer?.Add($"Resolved to cached instance {_instance}");
                return _instance;
            }
            else if (_concreteType != null)
            {
                context.ResolvedType = _concreteType;

                var obj = locator.CreateInstance(_concreteType, tracer, context, overrides);
                if (_isSingleton)
                {
                    tracer?.Add($"Caching {obj} as singleton");
                    _instance = obj;
                }

                return obj;
            }
            else
            {
                throw new Exception("Unable to get object, invalid registration.");
            }
        }

        protected void Resolve(object instance)
        {
            _instance = instance;
        }
    }
}