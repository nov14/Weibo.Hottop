using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
namespace HotTop
{
    class Program
    {
        static void Main(string[] args)
        {
            UpdateAsync().Wait();
        }

        public static async Task UpdateAsync()
        {
            var dataSource = GetHotTops();
            while(true)
            {
                var newData = GetHotTops();
                if(Enumerable.SequenceEqual(dataSource, newData))
                {
                    Console.WriteLine("Unchanged!");
                }
                else
                {
                    var data = newData.Except(dataSource);
                    foreach(var items in data)
                    {
                        Console.WriteLine(items);
                    }
                    var newSource = dataSource.Union(newData).ToList();
                    dataSource = newSource;
                }
                await Task.Delay(10000);
            }
        }

        public static List<string> GetHotTops()
        {
            string html = @"https://s.weibo.com/top/summary?Refer=top_hot";
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.UTF8;
            HtmlDocument doc = web.Load(html);

            HtmlNodeCollection hottopNodes = doc.DocumentNode.SelectNodes(
                "//tbody//td[@class='td-02']/a");
            
            var hottops = new List<string>();

            foreach(var item in hottopNodes)
            {
                string href = $"{"https://s.weibo.com"}{item.Attributes["href"].Value}";
                
                string hottop = item.InnerText;

                hottops.Add($"{hottop} - {href}");
            }

            return hottops;
        }
    }
}
