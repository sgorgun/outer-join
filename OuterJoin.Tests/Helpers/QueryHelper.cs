﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutocodeDB.Helpers
{
    internal static class QueryHelper
    {
        private const string BlockComments = @"/\*.*\*/";//@"/\*(.*?)\*/";
        private const string LineComments = @"--(.*$?)";
        public static string GetQuery(string file)
        {
            if (!File.Exists(file))
                return null;
            var rawData = File.ReadAllText(file);
            if (rawData.Length == 0)
                return string.Empty;
            rawData = RemoveComments(rawData);
            return rawData;
        }

        public static string[] GetQueries(string file)
        {
            var rawData = File.ReadAllText(file);
            rawData = RemoveComments(rawData);
            return ParseQueries(rawData);
        }

        public static string[] GetQueries(string[] files)
        {
            StreamWriter writer = new StreamWriter("log.txt", true);
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = GetQuery(files[i]);
            }
            return files;
        }

        public static string ComposeErrorMessage(string query, Exception ex = null, string message = null)
        {
            var exMessage = ex == null ? "" : $"\nException message: {ex.Message}";
            return $"{message}{exMessage}\nQuery with error: {query}";
        }

        public static string ComposeErrorMessage(string query, string message = null)
        {
            return ComposeErrorMessage(query, null, message);
        }

        public static string RemoveComments(string rawData)
        {
            rawData = Regex.Replace(rawData, LineComments, "", RegexOptions.Multiline);
            rawData = Regex.Replace(rawData, BlockComments, "", RegexOptions.Singleline);
            rawData = Regex.Replace(rawData, @"\s+", " ");
            return rawData;
        }

        private static string[] ParseQueries(string rawData) =>
            rawData.Split(";", StringSplitOptions.RemoveEmptyEntries)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
    }
}
