using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DLMS
{
    public static class RegexExtensions
    {
        public const string RegExWordSelection = @"(?<words>\w+)";

        public static string Replace(this string input, Regex regex, string groupName, string replacement)
        {
            return regex.Replace(input, m =>
            {
                return ReplaceNamedGroup(input, groupName, replacement, m);
            });
        }

        private static string ReplaceNamedGroup(string input, string groupName, string replacement, Match m)
        {
            string capture = m.Value;
            capture = capture.Remove(m.Groups[groupName].Index - m.Index, m.Groups[groupName].Length);
            capture = capture.Insert(m.Groups[groupName].Index - m.Index, replacement);
            return capture;
        }

        public static List<String> GetWordsList(string txtVal)
        {
            List<String> wordList = new List<string>();
            try
            {
                if (!String.IsNullOrEmpty(txtVal) &&
                    !String.IsNullOrWhiteSpace(txtVal))
                {
                    Regex wordSelectionRegEx = new Regex(RegExWordSelection, RegexOptions.IgnoreCase);
                    var collections = wordSelectionRegEx.Matches(txtVal);
                    foreach (Match wdCol in collections)
                    {
                        if (wdCol.Success)
                            wordList.Add(wdCol.Value);
                    }
                }
            }
            catch { }
            return wordList;
        }
    }
}
