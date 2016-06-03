using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BTN
{
    public class CXmlManager
    {
        private XmlDocument document;
        private XmlElement root;
        private XmlElement dataNode;
        public CXmlManager(string title)
        {
            document = new XmlDataDocument();
            root = document.CreateElement(title);
            document.AppendChild(root);
            dataNode = this.document.CreateElement("DATAS");
            root.AppendChild(dataNode);
        }

        public void SetProtocol(PROTOCOL protocol)
        {
            XmlElement node = this.document.CreateElement("PROTOCOL");
            node.SetAttribute("contents", ((int)protocol).ToString());
            root.AppendChild(node);
        }

        public void SetDataCount(int count)
        {
            XmlElement node = this.document.CreateElement("DATA_COUNT");
            node.SetAttribute("contents", count.ToString());
            root.AppendChild(node);
        }

        public void PushData(string data)
        {
            XmlElement node = this.document.CreateElement("DATA");
            node.SetAttribute("contents", data);
            dataNode.AppendChild(node);
        }

        public void XML_FORM(PROTOCOL protocol, List<string> datas)
        {
            if (datas.Count == 0)
            {
                return;
            }

            this.SetProtocol(protocol);
            this.SetDataCount(datas.Count);
            foreach(string o in datas)
            {
                PushData(o);
            }
        }

        public string OutPutByString()
        {
            return document.InnerText;
        }

        public void ParseFromXml(string xml)
        {
            PROTOCOL protocol;
            int dataCount;
            List<string> datas = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlElement e_protoocl = doc["PROTOCOL"];
            protocol = (PROTOCOL)int.Parse(e_protoocl.GetAttribute("contents"));

            XmlElement e_data_count = doc["DATA_COUNT"];
            dataCount = int.Parse(e_data_count.GetAttribute("contents"));

            XmlElement e_datas = doc["DATAS"];
            foreach (XmlNode o in e_datas.ChildNodes)
            {
                datas.Add(o.SelectSingleNode("DATA").SelectSingleNode("contents").InnerText);
            }
        }
    }
}
