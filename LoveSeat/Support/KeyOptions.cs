﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace LoveSeat
{
    public interface IKeyOptions
    {       
        string ToString();
        void Insert(int index, JToken item);
        void RemoveAt(int index);
        void Add(JToken item);
        bool Remove(JToken item);
        int Count { get; }
        bool HasValues { get; }
        void Add(object content);
    }

    public class KeyOptions : IKeyOptions
    {
        private JArray objects;
       public KeyOptions(params object[] objects) 
       {           
           this.objects = new JArray(objects);
       }
        public KeyOptions(JArray jArray)
        {
            this.objects = jArray;
        }
       public override string ToString()
       {
           if (objects.Count ==0 ) return "";
           if (objects.Count  ==1 ) return objects[0].ToString(Formatting.None, new IsoDateTimeConverter());
           string result = "[";
           bool first = true;
           foreach (var item in objects)
           {
               if (!first)
                   result += ",";
               first = false;
               result +=  item.ToString(Formatting.None, new IsoDateTimeConverter());
           }
           result += "]";
           return result;
       }

        public void Insert(int index, JToken item)
        {
            objects.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            objects.RemoveAt(index);
        }

        public void Add(JToken item)
        {
            objects.Add(item);
        }

        public bool Remove(JToken item)
        {
            return objects.Remove(item);
        }

        public int Count
        {
            get { return objects.Count; }
        }

        public bool HasValues
        {
            get { return objects.Count > 0; }
        }
        public void Add(params object[] items)
        {
            foreach (var item in items)
                Add(item);
        }
        public void Add(object item)
        {
            if (item == CouchValue.Empty)
            {
                objects.Add(new JRaw("{}"));
                return;
            }
            objects.Add(item);
        }
    }
    public static class CouchValue
    {
        static object value = new object();
        public static object Empty
        {
            get
            {
                return value;
            }
        }
    }
}