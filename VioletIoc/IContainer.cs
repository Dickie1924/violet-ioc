using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioletIoc
{
    /// <summary>
    /// Container.
    /// </summary>
    public interface IContainer : IDisposable
    {
        /// <summary>
        /// Register the specified interfaceType, instance and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="instance">Instance.</param>
        /// <param name="key">Key.</param>
        IContainer Register(Type interfaceType, object instance, string key);

        /// <summary>
        /// Register the specified interfaceType, asType, key and singleton.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="asType">As type.</param>
        /// <param name="key">Key.</param>
        /// <param name="singleton">If set to <c>true</c> singleton.</param>
        IContainer Register(Type interfaceType, Type asType, string key, bool singleton);

        /// <summary>
        /// Register the specified interfaceType, factory, key and singleton.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <param name="singleton">If set to <c>true</c> singleton.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        IContainer Register<TType>(Type interfaceType, Func<IContainer, TType> factory, string key, bool singleton)
            where TType : class;

        /// <summary>
        /// Register the specified interfaceType, factory, key and singleton.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <param name="singleton">If set to <c>true</c> singleton.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        IContainer Register<TType>(Type interfaceType, Func<IContainer, ResolutionContext, TType> factory, string key, bool singleton)
            where TType : class;

        /// <summary>
        /// Resolve the specified overrides.
        /// </summary>
        /// <returns>The resolve.</returns>
        /// <param name="overrides">Overrides.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T Resolve<T>(params IParameterOverride[] overrides)
            where T : class;

        /// <summary>
        /// Resolve the specified key and overrides.
        /// </summary>
        /// <returns>The resolve.</returns>
        /// <param name="key">Key.</param>
        /// <param name="overrides">Overrides.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T Resolve<T>(string key, params IParameterOverride[] overrides)
            where T : class;

        /// <summary>
        /// Creates a resolver.
        /// </summary>
        /// <returns>The resolver.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        IResolver<T> ResolverFor<T>()
            where T : class;

        /// <summary>
        /// Resolve the specified type and overrides.
        /// </summary>
        /// <returns>The resolve.</returns>
        /// <param name="type">Type.</param>
        /// <param name="overrides">Overrides.</param>
        object Resolve(Type type, params IParameterOverride[] overrides);

        /// <summary>
        /// Resolve the specified type, key and overrides.
        /// </summary>
        /// <returns>The resolve.</returns>
        /// <param name="type">Type.</param>
        /// <param name="key">Key.</param>
        /// <param name="overrides">Overrides.</param>
        object Resolve(Type type, string key, params IParameterOverride[] overrides);

        /// <summary>
        /// Resolves the or default.
        /// </summary>
        /// <returns>The or default.</returns>
        /// <param name="overrides">Overrides.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T ResolveOrDefault<T>(params IParameterOverride[] overrides)
            where T : class;

        /// <summary>
        /// Resolves the or default.
        /// </summary>
        /// <returns>The or default.</returns>
        /// <param name="key">Key.</param>
        /// <param name="overrides">Overrides.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T ResolveOrDefault<T>(string key, params IParameterOverride[] overrides)
            where T : class;

        /// <summary>
        /// Tries the resolve.
        /// </summary>
        /// <returns><c>true</c>, if resolve was tryed, <c>false</c> otherwise.</returns>
        /// <param name="obj">Object.</param>
        /// <param name="overrides">Overrides.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        bool TryResolve<T>(out T obj, params IParameterOverride[] overrides)
            where T : class;

        /// <summary>
        /// Tries the resolve.
        /// </summary>
        /// <returns><c>true</c>, if resolve was tryed, <c>false</c> otherwise.</returns>
        /// <param name="key">Key.</param>
        /// <param name="obj">Object.</param>
        /// <param name="overrides">Overrides.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        bool TryResolve<T>(string key, out T obj, params IParameterOverride[] overrides)
            where T : class;

        /// <summary>
        /// Tries the resolve.
        /// </summary>
        /// <returns><c>true</c>, if resolve was tryed, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="obj">Object.</param>
        /// <param name="overrides">Overrides.</param>
        bool TryResolve(Type type, out object obj, params IParameterOverride[] overrides);

        /// <summary>
        /// Tries the resolve.
        /// </summary>
        /// <returns><c>true</c>, if resolve was tryed, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="key">Key.</param>
        /// <param name="obj">Object.</param>
        /// <param name="overrides">Overrides.</param>
        bool TryResolve(Type type, string key, out object obj, params IParameterOverride[] overrides);

        /// <summary>
        /// Gets the parent container.
        /// </summary>
        /// <returns>The parent container.</returns>
        IContainer GetParentContainer();

        /// <summary>
        /// Gets the root container.
        /// </summary>
        /// <returns>The root container.</returns>
        IContainer GetRootContainer();

        /// <summary>
        /// Creates a child container.
        /// </summary>
        /// <returns>The child container.</returns>
        IContainer CreateChildContainer();

        /// <summary>
        /// Creates a child container.
        /// </summary>
        /// <returns>The child container.</returns>
        /// <param name="appendTraceName">Append trace name.</param>
        IContainer CreateChildContainer(string appendTraceName);

        /// <summary>
        /// Cans the resolve.
        /// </summary>
        /// <returns><c>true</c>, if resolve was caned, <c>false</c> otherwise.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        bool CanResolve<T>();

        /// <summary>
        /// Cans the resolve.
        /// </summary>
        /// <returns><c>true</c>, if resolve was caned, <c>false</c> otherwise.</returns>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        bool CanResolve<T>(string key);

        /// <summary>
        /// Cans the resolve.
        /// </summary>
        /// <returns><c>true</c>, if resolve was caned, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        bool CanResolve(Type type);

        /// <summary>
        /// Cans the resolve.
        /// </summary>
        /// <returns><c>true</c>, if resolve was caned, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="key">Key.</param>
        bool CanResolve(Type type, string key);

        /// <summary>
        /// Cans the resolve locally.
        /// </summary>
        /// <returns><c>true</c>, if resolve locally was caned, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        bool CanResolveLocally(Type type);

        /// <summary>
        /// Cans the resolve locally.
        /// </summary>
        /// <returns><c>true</c>, if resolve locally was caned, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="key">Key.</param>
        bool CanResolveLocally(Type type, string key);
    }
}
