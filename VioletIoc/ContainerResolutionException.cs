using System;

namespace VioletIoc
{
    /// <summary>
    /// Container resolution exception.
    /// </summary>
    public class ContainerResolutionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.ContainerResolutionException"/> class.
        /// </summary>
        /// <param name="resolvingType">Resolving type.</param>
        /// <param name="innerException">Inner exception.</param>
        public ContainerResolutionException(Type resolvingType, Exception innerException)
            : base($"Could not resolve type {resolvingType.Name}", innerException)
        {
        }
    }
}
