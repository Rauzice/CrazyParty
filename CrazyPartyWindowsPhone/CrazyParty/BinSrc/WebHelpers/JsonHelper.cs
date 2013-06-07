using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.Generic;

namespace CrazyParty.BinSrc
{
    public class JsonHelper
    {
        public static string ObjectToJson(Type type, List<Type> knowtypes, object obj)
        { 
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(type, knowtypes);
            MemoryStream ms = new MemoryStream();

            var o = Convert.ChangeType(obj, type, null);

            serializer.WriteObject(ms, o);
            ms.Position = 0;
            byte[] buffer = new byte[ms.Length];
            ms.Read(buffer, 0, buffer.Length);
            return System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }


        public static object JsonToObject(Type type, List<Type> knowtypes, string jsonString)
        {
            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(type, knowtypes);
            return serializer.ReadObject(ms);
        }
    }
}
