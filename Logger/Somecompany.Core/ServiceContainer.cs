using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Somecompany.Core
{
    public class ServiceContainer
    {
        private readonly IDictionary<ServiceMappingKey, Type> _registeredServices = new Dictionary<ServiceMappingKey, Type>();
        public void Register(Type from, Type to, string instanceName = null)
        {
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }

            if (!from.GetTypeInfo().IsAssignableFrom(to.GetTypeInfo()))
            {
                string errorMessage = string.Format("Error trying to register the instance: '{0}' is not assignable from '{1}'",
                     from.FullName, to.FullName);

                throw new InvalidOperationException(errorMessage);
            }

            ServiceMappingKey key = new ServiceMappingKey(from, instanceName);
            if (_registeredServices.ContainsKey(key))
            {
                const string errorMessageFormat = "The requested mapping already exists - {0}";
                throw new InvalidOperationException(string.Format(errorMessageFormat, key.ToString()));
            }
            _registeredServices.Add(key, to);
        }

        public void ReplaceMapping(Type from, Type to)
        {
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }

            if (!from.GetTypeInfo().IsAssignableFrom(to.GetTypeInfo()))
            {
                string errorMessage = string.Format("Error trying to register the instance: '{0}' is not assignable from '{1}'",
                     from.FullName, to.FullName);

                throw new InvalidOperationException(errorMessage);
            }

            ServiceMappingKey key = new ServiceMappingKey(from, null);
            if (_registeredServices.ContainsKey(key))
            {
                _registeredServices[key] = to;
                return;
            }
            _registeredServices.Add(key, to);
        }

        private readonly IDictionary<ServiceMappingKey, object> _instancesCache = new Dictionary<ServiceMappingKey, object>();

        public object Resolve(Type type, string instanceName = null)
        {
            if (!IsRegistered(type, instanceName))
            {
                return null;
            }
            ServiceMappingKey key = new ServiceMappingKey(type, instanceName);
            object instance;
            if (_instancesCache.TryGetValue(key, out instance))
            {
                return instance;
            }
            instance = Activator.CreateInstance(_registeredServices[key]);
            _instancesCache.Add(key, instance);
            return instance;
        }

        public T Resolve<T>(string instanceName = null)
        {
            return (T)Resolve(typeof(T), instanceName); ;
        }

        private bool IsRegistered(Type type, string instanceName = null)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var key = new ServiceMappingKey(type, instanceName);
            return _registeredServices.ContainsKey(key);
        }

        private ServiceContainer()
        {
        }

        private static readonly Lazy<ServiceContainer> _instance = new Lazy<ServiceContainer>(() => new ServiceContainer());

        public static ServiceContainer Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }

    public class ServiceMappingKey
    {
        public Type Type { get; private set; }
        public string InstanceName { get; private set; }

        public ServiceMappingKey(Type type, string instanceName)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Type = type;
            InstanceName = instanceName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + Type.GetHashCode();
                hash = hash * multiplier + (InstanceName == null ? 0 : InstanceName.GetHashCode());

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ServiceMappingKey compareTo = obj as ServiceMappingKey;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (compareTo == null)
                return false;

            return Type.Equals(compareTo.Type)
               && string.Equals(InstanceName, compareTo.InstanceName, StringComparison.CurrentCultureIgnoreCase);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", Type.FullName, InstanceName == null ? string.Empty : InstanceName);
        }

    }
}

