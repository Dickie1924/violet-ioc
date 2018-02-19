using System;
using System.Reflection;

namespace VioletIoc
{
    /// <summary>
    /// Resolution context.
    /// </summary>
    public class ResolutionContext
    {
        internal ResolutionContext(Type targetType)
        {
            TargetType = targetType;
        }

        internal ResolutionContext(Type targetType, ResolutionContext parentContext)
        {
            TargetType = targetType;
            ParentContext = parentContext;
        }

        /// <summary>
        /// Gets the type of the target.
        /// </summary>
        /// <value>The type of the target.</value>
        public Type TargetType { get; private set; }

        /// <summary>
        /// Gets the parent context.
        /// </summary>
        /// <value>The parent context.</value>
        public ResolutionContext ParentContext { get; private set; }

        /// <summary>
        /// Gets the type of the resolved.
        /// </summary>
        /// <value>The type of the resolved.</value>
        public Type ResolvedType { get; internal set; }
    }
}
