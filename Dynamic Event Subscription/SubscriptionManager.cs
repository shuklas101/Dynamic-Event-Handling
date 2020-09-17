using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using CommonLibrary;
namespace Dynamic_Event_Subscription
{
    public class SubscriptionManager
    {
        private static Dictionary<string, List<object>> _subscriberList;
        private static Dictionary<string, List<object>> _publisherList;
        static SubscriptionManager()
        {
            _subscriberList = new Dictionary<string, List<object>>();
            _publisherList = new Dictionary<string, List<object>>();
        }
        public static void PublishEvents(object instance)
        {
            Type t = instance.GetType();
            EventInfo[] events = t.GetEvents();
            foreach (EventInfo e in events)
            {
                EventAtrributePublisher attr = (EventAtrributePublisher)Attribute.GetCustomAttribute(e,
                    typeof(EventAtrributePublisher), false);
                if (attr != null)
                {
                    AddEventPublishEvent(attr.Name, instance, e);
                }
            }
        }
        private static void AddEventPublishEvent(string name, object instance, EventInfo e)
        {
            List<object> pubs = null;
            if (_publisherList.ContainsKey(name))
            {
                pubs = _publisherList[name];
            }
            else
            {
                pubs = new List<object>();
                _publisherList[name] = pubs;
            }
            object toAdd = new object[] { instance, e };
            pubs.Add(toAdd);
        }
        public static void LinkPublishers()
        {
            foreach (KeyValuePair<string, List<object>> obj in _publisherList)
            {
                LinkPubsSubs(obj.Key);
            }
        }
        private static void LinkPubsSubs(string name)
        {
            List<object> pubs = null;
            if (_publisherList.ContainsKey(name))
            {
                pubs = _publisherList[name];
            }
            if (pubs != null && pubs.Count > 0)
            {
                if (_subscriberList.ContainsKey(name))
                {
                    List<object> subs = _subscriberList[name];
                    foreach (object[] subscriber in subs)
                    {
                        object[] data = (object[])subscriber;
                        Type type = data[0].GetType();
                        MethodInfo mi = (MethodInfo)data[1];
                        Delegate dlg = null;
                        foreach (object[] publisher in pubs)
                        {
                            object[] pInfo = (object[])publisher;
                            object pubInstance = publisher[0];
                            EventInfo pubEvent = (EventInfo)publisher[1];
                            if (dlg == null)
                            {
                                dlg = (Delegate)Activator.CreateInstance(pubEvent.EventHandlerType, new object[] { data[0], mi.MethodHandle.GetFunctionPointer() });
                                if (dlg != null)
                                {
                                    pubEvent.AddEventHandler(pubInstance, dlg);
                                }
                                dlg = null;
                            }
                        }
                    }
                }
            }
        }
        public static void AddSubscriber(Assembly assembly)
        {
            Type[] allTypes = assembly.GetExportedTypes();            
            if (allTypes != null)
            {
                foreach (Type t in allTypes)
                {
                    if (t.IsClass)
                    {
                        MethodInfo[] mtCollection = t.GetMethods();
                        foreach (MethodInfo m in mtCollection)
                        {
                            EventAttributeSubscriber spa = (EventAttributeSubscriber)
                                Attribute.GetCustomAttribute(m,
                                    typeof(EventAttributeSubscriber));
                            if (spa != null)
                            {
                                AddEventSubscriber(spa.Name, t, m);
                            }
                        }
                    }
                }
            }
        }
        private static void AddEventSubscriber(string name, object instance, MethodInfo current)
        {
            List<object> subs = null;
            if (_subscriberList.ContainsKey(name))
            {
                subs = _subscriberList[name];
            }
            else
            {
                subs = new List<object>();
                _subscriberList[name] = subs;
            }
            object toAdd = new object[] { instance, current };
            subs.Add(toAdd);
        }
    }
}
