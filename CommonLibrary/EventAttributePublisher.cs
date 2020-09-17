using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    [AttributeUsage(AttributeTargets.Event)]
    public class EventAtrributePublisher : Attribute
    {
        private string _name;
        public EventAtrributePublisher(string name)
        {
            this._name = name;
        }
        public string Name
        {
            get
            {
                return this._name;
            }
        }
    }
}
