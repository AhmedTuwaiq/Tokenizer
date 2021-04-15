using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    enum TokenType { ID, NUMBER, EQUAL, NONE}
    class TokenReader
    {
        public static List<string> getHashTag(string value)
        {
            List<string> result = new();
            string currentHashTag = "";

            for(int i = 0; i < value.Length; i++)
            {
                hashtagProcessing:
                if(value[i].Equals('#'))
                {
                    while(i < value.Length)
                    {
                        if (value[i].Equals(' '))
                        {
                            if (currentHashTag.Length > 1)
                            {
                                result.Add(currentHashTag);
                            }

                            currentHashTag = "";
                            break;
                        }
                        else if(value[i].Equals('#') && currentHashTag.Length > 0)
                        {
                            if(currentHashTag.Length > 1)
                            {
                                result.Add(currentHashTag);
                            }

                            currentHashTag = "";
                            goto hashtagProcessing;
                        }

                        currentHashTag += value[i];
                        i++;
                    }
                }
            }

            if(currentHashTag.Length > 1)
            {
                result.Add(currentHashTag);
            }

            return result;
        }

        public static List<string> getMention(string value)
        {
            List<string> result = new();

            string currentMention = "";

            for (int i = 0; i < value.Length; i++)
            {
                mentionProcessing:
                if (value[i].Equals('@'))
                {
                    while (i < value.Length)
                    {
                        if (value[i].Equals(' '))
                        {
                            if (currentMention.Length > 1)
                            {
                                result.Add(currentMention);
                            }

                            currentMention = "";
                            break;
                        }
                        else if (value[i].Equals('@') && currentMention.Length > 0)
                        {
                            if (currentMention.Length > 1)
                            {
                                result.Add(currentMention);
                            }

                            currentMention = "";
                            goto mentionProcessing;
                        }

                        currentMention += value[i];
                        i++;
                    }
                }
            }

            if (currentMention.Length > 1)
            {
                result.Add(currentMention);
            }

            return filterMention(result);
        }

        public static List<string> filterMention(List<string> list)
        {
            List<string> result = new();

            foreach(string value in list)
                if (isLegal(value))
                    result.Add(value);

            return result;
        }

        public static bool isLegal(string value)
        {
            if (value.Length > 15)
                return false;

            for(int i = 1; i < value.Length; i++)
                if (!char.IsLetterOrDigit(value[i]) && !value[i].Equals('_'))
                    return false;

            return true;
        }

        public static List<string> readHexColors(string value)
        {
            List<string> hexColors = new();
            string currentHash = "";
            bool foundHash = false;
            // extract hashtags, and mentions
            // print the count of hashtags and mentions
            // write a list of use cases of tokenizer

            foreach(char c in value)
            {
                if(foundHash)
                {
                    if(!isHex(c))
                    {
                        if((c.Equals(' ') || c.Equals('#')) && currentHash.Length <= 7)
                        {
                            hexColors.Add(currentHash);
                        }

                        foundHash = false;
                        currentHash = "";
                    }
                    else
                    {
                        currentHash += c;
                    }
                } else if(c.Equals('#'))
                {
                    foundHash = true;
                    currentHash = "#";
                }
            }

            if(currentHash.Length > 0 && currentHash.Length < 8)
            {
                hexColors.Add(currentHash);
            }

            return hexColors;
        }

        public static bool isHex(char c)
        {
            if (char.IsDigit(c))
                return true;

            switch(c)
            {
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                    return true;
            }

            return false;
        }
    }
}
