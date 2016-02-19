using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace RenameMyAss.Class
{
    public class XMLHelper
    {
        public XMLHelper()
        {

        }
        public bool SaveProfileXML(List<ListViewItem> list, string filename)
        {
            try
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.NewLineOnAttributes = false;
                xmlWriterSettings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(filename, xmlWriterSettings))
                {
                    writer.WriteStartDocument();

                    writer.WriteStartElement("Rules");
                    foreach (ListViewItem lvi in list)
                    {
                        writer.WriteStartElement("Rule");
                        writer.WriteAttributeString("Target", string.IsNullOrEmpty(lvi.SubItems[0].Text) ? "" : lvi.SubItems[0].Text);
                        writer.WriteAttributeString("Mode", string.IsNullOrEmpty(lvi.SubItems[1].Text) ? "" : lvi.SubItems[1].Text);
                        writer.WriteAttributeString("Parameter1", string.IsNullOrEmpty(lvi.SubItems[2].Text) ? "" : lvi.SubItems[2].Text);
                        writer.WriteAttributeString("Parameter2", string.IsNullOrEmpty(lvi.SubItems[3].Text) ? "" : lvi.SubItems[3].Text);
                        writer.WriteAttributeString("AuxParameter", (lvi.SubItems.Count > 4) ? (string.IsNullOrEmpty(lvi.SubItems[4].Text) ? "" : lvi.SubItems[4].Text) : "");
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Rules> LoadProfileXML(string filename)
        {
            try
            {
                XDocument xdoc = XDocument.Load(filename);
                List<Rules> rulist =
                (
                from e in xdoc.Root.Descendants("Rule")
                select new Rules
                {
                    Target = (string)e.Attributes("Target").Single().Value,
                    Mode = (string)e.Attributes("Mode").Single().Value,
                    Parameter1 = (string)e.Attributes("Parameter1").Single().Value,
                    Parameter2 = (string)e.Attributes("Parameter2").Single().Value,
                    AuxParameter = (string)e.Attributes("AuxParameter").Single().Value
                }).ToList();
                return rulist;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
