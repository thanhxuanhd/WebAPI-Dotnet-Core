using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Service.DataContracts
{
    public class BaseDto
    {
        private List<string> _serializableProperties;

        public List<string> SerializableProperties
        {
            get
            {
                if (_serializableProperties == null)
                {
                    var members = this.GetType().GetTypeInfo().GetMembers();
                    _serializableProperties = new List<string>();
                    _serializableProperties.AddRange(members.Select(x => x.Name).ToList());
                }

                return _serializableProperties;
            }
            set { _serializableProperties = value; }
        }

        public void SetSerializableProperties(string fields)
        {
            if (!string.IsNullOrEmpty(fields))
            {
                var returnFields = fields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                _serializableProperties = returnFields.ToList();
                return;
            }
            var members = this.GetType().GetTypeInfo().GetMembers();

            _serializableProperties = new List<string>();
            _serializableProperties.AddRange(members.Select(x => x.Name).ToList());
        }
    }
}