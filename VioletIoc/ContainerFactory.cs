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
        /// <returns>The root container.</returns>
        /// <param name="traceName">Trace name.</param>
        public static IContainer CreateRootContainer(string traceName)
        {
            return new Container(null, traceName);
        }
    }
}
