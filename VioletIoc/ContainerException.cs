using System;

namespace VioletIoc
{
    /// <summary>
    /// Container exception.
    /// </summary>
    public class ContainerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.ContainerException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public ContainerException(string message)
            : base(message)
        {
        }
    }
}
