using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace Sonatiina
{
    internal static class FeedService
    {
        internal static List<Ravintola> GetRavintolat(string[] url, bool daily)
        {
            List<Ravintola> ravintolaList = new List<Ravintola>();

            for (int i2 = 0; i2 < url.Length; i2++)
            {
                try
                {
                    WebRequest webRequest = WebRequest.Create(url[i2]);
                    WebResponse webResponse = webRequest.GetResponse();

                    Stream stream = webResponse.GetResponseStream();
                    XmlDocument xmlDocument = new XmlDocument();

                    xmlDocument.Load(stream);

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
                    nsmgr.AddNamespace("dc", xmlDocument.DocumentElement.GetNamespaceOfPrefix("dc"));
                    nsmgr.AddNamespace("content", xmlDocument.DocumentElement.GetNamespaceOfPrefix("content"));

                    if (daily == true)
                    {
                        XmlNodeList itemNodes = xmlDocument.SelectNodes("rss");
                        for (int i = 0; i < itemNodes.Count; i++)
                        {
                            Ravintola ravintola = new Ravintola();

                            if (itemNodes[i].SelectSingleNode("channel/item/title") != null)
                            {
                                String nimi = itemNodes[i].SelectSingleNode("channel/title").InnerText;
                                ravintola.Nimi = nimi.Replace("Ravintola", "");
                            }

                            if (itemNodes[i].SelectSingleNode("channel/item/description") != null)
                            {
                                ravintola.Ruokalista = itemNodes[i].SelectSingleNode("channel/item/description").InnerText;
                            }

                            ravintolaList.Add(ravintola);
                        }
                    }

                    if (daily == false)
                    {
                        XmlNodeList itemNodes = xmlDocument.SelectNodes("rss/channel/item");
                        for (int i = 0; i < itemNodes.Count; i++)
                        {
                            Ravintola ravintola = new Ravintola();

                            if (itemNodes[i].SelectSingleNode("title") != null)
                            {
                                ravintola.Nimi = itemNodes[i].SelectSingleNode("title").InnerText;
                            }

                            if (itemNodes[i].SelectSingleNode("description") != null)
                            {
                                ravintola.Ruokalista = itemNodes[i].SelectSingleNode("description").InnerText;
                            }

                            ravintolaList.Add(ravintola);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return ravintolaList;
        }
    }
}