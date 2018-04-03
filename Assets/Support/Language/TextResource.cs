using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

using Assets.Model;
using Assets.Support;

namespace Assets.Support.Language
{
    public class TextResource
    {
        private static TextResource _instance;

        private Setting _setting;

        private XmlDocument _xmlDocument;

        public TextResource()
        {
            _setting = Setting.GetInstance();
            _xmlDocument = new XmlDocument();
            _xmlDocument.LoadXml(
                (Resources.Load("Text") as TextAsset).text
            );
        }

        public static TextResource GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TextResource();
            }

            return _instance;
        }

        /// <summary>
        /// 설정된 언어값과 텍스트 번호를 기반으로 Xml로부터 Text 리소스를 불러옴.
        /// </summary>
        /// <param name="code">찾을 텍스트 번호</param>
        /// <returns></returns>
        public string GetText(TextCode code)
        {
            var nodeList = _xmlDocument.SelectNodes("resources/string");
            var text = "";
            var domain = _setting.Country.ToDomain();
            var _code = Enum.GetName(typeof(TextCode), code);

            foreach (var node in nodeList)
            {
                var _node = node as XmlElement;

                if (_node.GetAttribute("name") == _code)
                {
                    text = _node.SelectSingleNode(domain).InnerText;
                    break;
                }
            }

            return text;
        }
    }
}
