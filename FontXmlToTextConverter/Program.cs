using System;
using System.IO;
using System.Xml;

namespace FontXmlToTextConverter {

	class Program {
		static void Main(string[] args) {
			if (args.Length < 1) {
				Console.WriteLine("Usage: xml2bmfont font1.fnt [font2.fnt ...]");
				Console.WriteLine("Output: font1.fnt.conv font2.fnt.conv ...");
				Console.WriteLine("Fnt files are converted from xml to text format, which is readable by defold");
				return;
			}

			for (int j = 0; j < args.Length; j++) {
				XmlDocument xmldoc = new XmlDocument();
				string filename = args[j];
				Console.WriteLine("Processing file " + filename);
				FileStream fs = new FileStream(args[j], FileMode.Open, FileAccess.Read);
				xmldoc.Load(fs);
				using (StreamWriter writetext = new StreamWriter(filename + ".conv")) {
					XmlElement xInfo = xmldoc.GetElementsByTagName("info")[0] as XmlElement;
					writetext.WriteLine("info aa={0} size={1} smooth={2} stretchH={3} bold={4} padding={5} spacing={6} charset=\"{7}\" italic={8} unicode={9} face=\"{10}\"",
						xInfo.GetAttribute("aa"), xInfo.GetAttribute("size"), xInfo.GetAttribute("smooth"),
						xInfo.GetAttribute("stretchH"), xInfo.GetAttribute("bold"), xInfo.GetAttribute("padding"),
						xInfo.GetAttribute("spacing"), xInfo.GetAttribute("charset"), xInfo.GetAttribute("italic"),
						xInfo.GetAttribute("unicode"), xInfo.GetAttribute("face")
					);

					XmlElement xCommon = xmldoc.GetElementsByTagName("common")[0] as XmlElement;
					writetext.WriteLine("common lineHeight={0} base={1} scaleW={2} scaleH={3} pages={4} packed={5}",
						xCommon.GetAttribute("lineHeight"), xCommon.GetAttribute("base"),
						xCommon.GetAttribute("scaleW"), xCommon.GetAttribute("scaleH"),
						xCommon.GetAttribute("pages"), xCommon.GetAttribute("packed")
					);

					int i;
					XmlNodeList xPage = xmldoc.GetElementsByTagName("page");
					for (i = 0; i <= xPage.Count - 1; i++) {
						XmlElement xElem = xPage[i] as XmlElement;
						if (xElem != null)
							writetext.WriteLine("page id={0} file=\"{1}\"", xElem.GetAttribute("id"), xElem.GetAttribute("file"));
					}

					XmlElement xChars = xmldoc.GetElementsByTagName("chars")[0] as XmlElement;
					writetext.WriteLine("chars count={0}", xChars.GetAttribute("count"));

					XmlNodeList xmlnode = xmldoc.GetElementsByTagName("char");
					for (i = 0; i <= xmlnode.Count - 1; i++) {
						XmlElement xElem = xmlnode[i] as XmlElement;
						if (xElem != null) {
							//xadvance='7' x='114' chnl='0' yoffset='24' y='368' xoffset='0' id='32' page='0' height='0' width='0'
							writetext.WriteLine("char id={0,-5} x={1,-5} y={2,-5} width={3,-5} height={4,-5} xoffset={5,-5} yoffset={6,-5} xadvance={7,-5} page={8,-5} chnl={9,-5}",
								xElem.GetAttribute("id"), xElem.GetAttribute("x"), xElem.GetAttribute("y"),
								xElem.GetAttribute("width"), xElem.GetAttribute("height"),
								xElem.GetAttribute("xoffset"), xElem.GetAttribute("yoffset"),
								xElem.GetAttribute("xadvance"), xElem.GetAttribute("page"), xElem.GetAttribute("chnl")
							);
						}
					}
				}
			}
		}
	}

}
