using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstParser
{
    public class TextSearcher
    {
        private string myText;
        private int position;

        public TextSearcher(string text)
        {
            myText = text;
        }

        public void GoTo(string text)
        { 
            int p = myText.IndexOf(text, position);
            if (p > -1)
                position = p;
        }

        public bool Skip(string text)
        {
            int p = myText.IndexOf(text, position);
            if (p > -1)
            {
                position = p + text.Length;
                return true;
            }
            return false;
        }

        public string ReadTo(string text)
        {
            int p = myText.IndexOf(text, position);
            string result = "";
            if (p > -1)
            {
                result = myText.Substring(position, p - position);
                position = p;
            }
            return result;
        }

        public string Crop(string start, string finish)
        {
            int s = myText.IndexOf(start, 0);
            int f = myText.IndexOf(finish, 0);
            if (s > -1 && f > -1)
            myText = myText.Substring(s, f-s);
            return myText;
        }
    }
}
