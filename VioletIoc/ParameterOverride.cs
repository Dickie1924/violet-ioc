using System;
using System.Reflection;

namespace VioletIoc
{
    /// <summary>
    /// Parameter override.
    /// </summary>
    public class ParameterOverride : IParameterOverride
    {
        private readonly Type _paramType;
        private readonly object _value;
        private readonly Func<IContainer, object> _valueFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.ParameterOverride"/> class.
        /// </summary>
        /// <param name="paramType">Parameter type.</param>
        /// <param name="value">Value.</param>
        public ParameterOverride(Type paramType, object value)
        {
            _paramType = paramType.ThrowIfNull(nameof(paramType));
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.ParameterOverride"/> class.
        /// </summary>
        /// <param name="paramType">Parameter type.</param>
        /// <param name="valueFactory">Value factory.</param>
        public ParameterOverride(Type paramType, Func<IContainer, object> valueFactory)
        {
            _paramType = paramType.ThrowIfNull(nameof(paramType));
            _valueFactory = valueFactory.ThrowIfNull(nameof(valueFactory));
        }

        /// <summary>
        /// Override the specified value.
        /// </summary>
        /// <returns>The override.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static ParameterOverride Override<T>(T value)
            where T : class
        {
            return new ParameterOverride(typeof(T), value);
        }

        /// <summary>
        /// Override the specified valueFactory.
        /// </summary>
        /// <returns>The override.</returns>
        /// <param name="valueFactory">Value factory.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static ParameterOverride Override<T>(Func<IContainer, T> valueFactory)
            where T : class
        {
            return new ParameterOverride(typeof(T), valueFactory);
        }

        /// <summary>
        /// Cans the override.
        /// </summary>
        /// <returns><c>true</c>, if override was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        public bool CanOverride(ParameterInfo parameter)
            => parameter.ParameterType == _paramType;

        /// <summary>
        /// Values for parameter.
        /// </summary>
        /// <returns>The for parameter.</returns>
        /// <param name="parameter">Parameter.</param>
        /// <param name="container">Container.</param>
        public object ValueForParameter(ParameterInfo parameter, IContainer container)
        {
            if (parameter.ParameterType != _paramType)
            {
                return null;
            }

            if (_valueFactory != null)
            {
                return _valueFactory(container);
            }

            return _value;
        }
    }
}
