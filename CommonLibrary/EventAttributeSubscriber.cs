using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventAttributeSubscriber : Attribute
    {

        private string _name;
        public EventAttributeSubscriber(string name)
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