using System.IO;
using System.Text;
using System.Xml.Serialization;
using System;

namespace MyDLL
{
    public class My
    {
        public static String encode(String s)
        {
            if (s == null) return s;
            byte[] buf = Encoding.UTF8.GetBytes(s);
            s = "";
            foreach (byte b in buf) s = s + b.ToString("X2");
            return s;
        }
        public static String decode(String s)
        {
            if (s == null) return s;
            byte[] buf = new byte[s.Length / 2];
            for (int i = 0; i < buf.Length; i++)
                buf[i] = byte.Parse(s.Substring(2 * i, 2), System.Globalization.NumberStyles.HexNumber);
            return Encoding.UTF8.GetString(buf);
        }
        public static String serialize<T>(T obj)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            xml.Serialize(ms, obj);
            String s = Encoding.UTF8.GetString(ms.ToArray());
            return s;
        }
        public static T deserialize<T>(String s)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            byte[] buf = Encoding.UTF8.GetBytes(s);
            MemoryStream ms = new MemoryStream(buf);
            T obj = (T)xml.Deserialize(ms);
            return obj;
        }
    }
}
