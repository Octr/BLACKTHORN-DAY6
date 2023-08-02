using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Serializer
{
    public static string SerializeDictionary(Dictionary<int, int> dict)
    {
        //serialize dictionary base64 string
        StringBuilder sb = new StringBuilder();

        foreach (var kvp in dict)
        {
            sb.AppendFormat("{0}:{1},", kvp.Key, kvp.Value);
        }

        // Remove the trailing comma
        if (sb.Length > 0)
        {
            sb.Length--;
        }

        byte[] bytes = Encoding.UTF8.GetBytes( sb.ToString() );
        return System.Convert.ToBase64String(bytes);
    }

    public static Dictionary<int, int> DeserializeDictionary(string base64)
    {
        //convert base64 string to dictionary
        byte[] bytes = System.Convert.FromBase64String(base64);
        string s = Encoding.UTF8.GetString(bytes);

        Dictionary<int, int> dictionary = new Dictionary<int, int>();

        string[] pairs = s.Split(',');

        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split(':');
            if (keyValue.Length == 2)
            {
                int key, value;
                if (int.TryParse(keyValue[0], out key) && int.TryParse(keyValue[1], out value))
                {
                    dictionary.Add(key, value);
                }
            }
        }

        return dictionary;
    }

    public static string SerializeBoolArray(bool[] array) {
        //serialize bool array to base64 string
        byte[] bytes = new byte[(array.Length + 7) / 8];
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i])
            {
                bytes[i / 8] |= (byte)(1 << (7 - (i % 8)));
            }
        }

        return System.Convert.ToBase64String(bytes);
    }

    public static bool[] DeserializeBoolArray(string base64) {                
        //convert base64 string to bool array
        byte[] bytes = System.Convert.FromBase64String(base64);
        bool[] boolArray = new bool[bytes.Length * 8];

        for (int i = 0; i < boolArray.Length; i++)
        {
            boolArray[i] = (bytes[i / 8] & (1 << (7 - (i % 8)))) != 0;
        }

        return boolArray;
    }
}
