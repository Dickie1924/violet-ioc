using System;

namespace VioletIoc
{
    /// <summary>
    /// Container registration convenience extensions.
    /// </summary>
    public static class ContainerRegistrationConvenienceExtensions
    {
        /// <summary>
        /// Register the specified container.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer Register<TType>(this IContainer container)
            where TType : class
            => container.Register(typeof(TType), typeof(TType), (string)null, false);

        /// <summary>
        /// Register the specified container and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TType">The 1st type parameter.</typeparam>
        public static IContainer Register<TType>(this IContainer container, string key)
            where TType : class
            => container.Register(typeof(TType), typeof(TType), key, false);

        /// <summary>
        /// Register the specified container.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer Register<TInterface, TType>(this IContainer container)
            where TInterface : class
            where TType : class, TInterface
            => container.Register(typeof(TInterface), typeof(TType), (string)null, false);

        /// <summary>
        /// Register the specified container and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer Register<TInterface, TType>(this IContainer container, string key)
            where TInterface : class
            where TType : class, TInterface
            => container.Register(typeof(TInterface), typeof(TType), key, false);

        /// <summary>
        /// Register the specified container and factory.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer Register<TInterface>(this IContainer container, Func<IContainer, TInterface> factory)
            where TInterface : class
            => container.Register<TInterface>(factory, (string)null);

        /// <summary>
        /// Register the specified container, factory and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer Register<TInterface>(this IContainer container, Func<IContainer, TInterface> factory, string key)
            where TInterface : class
            => container.Register<TInterface>(typeof(TInterface), factory, key, false);

        /// <summary>
        /// Register the specified container and factory.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer Register<TInterface>(this IContainer container, Func<IContainer, ResolutionContext, TInterface> factory)
            where TInterface : class
            => container.Register<TInterface>(typeof(TInterface), factory, (string)null, false);

        /// <summary>
        /// Register the specified container, factory and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer Register<TInterface>(this IContainer container, Func<IContainer, ResolutionContext, TInterface> factory, string key)
            where TInterface : class
            => container.Register<TInterface>(typeof(TInterface), factory, key, false);

        /// <summary>
        /// Register the specified container and factory.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer Register<TInterface>(this IContainer container, Func<IContainer, object> factory)
            where TInterface : class
            => container.Register<object>(factory, (string)null);

        /// <summary>
        /// Register the specified container, factory and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer Register<TInterface>(this IContainer container, Func<IContainer, object> factory, string key)
            where TInterface : class
            => container.Register<object>(typeof(TInterface), factory, key, false);

        /// <summary>
        /// Register the specified container and factory.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer Register<TInterface>(this IContainer container, Func<IContainer, ResolutionContext, object> factory)
            where TInterface : class
            => container.Register<object>(typeof(TInterface), factory, (string)null, false);

        /// <summary>
        /// Register the specified container, factory and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        public static IContainer Register<TInterface>(this IContainer container, Func<IContainer, ResolutionContext, object> factory, string key)
            where TInterface : class
            => container.Register<object>(typeof(TInterface), factory, key, false);

        /// <summary>
        /// Register the specified container and factory.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer Register<TInterface, TType>(this IContainer container, Func<IContainer, TType> factory)
            where TType : class, TInterface
            => container.Register<TType>(typeof(TInterface), factory, (string)null, false);

        /// <summary>
        /// Register the specified container, factory and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer Register<TInterface, TType>(this IContainer container, Func<IContainer, TType> factory, string key)
            where TType : class, TInterface
            => container.Register<TType>(typeof(TInterface), factory, key, false);

        /// <summary>
        /// Register the specified container and factory.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer Register<TInterface, TType>(this IContainer container, Func<IContainer, ResolutionContext, TType> factory)
            where TType : class, TInterface
            => container.Register<TType>(typeof(TInterface), factory, (string)null, false);

        /// <summary>
        /// Register the specified container, factory and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="factory">Factory.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="TInterface">The 1st type parameter.</typeparam>
        /// <typeparam name="TType">The 2nd type parameter.</typeparam>
        public static IContainer Register<TInterface, TType>(this IContainer container, Func<IContainer, ResolutionContext, TType> factory, string key)
            where TType : class, TInterface
            => container.Register<TType>(typeof(TInterface), factory, key, false);

        /// <summary>
        /// Register the specified container and asType.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="asType">As type.</param>
        public static IContainer Register(this IContainer container, Type asType)
            => container.Register(asType, asType, (string)null, false);

        /// <summary>
        /// Register the specified container, asType and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="asType">As type.</param>
        /// <param name="key">Key.</param>
        public static IContainer Register(this IContainer container, Type asType, string key)
            => container.Register(asType, asType, key, false);

        /// <summary>
        /// Register the specified container, interfaceType and asType.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="asType">As type.</param>
        public static IContainer Register(this IContainer container, Type interfaceType, Type asType)
            => container.Register(interfaceType, asType, (string)null, false);

        /// <summary>
        /// Register the specified container, interfaceType, asType and key.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="container">Container.</param>
        /// <param name="interfaceType">Interface type.</param>
        /// <param name="asType">As type.</param>
        /// <param name="key">Key.</param>
        public static IContainer Register(this IContainer container, Type interfaceType, Type asType, string key)
            => container.Register(interfaceType, asType, key, false);
    }
}
