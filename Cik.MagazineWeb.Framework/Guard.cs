namespace Cik.MagazineWeb.Framework
{
    using System;
    using System.Globalization;

    public static class Guard
    {
        // Methods
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
#if DEBUG
            System.Diagnostics.Contracts.Contract.Assert(argumentValue != null, argumentName);
#endif

            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        // Methods
        public static void ArgumentMustMoreThanZero(int? argumentValue, string argumentName)
        {
#if DEBUG
            System.Diagnostics.Contracts.Contract.Assert(argumentValue > 0, argumentName);
#endif

            if (argumentValue <= 0)
            {
                throw new ArgumentException(string.Format("{0} must be more than zero", argumentName));
            }
        }

        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
#if DEBUG
            System.Diagnostics.Contracts.Contract.Assert(!string.IsNullOrEmpty(argumentValue), argumentName);
#endif

            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            if (argumentValue.Length == 0)
            {
                throw new ArgumentException("ArgumentMustNotBeEmpty", argumentName);
            }
        }

        private static string GetTypeName(object assignmentInstance)
        {
            try
            {
                return assignmentInstance.GetType().FullName;
            }
            catch (Exception)
            {
                return "UnknownType";
            }
        }

        public static void InstanceIsAssignable(Type assignmentTargetType, object assignmentInstance, string argumentName)
        {
            if (assignmentTargetType == null)
            {
                throw new ArgumentNullException("assignmentTargetType");
            }
            if (assignmentInstance == null)
            {
                throw new ArgumentNullException("assignmentInstance");
            }
            if (!assignmentTargetType.IsInstanceOfType(assignmentInstance))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "TypesAreNotAssignable", new object[] { assignmentTargetType, GetTypeName(assignmentInstance) }), argumentName);
            }
        }

        public static void TypeIsAssignable(Type assignmentTargetType, Type assignmentValueType, string argumentName)
        {
            if (assignmentTargetType == null)
            {
                throw new ArgumentNullException("assignmentTargetType");
            }
            if (assignmentValueType == null)
            {
                throw new ArgumentNullException("assignmentValueType");
            }
            if (!assignmentTargetType.IsAssignableFrom(assignmentValueType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "TypesAreNotAssignable", new object[] { assignmentTargetType, assignmentValueType }), argumentName);
            }
        }

        public static void EnsureAllInstanceIsNotNull(params object[] objs)
        {
            foreach (var o in objs)
            {
                ArgumentNotNull(o, o.GetType().FullName);
            }
        }

        public static void IsTrue(bool predicate, string message)
        {
            if (!predicate)
                throw new ArgumentNullException(message);
        }
    }
}