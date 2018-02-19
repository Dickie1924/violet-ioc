using System;

namespace VioletIoc
{
    /// <summary>
    /// Parameter override.
    /// </summary>
    /// <typeparam name="T">The type of parameter</typeparam>
    public class ParameterOverride<T> : ParameterOverride
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.ParameterOverride{T}"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        public ParameterOverride(T value)
            : base(typeof(T), value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.ParameterOverride{T}"/> class.
        /// </summary>
        /// <param name="valueFactory">Value factory.</param>
        public ParameterOverride(Func<IContainer, T> valueFactory)
            : base(typeof(T), valueFactory)
        {
        }
    }
}
