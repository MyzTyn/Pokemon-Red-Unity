// Decompiled with JetBrains decompiler
// Type: PokemonUnity.Localization.XmlStringRes
// Assembly: PokemonUnity.Shared, Version=16.7.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16E3BA-00FE-4D28-8FA0-7EDDE6B25176
// Assembly location: PokemonUnity.Shared.dll

using PokemonUnity.Utility;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using PokemonUnity;

// ToDo: Remove this class because for now, the Game.LocalizationDictionary is not working properly
public class TempLocalizationXML
{
  public IDictionary<string, string> stringMap;
  public static TempLocalizationXML instance { get; private set; } = new TempLocalizationXML();
  
  private string nodeElement;
  private string nodeAttribute;

  public TempLocalizationXML(string element = "STR", string attribute = "id")
  {
    this.stringMap = (IDictionary<string, string>)new Dictionary<string, string>();
    this.nodeElement = element;
    this.nodeAttribute = attribute;
  }

  public bool Initialize(string fileName, int languageId = 0)
  {
    if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
      return false;
    Core.Logger?.Log("Load XML to memory : {0} ({1})", (object)fileName, (object)languageId);
    string xml = File.ReadAllText(fileName);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.LoadXml(xml);
    Core.Logger?.Log("SUCCESS");
    List<string> source = new List<string>();
    XmlNodeList childNodes = xmlDocument.ChildNodes;
    if (childNodes != null)
    {
      foreach (XmlNode xmlNode in childNodes)
      {
        if (xmlNode.HasChildNodes)
        {
          foreach (XmlNode node in xmlNode)
          {
            if ((this.nodeElement == node.LocalName.ToString() || "MSG" == node.LocalName.ToString() ||
                 "MESSAGE" == node.LocalName.ToString()) && node.HasChildNodes &&
                node.FirstChild.NodeType == XmlNodeType.Text)
            {
              string attributeValueOrNull = node.GetAttributeValueOrNull(this.nodeAttribute);
              if (!string.IsNullOrEmpty(attributeValueOrNull))
              {
                if (this.stringMap.ContainsKey(attributeValueOrNull))
                  source.Add(attributeValueOrNull);
                this.stringMap.Add(attributeValueOrNull, node.InnerText.TrimStart('\r', '\n'));
              }
            }
          }
        }
      }
    }

    if (source.Count > 0)
      Core.Logger.LogWarning("A dictionary can not contain same key twice. There are some duplicated names: " +
                             source.JoinAsString(", "));
    return true;
  }

  public void Release() => this.stringMap.Clear();

  public string GetStr(string code)
  {
    if (this.stringMap.ContainsKey(code))
      return this.stringMap[code];
    Core.Logger?.Log("Identifier `{0}` was not found in Localization dictionary", (object)code);
    return code;
  }
}