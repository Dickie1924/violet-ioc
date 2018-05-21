using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VioletIoc
{
    internal class Container : IContainer
    {
        private readonly string _traceName;
        private readonly bool _traceEnabled;
        private readonly Container _parent;
        private readonly Dictionary<RegistrationKey, Registration> _registrations = new Dictionary<RegistrationKey, Registration>();
        private readonly HashSet<IDisposable> _disposables = new HashSet<IDisposable>();

        private bool _disposed;

        public Container()
            : this(null, null)
        {
        }

        public Container(Container parent)
            : this(parent, null)
        {
        }

        public Container(Container parent, string traceName)
        {
            _parent = parent;
            _traceName = traceName;
            _traceEnabled = traceName != null;

            // Resolving an IContainer creates a child of the current container.
            Register<IContainer>(c => c.CreateChildContainer());

            // Resolving a Container get's this (needed by Resolver<T>)
            RegisterSingleton<Container>(this);

            Register(typeof(IResolver<>), typeof(Resolver<>));
        }

        public string TraceName => _traceName;

        public object CreateInstance(Type type, params IParameterOverride[] overrides)
            => CreateInstance(type, null, null, overrides);

        public T CreateInstance<T>(params IParameterOverride[] overrides)
            where T : class
        {
            return CreateInstance(typeof(T), overrides) as T;
        }

        public T Resolve<T>(params IParameterOverride[] overrides)
            where T : class
            => Resolve(typeof(T), overrides) as T;

        public T Resolve<T>(string key, params IParameterOverride[] overrides)
            where T : class
            => Resolve(typeof(T), key, overrides) as T;

        public IResolver<T> ResolverFor<T>()
            where T : class
        {
            return new Resolver<T>(this);
        }

        public T ResolveOrDefault<T>(params IParameterOverride[] overrides)
            where T : class
            => ResolveOrDefault<T>(null, overrides);

        public T ResolveOrDefault<T>(string key, params IParameterOverride[] overrides)
            where T : class
        {
            object obj;
            if (TryResolve(typeof(T), key, out obj, overrides))
            {
                return obj as T;
            }
            else
            {
                return default(T);
            }
        }

        public bool TryResolve<T>(out T obj, params IParameterOverride[] overrides)
            where T : class
        => TryResolve<T>(null, out obj, overrides);

        public bool TryResolve<T>(string key, out T obj, params IParameterOverride[] overrides)
            where T : class
        {
            if (TryGet(typeof(T), key, out object o, null, this, null, overrides))
            {
                obj = (T)o;
                return true;
            }
            else
            {
                obj = default(T);
                return false;
            }
        }

        public bool TryResolve(Type type, out object obj, params IParameterOverride[] overrides)
            => TryGet(type, null, out obj, null, this, null, overrides);

        public bool TryResolve(Type type, string key, out object obj, params IParameterOverride[] overrides)
            => TryGet(type, key, out obj, null, this, null, overrides);

        public object Resolve(Type type, params IParameterOverride[] overrides)
            => Resolve(type, null, overrides);

        public object Resolve(Type type, string key, params IParameterOverride[] overrides)
        {
            object obj;
            if (TryResolve(type, key, out obj, overrides))
            {
                return obj;
            }
            else
            {
                throw new Exception($"Can not resolve type {type} with key {key ?? "NULL"}.");
            }
        }

        public IContainer GetParentContainer() => _parent;

        public IContainer GetRootContainer()
        {
            var s = this as IContainer;
            while (s.GetParentContainer() != null)
            {
                s = s.GetParentContainer();
            }

            return s;
        }

        public bool CanResolve<T>()
            => CanResolve(typeof(T), null);

        public bool CanResolve<T>(string key)
            => CanResolve(typeof(T), key);

        public bool CanResolve(Type type)
            => CanResolve(type, null);

        public bool CanResolve(Type type, string key)
        {
            type.ThrowIfNull(nameof(type));

            return CanResolveLocally(type, key)
                || (_parent != null && _parent.CanResolve(type, key));
        }

        public bool CanResolveLocally(Type type)
            => CanResolveLocally(type, null);

        public bool CanResolveLocally(Type type, string key)
        {
            type.ThrowIfNull(nameof(type));
            ThrowIfDisposed();

            var k = new RegistrationKey(type, key);

            return _registrations.ContainsKey(k);
        }

        public IContainer Register<TInterface, TType>()
            where TInterface : class
            where TType : class, TInterface
            => Register<TInterface, TType>((string)null);

        public IContainer Register<TInterface, TType>(string key)
            where TInterface : class
            where TType : class, TInterface
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TInterface), key);

            _registrations[k] = new Registration(typeof(TType), false);

            return this;
        }

        public IContainer Register<TInterface>(Func<IContainer, TInterface> factory)
            where TInterface : class
            => Register<TInterface>(factory, (string)null);

        public IContainer Register<TInterface>(Func<IContainer, TInterface> factory, string key)
            where TInterface : class
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TInterface), key);

            _registrations[k] = FactoryRegistration<TInterface>.Create(factory, false);

            return this;
        }

        public IContainer Register<TInterface>(Func<IContainer, ResolutionContext, TInterface> factory)
            where TInterface : class
            => Register<TInterface>(factory, (string)null);

        public IContainer Register<TInterface>(Func<IContainer, ResolutionContext, TInterface> factory, string key)
            where TInterface : class
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TInterface), key);

            _registrations[k] = FactoryRegistration<TInterface>.Create(factory, false);

            return this;
        }

        public IContainer Register(Type interfaceType, Type asType)
            => Register(interfaceType, asType, (string)null);

        public IContainer Register(Type interfaceType, Type asType, string key)
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(interfaceType, key);

            _registrations[k] = new Registration(asType, false);

            return this;
        }

        public IContainer RegisterSingleton<TType>(TType instance)
            where TType : class
            => RegisterSingleton<TType>(instance, (string)null);

        public IContainer RegisterSingleton<TType>(TType instance, string key)
            where TType : class
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TType), key);

            _registrations[k] = new Registration(instance);

            return this;
        }

        public IContainer RegisterSingleton<TType>()
            where TType : class
            => RegisterSingleton<TType, TType>((string)null);

        public IContainer RegisterSingleton<TInterface, TType>()
            where TType : class, TInterface
        => RegisterSingleton<TInterface, TType>((string)null);

        public IContainer RegisterSingleton<TInterface, TType>(string key)
            where TType : class, TInterface
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TInterface), key);

            _registrations[k] = new Registration(typeof(TType), true);

            return this;
        }

        public IContainer RegisterSingleton<TInterface, TType>(TType instance)
            where TType : class, TInterface
            => RegisterSingleton<TInterface, TType>(instance, (string)null);

        public IContainer RegisterSingleton<TInterface, TType>(TType instance, string key)
            where TType : class, TInterface
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TInterface), key);

            _registrations[k] = new Registration(instance);

            return this;
        }

        public IContainer RegisterSingleton<TType>(Func<IContainer, TType> factory)
            where TType : class
            => RegisterSingleton<TType>(factory, (string)null);

        public IContainer RegisterSingleton<TType>(Func<IContainer, TType> factory, string key)
            where TType : class
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TType), key);

            _registrations[k] = FactoryRegistration<TType>.Create(factory, true);

            return this;
        }

        public IContainer RegisterSingleton<TType>(Func<IContainer, ResolutionContext, TType> factory)
            where TType : class
            => RegisterSingleton<TType>(factory, (string)null);

        public IContainer RegisterSingleton<TType>(Func<IContainer, ResolutionContext, TType> factory, string key)
            where TType : class
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TType), key);

            _registrations[k] = FactoryRegistration<TType>.Create(factory, true);

            return this;
        }

        public IContainer RegisterSingleton<TInterface, TType>(Func<IContainer, TType> factory)
            where TType : class, TInterface
            => RegisterSingleton<TInterface, TType>(factory, (string)null);

        public IContainer RegisterSingleton<TInterface, TType>(Func<IContainer, TType> factory, string key)
            where TType : class, TInterface
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TInterface), key);

            _registrations[k] = FactoryRegistration<TType>.Create(factory, true);

            return this;
        }

        public IContainer RegisterSingleton<TInterface, TType>(Func<IContainer, ResolutionContext, TType> factory)
            where TType : class, TInterface
            => RegisterSingleton<TInterface, TType>(factory, (string)null);

        public IContainer RegisterSingleton<TInterface, TType>(Func<IContainer, ResolutionContext, TType> factory, string key)
            where TType : class, TInterface
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(typeof(TInterface), key);

            _registrations[k] = FactoryRegistration<TType>.Create(factory, true);

            return this;
        }

        public IContainer RegisterSingleton(Type asType)
            => RegisterSingleton(asType, (string)null);

        public IContainer RegisterSingleton(Type asType, string key)
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(asType, key);

            _registrations[k] = new Registration(asType, true);

            return this;
        }

        public IContainer RegisterSingleton(Type interfaceType, Type asType)
            => RegisterSingleton(interfaceType, asType, (string)null);

        public IContainer RegisterSingleton(Type interfaceType, Type asType, string key)
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(interfaceType, key);

            _registrations[k] = new Registration(asType, true);

            return this;
        }

        public IContainer RegisterSingleton(Type interfaceType, object instance)
            => RegisterSingleton(interfaceType, instance, (string)null);

        public IContainer RegisterSingleton(Type interfaceType, object instance, string key)
        {
            ThrowIfDisposed();
            var k = new RegistrationKey(interfaceType, key);

            _registrations[k] = new Registration(instance);

            return this;
        }

        public IContainer CreateChildContainer()
        {
            return new Container(this, _traceName);
        }

        public IContainer CreateChildContainer(string appendTraceName)
        {
            string traceName = default;

            if (appendTraceName != null)
            {
                traceName = $"{_traceName ?? string.Empty}{appendTraceName}";
            }
            else
            {
                traceName = _traceName;
            }

            return new Container(this, traceName);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        internal object CreateInstance(Type type, ResolutionTracer tracer, ResolutionContext context, params IParameterOverride[] overrides)
        {
            ThrowIfDisposed();
            tracer?.Add($"Creating instance of {type}...");

            if (context == null)
            {
                context = new ResolutionContext(type);
            }

            context.ResolvedType = type;

            if (type.GetTypeInfo().IsSubclassOf(typeof(Delegate)))
            {
                tracer?.Add("Creating factory...");

                var invoker = type.GetRuntimeMethods()
                                  .Single(m => m.Name == "Invoke");

                var returnType = invoker.ReturnParameter.ParameterType;

                var inputTypes = invoker.GetParameters()
                                        .Select(p => p.ParameterType)
                                        .ToArray();

                // construct delegate
                var d = new FactoryDelegate(returnType, inputTypes, this);
                return d.CreateDelegate();
            }

            ConstructorInfo ctor = null;
            var ctors = type.GetTypeInfo().GetConstructors();
            if (ctors.Count() > 1)
            {
                ctor = ctors.FirstOrDefault(ci => ci.GetCustomAttribute<IocConstructorAttribute>() != null);
                if (ctor == null)
                {
                    ctor = ctors.OrderBy(ci => ci.GetParameters().Count())
                                .First();
                }
            }
            else
            {
                ctor = ctors.SingleOrDefault();
            }

            if (ctor == null)
            {
                tracer?.Add($"No suitable constructor found for {type}");
                return null;
            }

            var ctorParams = ctor.GetParameters()
                .Select(p =>
                {
                    tracer?.Add($"Resolving {type} constructor parameter of type {p.ParameterType}");
                    object obj = overrides.FirstOrDefault(o => o.CanOverride(p))?.ValueForParameter(p, this);

                    if (obj != null)
                    {
                        return obj;
                    }
                    else if (TryGet(p.ParameterType, null, out obj, tracer, this, context, new IParameterOverride[] { }))
                    {
                        return obj;
                    }
                    else
                    {
                        return null;
                    }
                }).ToArray();

            object instance = null;

            try
            {
                instance = Activator.CreateInstance(type, ctorParams);
                if (instance is IDisposable disposable)
                {
                    _disposables.Add(disposable);
                }
            }
            catch (Exception ex)
            {
                throw new ContainerResolutionException(type, ex);
            }

            return instance;
        }

        internal bool TryGet(Type type, string key, out object obj, ResolutionTracer tracer, Container locator, ResolutionContext context, params IParameterOverride[] overrides)
        {
            ThrowIfDisposed();
            type.ThrowIfNull(nameof(type));
            locator.ThrowIfNull(nameof(locator));

            bool traceHead = false;
            obj = null;

            if (tracer == null && _traceEnabled)
            {
                tracer = new ResolutionTracer(null, TraceName);
                traceHead = true;
                tracer?.Add($"Resolving type {type}...");
            }

            if (context == null)
            {
                // first level in resolution
                context = new ResolutionContext(type);
            }
            else
            {
                context = new ResolutionContext(type, context);
            }

            var k = new RegistrationKey(type, key);

            if (_registrations.ContainsKey(k))
            {
                tracer?.Add($"Using local registration...");
                obj = _registrations[k].GetObject(tracer, context, locator, overrides);
            }
            else if (k.TryMakeOpenGeneric(out var openGenericKey) && _registrations.ContainsKey(openGenericKey))
            {
                var openReg = _registrations[openGenericKey];
                var closedReg = openReg.MakeClosedGeneric(k.Type.GenericTypeArguments);

                _registrations.Add(k, closedReg);

                obj = _registrations[k].GetObject(tracer, context, locator, overrides);
            }
            else if (_parent != null && _parent.TryGet(type, key, out object parentObj, tracer?.CreateChild(_parent.TraceName), locator, context, overrides))
            {
                tracer?.Add($"Parent resolved.");
                obj = parentObj;
            }
            else if (locator == this)
            {
                tracer?.Add($"Creating...");
                obj = CreateInstance(type, tracer, context, overrides);
            }
            else
            {
                tracer?.Add($"Unable to resolve...");
                obj = null;
            }

            if (traceHead)
            {
                Console.WriteLine(tracer);
            }

            if (obj == null)
            {
                return false;
            }

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    foreach (var disposable in _disposables)
                    {
                        disposable.Dispose();
                    }

                    _disposables.Clear();
                    _registrations.Clear();
                }

                _disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ContainerException("Attempt to use a container that has been disposed.");
            }
        }
    }
}