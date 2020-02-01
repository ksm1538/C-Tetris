using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tetris
{
    class DataManager
    {
        public static List<Rank> Ranks = new List<Rank>();

        static DataManager()
        {
            Load();
        }

        public static void Load()
        {
            try
            {
                string ranksOutput = File.ReadAllText(@"./Ranks.xml");
                XElement ranksXElement = XElement.Parse(ranksOutput);
                Ranks = (from item in ranksXElement.Descendants("rank")
                         select new Rank()
                         {
                             name = item.Element("name").Value,
                             score = int.Parse(item.Element("score").Value)
                         }).ToList<Rank>();
            }
            catch (FileLoadException exception)
            {
                Save();
            }
        }

        public static void Save()
        {
            string ranksOutput = "";
            ranksOutput += "<ranks>\n";
            foreach (var item in Ranks)
            {
                ranksOutput += "<rank>\n";
                ranksOutput += "  <name>" + item.name + "</name>\n";
                ranksOutput += "  <score>" + item.score + "</score>\n";
                ranksOutput += "</rank>\n";
            }
            ranksOutput += "</ranks>";


            File.WriteAllText(@"./Ranks.xml", ranksOutput);
        }
    }
}
