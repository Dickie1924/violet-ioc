using System;

namespace VioletIoc
{
    internal struct RegistrationKey
    {
        private readonly Type _type;
        private readonly string _key;

        public RegistrationKey(Type type, string key)
        {
            _type = type;
            _key = key;
        }

        public Type Type => _type;

        public string Key => _key;

        public static implicit operator Type(RegistrationKey k)
        {
            return k._type;
        }

        public static implicit operator RegistrationKey(Type t)
        {
            return new RegistrationKey(t, null);
        }

        public bool TryMakeOpenGeneric(out RegistrationKey genericRegistrationKey)
        {
            if (_type.IsGenericType)
            {
                genericRegistrationKey = new RegistrationKey(_type.GetGenericTypeDefinition(), _key);
                return true;
            }

            genericRegistrationKey = null;
            return false;
        }
    }
}
