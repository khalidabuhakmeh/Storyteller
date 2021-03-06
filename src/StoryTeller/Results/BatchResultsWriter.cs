﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Baseline;
using StoryTeller.Engine;
using StoryTeller.Remotes.Messaging;
using StoryTeller.Util;

namespace StoryTeller.Results
{
    public static class BatchResultsWriter
    {
        public static HtmlDocument BuildResults(BatchRunResponse results)
        {
            var document = new HtmlDocument
            {
                Title = "Storyteller Batch Results for {0}: {1}".ToFormat(results.system, results.suite)
            };

            WriteCSS(document);
            writeJavascript(results, document);

            return document;
        }


        [Obsolete("Trying to make this go away")]
        public static void WriteCSS(HtmlDocument document)
        {
            var styleTag = StyleTag();

            document.Head.Append(styleTag);
        }

        public static HtmlTag StyleTag()
        {
            var css = readFile("Storyteller.bootstrap.min.css") + "\n\n" + readFile("StoryTeller.storyteller.css");
            css += "\n\n" + readFile("StoryTeller.fixed-data-table.min.css");

            return new HtmlTag("style").Text(css).Encoded(false);
        }


        private static string readFile(string name)
        {
            var assembly = typeof(BatchResultsWriter).GetTypeInfo().Assembly;
            var names = assembly.GetManifestResourceNames();

            var actualName = names.FirstOrDefault(x => x.EqualsIgnoreCase(name));


            var stream = assembly.GetManifestResourceStream(actualName);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static void writeJavascript(BatchRunResponse results, HtmlDocument document)
        {
            var cleanJson = JsonSerialization.ToCleanJson(results);

            document.Body.Add("div").Hide().Id("batch-data").Text(cleanJson);
            document.Body.Add("div").Id("main");

            var js = readFile("StoryTeller.batch-bundle.js");

            document.Body.Add("script").Attr("language", "javascript").Text("\n\n" + js + "\n\n").Encoded(false);


        }
    }
}