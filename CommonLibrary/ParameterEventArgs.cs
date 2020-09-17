using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CommonLibrary
{
    public class ParameterEventArgs : EventArgs
    {
        private Hashtable _parameterCollection;
        public ParameterEventArgs(Hashtable ht)
        {
            _parameterCollection = ht;
        }
        public Hashtable ParameterCollection
        {
            get
            {
                return _parameterCollection;
            }
        }
    }  
}
