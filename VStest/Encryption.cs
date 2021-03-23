using System;

namespace VStest
{
    class Encryption
    {
        public static string Encode(string str, int railsNum)
        {
            if (!nullOrEmpty(str))
            {
                if (checkRailsNum(railsNum))
                {
                    string encodedStr = "";

                    string[] substrings = new string[railsNum];

                    int l = 0;
                    while (l != str.Length) {
                        for (int i = 0; i < railsNum; i++)
                        {
                            substrings[i] += str[l];
                            l++;
                            if (l == str.Length)
                                break;
                        }
                        if (l == str.Length)
                            break;
                        for (int i = railsNum-2; i > 0; i--)
                        {
                            substrings[i] += str[l];
                            l++;
                            if (l == str.Length)
                                break;
                        }
                    }
                    foreach(var s in substrings)
                    {
                        encodedStr += s;
                    }
                    return encodedStr;
                }
                throw new Exception("Rails must have greater length");
            }
            return "";
        }
        public static string Decode(string str, int railsNum)
        {
            if (!nullOrEmpty(str))
            {
                if (checkRailsNum(railsNum))
                {
                    string decodedStr = "";

                    int[] substringsLength = new int[railsNum];

                    int l = 0;
                    while (l != str.Length)
                    {
                        for (int i = 0; i < railsNum; i++)
                        {
                            substringsLength[i] ++;
                            l++;
                            if (l == str.Length)
                                break;
                        }
                        if (l == str.Length)
                            break;
                        for (int i = railsNum - 2; i > 0; i--)
                        {
                            substringsLength[i] ++;
                            l++;
                            if (l == str.Length)
                                break;
                        }
                    }
                    int startIndex = 0;

                    string[] substrings = new string[railsNum];
                    for (int i =0; i < railsNum; i++)
                    {
                        substrings[i] = str.Substring(startIndex, substringsLength[i]);
                        startIndex = substringsLength[i]+startIndex;
                    }

                    l = 0;
                    int[] indCounters = new int[railsNum];
                    int j = 0;
                    while (l != str.Length)
                    {
                        for (int i = 0; i < railsNum; i++)
                        {
                            decodedStr += substrings[i][indCounters[i]];
                            indCounters[i]++;
                            l++;
                            if (l == str.Length)
                                break;
                        }
                        j++;
                        if (l == str.Length)
                            break;
                        for (int i = railsNum - 2; i > 0; i--)
                        {
                            decodedStr += substrings[i][indCounters[i]];
                            indCounters[i]++;
                            l++;
                            if (l == str.Length)
                                break;
                        }
                        j++;
                    }

                    return decodedStr;
                }
                throw new Exception("Rails must have greater length");
            }
            return "";
        }
        private static bool checkRailsNum(int railsNum)
        {
            if (railsNum < 2)
                return false;
            return true;
        }
        private static bool nullOrEmpty(string str)
        {
            if (String.IsNullOrEmpty(str)) { return true; }
            return false;
        }
    }
}
