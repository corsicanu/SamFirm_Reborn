using System;
using System.IO;
using System.Xml.Linq;

namespace hadesFirm
{
  internal class Settings
  {
    private const string SettingFile = "hadesFirm.xml";

    public static string ReadSetting(string element)
    {
      try
      {
        if (!File.Exists("hadesFirm.xml"))
          Settings.GenerateSettings();
        return XDocument.Load("hadesFirm.xml").Element((XName) "hadesFirm").Element((XName) element).Value;
      }
      catch (Exception ex)
      {
        Logger.WriteLog("Error reading config file: " + ex.Message, false);
        return string.Empty;
      }
    }

    public static void SetSetting(string element, string value)
    {
      if (!File.Exists("hadesFirm.xml"))
        Settings.GenerateSettings();
      XDocument xdocument = XDocument.Load("hadesFirm.xml");
      XElement xelement = xdocument.Element((XName) "hadesFirm").Element((XName) element);
      if (xelement == null)
        xdocument.Element((XName) "hadesFirm").Add((object) new XElement((XName) element, (object) value));
      else
        xelement.Value = value;
      xdocument.Save("hadesFirm.xml");
    }

    private static void GenerateSettings()
    {
      File.WriteAllText("hadesFirm.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<hadesFirm>\r\n    <SaveFileDialog></SaveFileDialog>\r\n    <AutoInfo></AutoInfo>\r\n\t<Region></Region>\r\n\t<Model></Model>\r\n\t<PDAVer></PDAVer>\r\n\t<CSCVer></CSCVer>\r\n\t<PHONEVer></PHONEVer>\r\n    <BinaryNature></BinaryNature>\r\n    <CheckCRC></CheckCRC>\r\n    <AutoDecrypt></AutoDecrypt>\r\n</hadesFirm>");
    }
  }
}
