using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace VioletIoc
{
    internal class Container : IContainer
    {
        private static ConditionalWeakTable<object, object> _ids = new ConditionalWeakTable<object, object>();
        private static long currentId;

        private readonly string _traceName;
        private readonly bool _traceEnabled;
        private readonly Action<string> _logger;
        private readonly Container _parent;
        private readonly Dictionary<RegistrationKey, Registration> _registrations = new Dictionary<RegistrationKey, Registration>();
        private readonly HashSet<IDisposable> _disposables = new HashSet<IDisposable>();

        private bool _disposed;

        public Container()
            : this(null, null, null)
        {
        }

        public Container(Container parent)
            : this(parent, null, null)
        {
        }

        public Container(Container parent, string traceName)
            : this(parent, traceName, s => Console.WriteLine(s))
        {
        }

        public Container(Container parent, string traceName, Action<string> logger)
        {
            _parent = parent;
            _logger = logger;

            if (traceName != null)
            {
                _traceEnabled = true;
                _traceName = $"{traceName}[{DebugObjectId()}]";
            }

            _logger?.Invoke($"{_traceName} : Created container.");

            // Resolving a Container get's this (needed by Resolver<T>)
            RegisterSingleton(typeof(Container), this);
            Register(typeof(IResolver<>), typeof(Resolver<>), false);
        }

        public string TraceName => _traceName;

        public object Resolve(Type type, params IParameterOverride[] overrides)
        {
            object obj;
            if (TryResolve(type, out obj, overrides))
            {
                return obj;
            }
            else
            {
                throw new Exception($"Can not resolve type {type}.");
            }
        }

        public T Resolve<T>(params IParameterOverride[] overrides)
            where T : class
            => Resolve(typeof(T), overrides) as T;

        public IResolver<T> ResolverFor<T>()
            where T : class
        {
            return new Resolver<T>(this);
        }

        public T ResolveOrDefault<T>(params IParameterOverride[] overrides)
            where T : class
        {
            object obj;
            if (TryResolve(typeof(T), out obj, overrides))
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
        {
            if (TryResolve(typeof(T), out object o, null, this, null, overrides))
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
            => TryResolve(type, out obj, null, this, null, overrides);

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
            => CanResolve(typeof(T));

        public bool CanResolve(Type type)
        {
            type.ThrowIfNull(nameof(type));

            return CanResolveLocally(type)
                || (_parent != null && _parent.CanResolve(type));
        }

        public bool CanResolveLocally(Type type)
        {
            type.ThrowIfNull(nameof(type));
            ThrowIfDisposed();

            var k = new RegistrationKey(type);

            return _registrations.ContainsKey(k);
        }

        public IContainer RegisterSingleton(Type interfaceType, object instance)
        {
            ThrowIfDisposed();

            _logger?.Invoke($"{_traceName} : Created container.");

            var k = new RegistrationKey(interfaceType);
            _registrations[k] = new Registration(instance);

            return this;
        }

        public IContainer Register(Type interfaceType, Type asType, bool singleton)
        {
            ThrowIfDisposed();

            var k = new RegistrationKey(interfaceType);
            _registrations[k] = new Registration(asType, singleton);

            return this;
        }

        public IContainer Register<TType>(Type interfaceType, Func<IContainer, TType> factory, bool singleton)
            where TType : class
        {
            ThrowIfDisposed();

            var k = new RegistrationKey(interfaceType);
            _registrations[k] = FactoryRegistration<TType>.Create(factory, singleton);

            return this;
        }

        public IContainer Register<TType>(Type interfaceType, Func<IContainer, ResolutionContext, TType> factory, bool singleton)
            where TType : class
        {
            ThrowIfDisposed();

            var k = new RegistrationKey(interfaceType);
            _registrations[k] = FactoryRegistration<TType>.Create(factory, singleton);

            return this;
        }

        public IContainer Register<TType>()
            where TType : class
            => Register(typeof(TType), typeof(TType), false);

        public IContainer Register<TInterface, TType>()
            where TInterface : class
            where TType : class, TInterface
            => Register(typeof(TInterface), typeof(TType), false);

        public IContainer Register<TInterface>(Func<IContainer, TInterface> factory)
            where TInterface : class
            => Register<TInterface>(typeof(TInterface), factory, false);

        public IContainer Register<TInterface>(Func<IContainer, ResolutionContext, TInterface> factory)
            where TInterface : class
            => Register<TInterface>(typeof(TInterface), factory, false);

        public IContainer Register<TInterface>(Func<IContainer, object> factory)
            where TInterface : class
            => Register<object>(typeof(TInterface), factory, false);

        public IContainer Register<TInterface>(Func<IContainer, ResolutionContext, object> factory)
            where TInterface : class
            => Register<object>(typeof(TInterface), factory, false);

        public IContainer Register(Type interfaceType, Func<IContainer, object> factory)
            => Register<object>(interfaceType, factory, false);

        public IContainer Register(Type interfaceType, Func<IContainer, ResolutionContext, object> factory)
            => Register<object>(interfaceType, factory, false);

        public IContainer Register<TInterface, TType>(Func<IContainer, TType> factory)
            where TType : class, TInterface
            => Register<TType>(typeof(TInterface), factory, false);

        public IContainer Register<TInterface, TType>(Func<IContainer, ResolutionContext, TType> factory)
            where TType : class, TInterface
            => Register<TType>(typeof(TInterface), factory, false);

        public IContainer Register(Type asType)
            => Register(asType, asType, false);

        public IContainer Register(Type interfaceType, Type asType)
            => Register(interfaceType, asType, false);

        public IContainer RegisterSingleton<TType>(TType instance)
            where TType : class
            => RegisterSingleton(typeof(TType), instance);

        public IContainer RegisterSingleton<TType>()
            where TType : class
            => Register(typeof(TType), typeof(TType), true);

        public IContainer RegisterSingleton<TInterface, TType>()
            where TType : class, TInterface
            => Register(typeof(TInterface), typeof(TType), true);

        public IContainer RegisterSingleton<TInterface, TType>(TType instance)
            where TType : class, TInterface
            => RegisterSingleton(typeof(TInterface), instance);

        public IContainer RegisterSingleton<TType>(Func<IContainer, TType> factory)
            where TType : class
            => Register<TType>(typeof(TType), factory, true);

        public IContainer RegisterSingleton<TType>(Func<IContainer, ResolutionContext, TType> factory)
            where TType : class
            => Register<TType>(typeof(TType), factory, true);

        public IContainer RegisterSingleton<TInterface, TType>(Func<IContainer, TType> factory)
            where TType : class, TInterface
            => Register<TType>(typeof(TInterface), factory, true);

        public IContainer RegisterSingleton<TInterface, TType>(Func<IContainer, ResolutionContext, TType> factory)
            where TType : class, TInterface
            => Register<TType>(typeof(TInterface), factory, true);

        public IContainer RegisterSingleton(Type asType)
            => Register(asType, asType, true);

        public IContainer RegisterSingleton(Type interfaceType, Type asType)
            => Register(interfaceType, asType, true);

        public IContainer RegisterSingleton(Type interfaceType, Func<IContainer, object> factory)
            => Register<object>(interfaceType, factory, true);

        public IContainer RegisterSingleton(Type interfaceType, Func<IContainer, ResolutionContext, object> factory)
            => Register<object>(interfaceType, factory, true);

        public IContainer RegisterSingleton<TInterface>(Func<IContainer, object> factory)
            => Register<object>(typeof(TInterface), factory, true);

        public IContainer RegisterSingleton<TInterface>(Func<IContainer, ResolutionContext, object> factory)
            => Register<object>(typeof(TInterface), factory, true);

        public IContainer CreateChildContainer()
        {
            return new Container(this, _traceName, _logger);
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

            return new Container(this, traceName, _logger);
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
                return CreateFactory(type, tracer);
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Lazy<>))
            {
                return CreateLazy(type, tracer);
            }

            var ctor = GetConstructor(type, tracer);

            if (ctor == null)
            {
                return null;
            }

            var ctorParams = GetConstructorParams(ctor, tracer, context, overrides);

            object instance = null;

            try
            {
                tracer?.Add($"Creating with arguments [{string.Join(",", ctorParams.Select(p => p?.ToString() ?? "null"))}].");

                instance = Activator.CreateInstance(type, ctorParams);
            }
            catch (Exception ex)
            {
                throw new ContainerResolutionException(type, ex);
            }

            if (instance is IDisposable disposable)
            {
                tracer?.Add($"Instance marked for later disposal.");
                _disposables.Add(disposable);
            }

            return instance;
        }

        internal object CreateFactory(Type type, ResolutionTracer tracer)
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

        internal object CreateLazy(Type type, ResolutionTracer tracer)
        {
            tracer?.Add("Creating lazy...");

            var returnType = type.GenericTypeArguments[0];

            var d = new LazyDelegate(returnType, this);

            var lazyConstructor = type
                .GetConstructors()
                .Single(ci => ci.GetParameters().Count() == 1 && ci.GetParameters()[0].ParameterType.IsSubclassOf(typeof(Delegate)));

            return lazyConstructor.Invoke(new object[] { d.CreateDelegate() });
        }

        internal ConstructorInfo GetConstructor(Type type, ResolutionTracer tracer)
        {
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

            return ctor;
        }

        internal object[] GetConstructorParams(ConstructorInfo ctor, ResolutionTracer tracer, ResolutionContext context, params IParameterOverride[] overrides)
        {
            return ctor.GetParameters()
                .Select(p =>
                {
                    tracer?.Add($"Resolving {ctor.DeclaringType} constructor parameter of type {p.ParameterType}");
                    object obj = overrides.FirstOrDefault(o => o.CanOverride(p))?.ValueForParameter(p, this);

                    if (obj != null)
                    {
                        tracer?.Add($"Using paramater override value {obj}.");
                        return obj;
                    }
                    else if (TryResolve(p.ParameterType, out obj, tracer, this, context, new IParameterOverride[] { }))
                    {
                        return obj;
                    }
                    else
                    {
                        return null;
                    }
                }).ToArray();
        }

        internal bool TryResolve(Type type, out object obj, ResolutionTracer tracer, Container container, ResolutionContext context, params IParameterOverride[] overrides)
        {
            bool traceHead = false;

            if (tracer == null && _traceEnabled)
            {
                tracer = new ResolutionTracer(null, TraceName);
                traceHead = true;
                tracer?.Add($"Resolving type {type}...");
            }

            try
            {
                ThrowIfDisposed();
                type.ThrowIfNull(nameof(type));
                container.ThrowIfNull(nameof(container));

                obj = null;

                if (context == null)
                {
                    // first level in resolution
                    context = new ResolutionContext(type);
                }
                else
                {
                    context = new ResolutionContext(type, context);
                }

                var k = new RegistrationKey(type);

                if (_registrations.ContainsKey(k))
                {
                    tracer?.Add($"Using local registration...");
                    obj = _registrations[k].GetObject(tracer, context, this, overrides);
                }
                else if (k.TryMakeOpenGeneric(out var openGenericKey) && _registrations.ContainsKey(openGenericKey))
                {
                    var openReg = _registrations[openGenericKey];
                    var closedReg = openReg.MakeClosedGeneric(k.Type.GenericTypeArguments);

                    _registrations.Add(k, closedReg);

                    obj = _registrations[k].GetObject(tracer, context, container, overrides);
                }
                else if (_parent != null && _parent.TryResolve(type, out object parentObj, tracer?.CreateChild(_parent.TraceName), container, context, overrides))
                {
                    tracer?.Add($"Parent resolved.");
                    obj = parentObj;
                }
                else if (container == this)
                {
                    tracer?.Add($"Creating...");
                    obj = CreateInstance(type, tracer, context, overrides);
                }
                else
                {
                    tracer?.Add($"Unable to resolve...");
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                tracer?.Add($"Exception... {ex.Message}.");

                if (traceHead)
                {
                    _logger?.Invoke(tracer.ToString());
                }

                throw;
            }

            if (traceHead)
            {
                _logger?.Invoke(tracer.ToString());
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
                    _logger?.Invoke($"{_traceName} : Disposing {_disposables?.Count ?? 0} objects...");

                    foreach (var disposable in _disposables)
                    {
                        _logger?.Invoke($"{_traceName} : Disposing {disposable}...");
                        disposable.Dispose();
                    }

                    _disposables.Clear();
                    _registrations.Clear();
                }

                _logger?.Invoke($"{_traceName} : Disposed.");
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

        private long DebugObjectId()
        {
            return (long)_ids.GetValue(this, (key) =>
            {
                return Interlocked.Increment(ref currentId);
            });
        }
    }
}