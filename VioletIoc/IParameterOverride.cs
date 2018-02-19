using System.Reflection;

namespace VioletIoc
{
    /// <summary>
    /// Parameter override.
    /// </summary>
    public interface IParameterOverride
    {
        /// <summary>
        /// Cans the override.
        /// </summary>
        /// <returns><c>true</c>, if override was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        bool CanOverride(ParameterInfo parameter);

        /// <summary>
        /// Values for parameter.
        /// </summary>
        /// <returns>The for parameter.</returns>
        /// <param name="parameter">Parameter.</param>
        /// <param name="container">Container.</param>
        object ValueForParameter(ParameterInfo parameter, IContainer container);
    }
}
