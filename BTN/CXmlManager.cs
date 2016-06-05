using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BTN
{
    public struct XMLPACKET
    {
        public string userName;
        public string userSession;
        public PROTOCOL protocol;
        public int dataCount;
        public List<string> datas;
    }

    public class CXmlManager
    {
        private XmlDocument document;
        private XmlElement root;
        private XmlElement dataNode;
        private int pushCount;

        public CXmlManager()
        {
            document = new XmlDocument();
            root = document.CreateElement("PACKET");
            document.AppendChild(root);
            dataNode = this.document.CreateElement("DATAS");
            root.AppendChild(dataNode);
            pushCount = 0;
        }

        private void SetProtocol(PROTOCOL protocol)
        {
            root.SetAttribute("PROTOCOL", ((int)protocol).ToString());
        }

        private void SetDataCount(int count)
        {
            root.SetAttribute("DATA_COUNT", (count).ToString());
        }

        private void SetUserName(string user_name)
        {
            root.SetAttribute("USER_NAME", user_name);
        }

        private void SetUserSession(string user_session)
        {
            root.SetAttribute("USER_SESSION", user_session);
        }

        private void PushData(string data)
        {
            XmlElement node = this.document.CreateElement("DATA");
            node.SetAttribute("DOT", data);
            dataNode.AppendChild(node);
        }

        public void XML_FORM(string user_name, string user_session, PROTOCOL protocol, List<string> datas)
        {
            if (datas.Count == 0)
            {
                return;
            }

            this.SetUserName(user_name);
            this.SetUserSession(user_session);
            this.SetProtocol(protocol);
            this.SetDataCount(datas.Count);
            foreach (string o in datas)
            {
                this.PushData(o);
                this.pushCount++;
            }
        }

        public string ByString()
        {
            return document.InnerXml;
        }

        public void ByFile(string filePath, string fileName)
        {
            document.Save(filePath + "/" + fileName + ".xml");
        }

        public static XMLPACKET ParseFromXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            List<string> datas = new List<string>();
            XmlElement xml_packet = doc.DocumentElement;
            string user_name = xml_packet.GetAttribute("USER_NAME");
            string user_session = xml_packet.GetAttribute("USER_SESSION");
            PROTOCOL protocol = (PROTOCOL)int.Parse(xml_packet.GetAttribute("PROTOCOL"));
            int data_count = int.Parse(xml_packet.GetAttribute("DATA_COUNT"));

            foreach (XmlNode xml_data in xml_packet.SelectSingleNode("DATAS").ChildNodes)
            {
                datas.Add(xml_data.Attributes.Item(0).InnerText);
            }

            XMLPACKET v = new XMLPACKET();
            v.userName = user_name;
            v.userSession = user_session;
            v.protocol = protocol;
            v.dataCount = data_count;
            v.datas = datas;

            return v;
        }
    }
}
