namespace VioletIoc
{
    /// <summary>
    /// Resolver.
    /// </summary>
    /// <typeparam name="T">The type that this resolver can resolve.</typeparam>
    public interface IResolver<T>
        where T : class
    {
        /// <summary>
        /// Resolve an instance.
        /// </summary>
        /// <returns>The instance.</returns>
        T Resolve();

        /// <summary>
        /// Resolve an instance with the specified param.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="param">Parameter.</param>
        /// <typeparam name="TParam">The 1st type parameter.</typeparam>
        T Resolve<TParam>(TParam param);

        /// <summary>
        /// Resolve an instance with the specified parameters.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="param1">Param1.</param>
        /// <param name="param2">Param2.</param>
        /// <typeparam name="TParam1">The 1st type parameter.</typeparam>
        /// <typeparam name="TParam2">The 2nd type parameter.</typeparam>
        T Resolve<TParam1, TParam2>(TParam1 param1, TParam2 param2);

        /// <summary>
        /// Resolve an instance with the specified parameters.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="param1">Param1.</param>
        /// <param name="param2">Param2.</param>
        /// <param name="param3">Param3.</param>
        /// <typeparam name="TParam1">The 1st type parameter.</typeparam>
        /// <typeparam name="TParam2">The 2nd type parameter.</typeparam>
        /// <typeparam name="TParam3">The 3rd type parameter.</typeparam>
        T Resolve<TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3);

        /// <summary>
        /// Resolve an instance with the specified parameters.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="param1">Param1.</param>
        /// <param name="param2">Param2.</param>
        /// <param name="param3">Param3.</param>
        /// <param name="param4">Param4.</param>
        /// <typeparam name="TParam1">The 1st type parameter.</typeparam>
        /// <typeparam name="TParam2">The 2nd type parameter.</typeparam>
        /// <typeparam name="TParam3">The 3rd type parameter.</typeparam>
        /// <typeparam name="TParam4">The 4th type parameter.</typeparam>
        T Resolve<TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);

        /// <summary>
        /// Resolve an instance with the specified parameters.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="param1">Param1.</param>
        /// <param name="param2">Param2.</param>
        /// <param name="param3">Param3.</param>
        /// <param name="param4">Param4.</param>
        /// <param name="param5">Param5.</param>
        /// <typeparam name="TParam1">The 1st type parameter.</typeparam>
        /// <typeparam name="TParam2">The 2nd type parameter.</typeparam>
        /// <typeparam name="TParam3">The 3rd type parameter.</typeparam>
        /// <typeparam name="TParam4">The 4th type parameter.</typeparam>
        /// <typeparam name="TParam5">The 5th type parameter.</typeparam>
        T Resolve<TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);
    }
}
