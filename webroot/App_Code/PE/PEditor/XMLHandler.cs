using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;

namespace HIT.PEditor.Core
{
    public class XMLHandler
    {
        public XMLHandler(string path, string fileName)
        {
            XMLFilePath = path;
            FileName = fileName;
        }

        public string FileName
        {
            get;
            set;
        }
        public string XMLFilePath
        {
            get;
            set;
        }
        /// <summary>
        /// XmlElement will be available after calling GetFields Method
        /// </summary>
        public XElement XmlElement
        {
            set;
            get;
        }

        public Dictionary<string, string> GetFields()
        {
            Dictionary<string, string> fieldValuePair = new Dictionary<string, string>();
            string fileURI = string.Concat(XMLFilePath, FileName);
            XmlElement = XElement.Load(fileURI);
            var data = from xmlValues in XmlElement.Descendants("field")
                       select xmlValues;
            foreach (var item in data)
            {
                fieldValuePair.Add(item.Element("name").Value, item.Element("value").Value);
            }
            return fieldValuePair;
        }

        public string CreateXML(Dictionary<string, string> fieldValuePair)
        {
            string sourceFile = string.Concat(XMLFilePath, FileName);
            //XElement XmlElement = XElement.Load(sourceFile);
            XmlDocument xml = new XmlDocument();
            xml.Load(sourceFile);
            XmlNodeList nodeList = xml.SelectNodes("/CAD/field");
            XmlElement root = xml.DocumentElement;
            foreach (XmlNode node in nodeList)
            {
                root.RemoveChild(node);
               
            }
            foreach (var item in fieldValuePair)
            {
                XmlElement field = xml.CreateElement("field");
                XmlElement name = xml.CreateElement("name");
                XmlElement value = xml.CreateElement("value");
                name.InnerText = item.Key;
                value.InnerText = item.Value;
                field.AppendChild(name);
                field.AppendChild(value);

                root.AppendChild(field);

                //var elm = XmlElement.Descendants("name").Single(node => node.Value == item.Key);
                //if (elm != null)
                //{
                //    var valueNode = elm.ElementsAfterSelf("value").Single();
                //    valueNode.SetValue(item.Value);
                //}
            }
            //XmlElement.Save(sourceFile);
            xml.Save(sourceFile);
            return sourceFile;
        }

    }
}