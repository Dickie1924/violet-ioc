using System;

namespace VioletIoc
{
    /// <summary>
    /// Container factory.
    /// </summary>
    public static class ContainerFactory
    {
        /// <summary>
        /// Creates a root container.
        /// </summary>
        /// <returns>The root container.</returns>
        public static IContainer CreateRootContainer()
        {
            return new Container();
        }

        /// <summary>
        /// Creates a root container.
        /// </summary>
        /// <returns>The root container logging to Console.WriteLine.</returns>
        /// <param name="traceName">Trace name.</param>
        public static IContainer CreateRootContainer(string traceName)
        {
            return new Container(null, traceName);
        }

        /// <summary>
        /// Creates a root container.
        /// </summary>
        /// <returns>The root container.</returns>
        /// <param name="traceName">Trace name.</param>
        /// <param name="logger">Logger.</param>
        public static IContainer CreateRootContainer(string traceName, Action<string> logger)
        {
            return new Container(null, traceName, logger);
        }
    }
}
