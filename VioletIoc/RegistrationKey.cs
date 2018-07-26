using System;

namespace VioletIoc
{
    internal struct RegistrationKey
    {
        private readonly Type _type;

        public RegistrationKey(Type type)
        {
            _type = type;
        }

        public Type Type => _type;

        public static implicit operator Type(RegistrationKey k)
        {
            return k._type;
        }

        public static implicit operator RegistrationKey(Type t)
        {
            return new RegistrationKey(t);
        }

        public bool TryMakeOpenGeneric(out RegistrationKey genericRegistrationKey)
        {
            if (_type.IsGenericType)
            {
                genericRegistrationKey = new RegistrationKey(_type.GetGenericTypeDefinition());
                return true;
            }

            genericRegistrationKey = null;
            return false;
        }
    }
}
