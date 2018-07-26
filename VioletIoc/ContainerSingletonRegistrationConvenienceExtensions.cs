using System;

namespace VioletIoc
{
    /// <summary>
    /// Container singleton registration convenience extensions.
    /// </summary>
    public static class ContainerSingletonRegistrationConvenienceExtensions
    {
        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="instance">Instance.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TType>(this IContainer container, TType instance)
            where TType : class
            => container.Register(typeof(TType), instance, (string)null);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="instance">Instance.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TType>(this IContainer container, TType instance, string key)
            where TType : class
            => container.Register(typeof(TType), instance, key);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TType>(this IContainer container)
            where TType : class
            => container.Register(typeof(TType), typeof(TType), (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface, TType>(this IContainer container)
            where TType : class, TInterface
            => container.Register(typeof(TInterface), typeof(TType), (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface, TType>(this IContainer container, string key)
            where TType : class, TInterface
            => container.Register(typeof(TInterface), typeof(TType), key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="instance">Instance.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface, TType>(this IContainer container, TType instance)
            where TType : class, TInterface
            => container.Register(typeof(TInterface), instance, (string)null);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="instance">Instance.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface, TType>(this IContainer container, TType instance, string key)
            where TType : class, TInterface
            => container.Register(typeof(TInterface), instance, key);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TType>(this IContainer container, Func<IContainer, TType> factory)
            where TType : class
            => container.Register<TType>(typeof(TType), factory, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TType>(this IContainer container, Func<IContainer, TType> factory, string key)
            where TType : class
            => container.Register<TType>(typeof(TType), factory, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TType>(this IContainer container, Func<IContainer, ResolutionContext, TType> factory)
            where TType : class
            => container.Register<TType>(typeof(TType), factory, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TType>(this IContainer container, Func<IContainer, ResolutionContext, TType> factory, string key)
            where TType : class
            => container.Register<TType>(typeof(TType), factory, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface, TType>(this IContainer container, Func<IContainer, TType> factory)
            where TType : class, TInterface
            => container.Register<TType>(typeof(TInterface), factory, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface, TType>(this IContainer container, Func<IContainer, TType> factory, string key)
            where TType : class, TInterface
            => container.Register<TType>(typeof(TInterface), factory, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface, TType>(this IContainer container, Func<IContainer, ResolutionContext, TType> factory)
            where TType : class, TInterface
            => container.Register<TType>(typeof(TInterface), factory, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface, TType>(this IContainer container, Func<IContainer, ResolutionContext, TType> factory, string key)
            where TType : class, TInterface
            => container.Register<TType>(typeof(TInterface), factory, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="asType">As type.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type asType)
            => container.Register(asType, asType, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="asType">As type.</param>
        /// <param name="key">Key.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type asType, string key)
            => container.Register(asType, asType, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="asType">As type.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type interfaceType, Type asType)
            => container.Register(interfaceType, asType, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="asType">As type.</param>
        /// <param name="key">Key.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type interfaceType, Type asType, string key)
            => container.Register(interfaceType, asType, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="instance">Instance.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type interfaceType, object instance)
            => container.Register(interfaceType, instance, (string)null);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="instance">Instance.</param>
        /// <param name="key">Key.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type interfaceType, object instance, string key)
            => container.Register(interfaceType, instance, key);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="factory">Factory.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type interfaceType, Func<IContainer, object> factory)
            => container.Register<object>(interfaceType, factory, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type interfaceType, Func<IContainer, object> factory, string key)
            => container.Register<object>(interfaceType, factory, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="factory">Factory.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type interfaceType, Func<IContainer, ResolutionContext, object> factory)
            => container.Register<object>(interfaceType, factory, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        public static IContainer RegisterSingleton(this IContainer container, Type interfaceType, Func<IContainer, ResolutionContext, object> factory, string key)
            => container.Register<object>(interfaceType, factory, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface>(this IContainer container, Func<IContainer, object> factory)
            => container.Register<object>(typeof(TInterface), factory, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface>(this IContainer container, Func<IContainer, object> factory, string key)
            => container.Register<object>(typeof(TInterface), factory, key, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface>(this IContainer container, Func<IContainer, ResolutionContext, object> factory)
            => container.Register<object>(typeof(TInterface), factory, (string)null, true);

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <returns>The singleton.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer RegisterSingleton<TInterface>(this IContainer container, Func<IContainer, ResolutionContext, object> factory, string key)
            => container.Register<object>(typeof(TInterface), factory, key, true);
    }
}
