using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using CommonLibrary; 
namespace MethodHandlerLibrary
{   
        public class PackageVariableMetaData
        {
            [EventAttributeSubscriber("DynamicEventMethod")]
            public void AddParameterData(object sender, ParameterEventArgs pe)
        {
            Hashtable ht = pe.ParameterCollection;
            ht["name"] = ht["name"] + " Method got invoked";
        }
        }
}
