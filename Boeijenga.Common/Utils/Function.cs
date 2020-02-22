using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Utils
{
    public class Function
    {
        public static string GetCommaSepartedList(Int32[] orderNrs)
        {
            string list = string.Empty;
            foreach (Int32 ordernr in orderNrs)
            {
                list += ordernr + ",";
            }
            return list.TrimEnd(',');
        }

        /// <summary>
        /// This routine Replace the text 
        /// by current date if the type is 0(date) otherwise the text
        /// and by 0 if the type is numeric
        /// </summary>
        /// <param name="text">the value which should be checked</param>
        /// <param name="type">Type of the text</param>
        /// <returns>changed value</returns>
        public static string HandleNull(string text, int type)
        {
            switch (type)
            {
                case 0:  //for date value
                    return text == "" ? String.Format("{0:dd-MM-yyyy}", DateTime.Now) : text;
                default: //for numeric value
                    return text == "" ? "0" : text;
            }
        }
        /// <summary>
        /// This routine Replace the text 
        /// by Field if the type is string else
        /// by the text which is sent
        /// </summary>
        /// <param name="text">the value which should be checked</param>
        /// <returns>changed value</returns>
        public static string HandleNull(string text)
        {
            return text == "" ? "" : text;
        }
    }
}
