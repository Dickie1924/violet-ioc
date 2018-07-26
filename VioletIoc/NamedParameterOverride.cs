using System;
using System.Reflection;

namespace VioletIoc
{
    /// <summary>
    /// Named parameter override.
    /// </summary>
    public class NamedParameterOverride : IParameterOverride
    {
        private readonly string _paramName;
        private readonly object _value;
        private readonly Func<IContainer, object> _valueFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.NamedParameterOverride"/> class.
        /// </summary>
        /// <param name="paramName">Parameter name.</param>
        /// <param name="value">Value.</param>
        public NamedParameterOverride(string paramName, object value)
        {
            _paramName = paramName.ThrowIfNull(nameof(paramName));
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.NamedParameterOverride"/> class.
        /// </summary>
        /// <param name="paramName">Parameter name.</param>
        /// <param name="valueFactory">Value factory.</param>
        public NamedParameterOverride(string paramName, Func<IContainer, object> valueFactory)
        {
            _paramName = paramName.ThrowIfNull(nameof(paramName));
            _valueFactory = valueFactory.ThrowIfNull(nameof(valueFactory));
        }

        /// <summary>
        /// Override the specified value.
        /// </summary>
        /// <returns>The override.</returns>
        /// <param name="paramName">Parameter name.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static NamedParameterOverride Override<T>(string paramName, T value)
            where T : class
        {
            return new NamedParameterOverride(paramName, value);
        }

        /// <summary>
        /// Override the specified valueFactory.
        /// </summary>
        /// <returns>The override.</returns>
        /// <param name="paramName">Parameter name.</param>
        /// <param name="valueFactory">Value factory.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static NamedParameterOverride Override<T>(string paramName, Func<IContainer, T> valueFactory)
            where T : class
        {
            return new NamedParameterOverride(paramName, valueFactory);
        }

        /// <summary>
        /// Cans the override.
        /// </summary>
        /// <returns><c>true</c>, if override was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        public bool CanOverride(ParameterInfo parameter)
            => parameter.Name == _paramName;

        /// <summary>
        /// Values for parameter.
        /// </summary>
        /// <returns>The for parameter.</returns>
        /// <param name="parameter">Parameter.</param>
        /// <param name="container">Container.</param>
        public object ValueForParameter(ParameterInfo parameter, IContainer container)
        {
            if (parameter.Name != _paramName)
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
