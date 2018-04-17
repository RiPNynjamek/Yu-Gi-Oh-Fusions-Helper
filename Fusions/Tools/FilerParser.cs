using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Fusions.Tools
{
    public class FileParser
    {
        private Dictionary<string, string[]> _content;

        public Dictionary<string, string[]> Content { get => _content; set => _content = value; }

        public FileParser() => Content = new Dictionary<string, string[]>();

        public Dictionary<string, string[]> GetFileContent(string filePath)
        {
            StreamReader reader = File.OpenText(filePath);
            var output = new Dictionary<string, string[]>();
            var headersList = new List<string>();
            string line;
            List<string> items = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                items.Add(line);
            }

            var _headers = GetHeaders(items);
            foreach (var item in _headers)
            {
                headersList.Add(item);
            }

            var _contents = GetContents(items, _headers);
            for (int i = 0; i < headersList.Count; i++)
            {
                output.Add(headersList[i], _contents[i].ToArray());
            }

            return output;
        }

        private List<List<string>> GetContents(List<string> items, List<string> headers)
        {
            var Contents = new List<List<string>>();

            var headerCounter = 0;
            var subLineCounter = 0;
            bool isFirstOccurence = true;
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].Contains(headers[headerCounter]))
                {
                    if (isFirstOccurence)
                    {
                        isFirstOccurence = false;
                        continue;
                    }
                    var subContent = new List<string>();
                    subLineCounter = i;
                    subContent = ExtractFromHeader(items, headers, headerCounter, subLineCounter, subContent);
                    Contents.Add(subContent);
                    i = subLineCounter - 1;
                    if (headerCounter >= headers.Count - 1)
                    {
                        return Contents;
                    }
                    headerCounter++;
                }
            }
            Contents.Last().Add(items.Last());
            return Contents;
        }

        private static List<string> ExtractFromHeader(List<string> items, List<string> headers, int headerCounter, int subLineCounter, List<string> subContent)
        {
            string head;
            if (headerCounter < headers.Count - 1)
            {
                head = headers[headerCounter + 1];
            }
            else
            {
                head = items.Last();
            }

            while (!(items[subLineCounter].Contains(head)))
            {
                subContent.Add(items[subLineCounter]);
                subLineCounter++;
            }
            return subContent;
        }

        private List<string> GetHeaders(List<string> items)
        {
            var Headers = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                var line = items[i];
                if (line.StartsWith("===") && items[i + 1] != "")
                {
                    Headers.Add(items[i + 1]);
                }
                
            }
            Headers.RemoveAt(0);
            for (int j = 0; j < Headers.Count; j++)
            {
                Headers[j] = Headers[j].Substring(Headers[j].IndexOf('.') + 1);
            }
            return Headers;
        }
    }
}
