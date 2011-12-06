using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigIt.DesertRace.Entities
{
    public static class Util
    {
        [System.Diagnostics.DebuggerStepThrough()]
        public static string[] SeparedParams(string parameters)
        {
            return parameters.Split(' ');
        }

        public static void RaiseError(string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            var ex = new GameException(null, formattedMessage);
            throw ex;
        }

        /// <summary>
        /// Based on the available values in an Enum type, generate a dictionary
        /// where the key is the the first letter of each name and the value is the enum value
        /// </summary>
        /// <typeparam name="TEnum">The source of the values to abbreviate</typeparam>
        /// <remarks>Just Enum type with names that begins with a different letter are supported. Otherwise
        /// it is expected an Exception</remarks>
        public static Dictionary<char, TEnum> GetEnumAbreviations<TEnum>()
        {
            string[] names = Enum.GetNames(typeof(TEnum));
            Array values = Enum.GetValues(typeof(TEnum));
            int length = values.Length;
            var result = new Dictionary<char, TEnum>(length);
            for (int i = length - 1; i >= 0; i--)
                result.Add(names[i][0], (TEnum)values.GetValue(i));
            return result;
        }

        /// <summary>
        /// Convert the abbreviations obtained in string to the corresponding list of
        /// values that 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static List<TEnum> TranslateCharToEnum<TEnum>(string sequence)
        {
            if (sequence == null)
                // Returns just an empty list
                return new List<TEnum>();

            Dictionary<char, TEnum> abreviations = GetEnumAbreviations<TEnum>();
            List<TEnum> result = new List<TEnum>(sequence.Length);
            foreach (char ch in sequence)
            {
                TEnum enumValue;
                if (abreviations.TryGetValue(ch, out enumValue))
                    result.Add(enumValue);
                else
                    RaiseError(Messages.InvalidEnumAbreviation, ch, typeof(TEnum).Name);
            }
            return result;
        }
    }
}
