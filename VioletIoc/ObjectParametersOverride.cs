using System;
using System.Reflection;

namespace VioletIoc
{
    /// <summary>
    /// Object parameters override.
    /// </summary>
    public class ObjectParametersOverride : IParameterOverride
    {
        private readonly object _parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="VioletIoc.ObjectParametersOverride"/> class.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        public ObjectParametersOverride(object parameters)
        {
            _parameters = parameters.ThrowIfNull(nameof(parameters));
        }

        /// <summary>
        /// Cans the override.
        /// </summary>
        /// <returns><c>true</c>, if override was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        public bool CanOverride(ParameterInfo parameter)
        {
            return _parameters.GetType().GetRuntimeField(parameter.Name) != null ||
                       _parameters.GetType().GetRuntimeProperty(parameter.Name) != null;
        }

        /// <summary>
        /// Values for parameter.
        /// </summary>
        /// <returns>The for parameter.</returns>
        /// <param name="parameter">Parameter.</param>
        /// <param name="container">Container.</param>
        public object ValueForParameter(ParameterInfo parameter, IContainer container)
        {
            var value = _parameters
                .GetType()
                .GetRuntimeField(parameter.Name)?
                .GetValue(_parameters);

            if (value == null)
            {
                value = _parameters
                    .GetType()
                    .GetRuntimeProperty(parameter.Name)?
                    .GetValue(_parameters);
            }

            return value;
        }
    }
}
