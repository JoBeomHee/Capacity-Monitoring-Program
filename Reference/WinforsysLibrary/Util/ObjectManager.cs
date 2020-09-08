using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Winforsys.Util
{
    public class ObjectManager
    {
        public static bool Save<T>(T obj, string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, obj);
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return false;
            }

            return true;
        }

        public static bool Load<T>(ref T obj, string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                obj = default(T);
                
                return false;
            }

            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    obj = (T)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    obj = default(T);
                    return false;
                }
            }

            return true;
        }

        public static bool Compare<T>(T obj01, T obj02, params object[] exceptObjs)
        {
            FieldInfo[] obj01Arr = obj01.GetType().GetFields();
            FieldInfo[] obj02Arr = obj02.GetType().GetFields();

            foreach(FieldInfo fi in obj01Arr)
            {
                bool skip = false;

                foreach (object except in exceptObjs)
                {
                    if (object.Equals(fi.GetValue(obj01), except))
                    {
                        skip = true;
                        break;
                    }
                }

                if (skip) continue;

                if (object.Equals(fi.GetValue(obj01), fi.GetValue(obj02)) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Copy<T>(T tObj, T sObj, params object[] exceptObjs)
        {
            FieldInfo[] obj01Arr = tObj.GetType().GetFields();
            FieldInfo[] obj02Arr = sObj.GetType().GetFields();

            try
            {
                foreach (FieldInfo fi in obj01Arr)
                {
                    bool skip = false;

                    foreach (object except in exceptObjs)
                    {
                        if (object.Equals(fi.GetValue(tObj), except))
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (skip) continue;

                    fi.SetValue(tObj, fi.GetValue(sObj));
                }
            }
            catch(Exception ex)
            {
                if (ex.Message != string.Empty)
                    return false;
            }

            return true;
        }
    }
}
