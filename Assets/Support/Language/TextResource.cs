using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

using Assets.Model;
using Assets.Model.ChessSkill;
using Assets.Model.SkillChessPiece;
using Assets.Support;

namespace Assets.Support.Language
{
    public class TextResource
    {
        private static TextResource _instance;

        private Setting _setting;

        private XmlDocument _xmlDocument;

        private TextFile _textFile;

        public TextResource()
        {
            _setting = Setting.GetInstance();
            _xmlDocument = new XmlDocument();
            _xmlDocument.LoadXml(
                (Resources.Load("Text") as TextAsset).text
            );
            _textFile = TextFile.Text;
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
            if (_textFile != TextFile.Text)
            {
                _xmlDocument.LoadXml(
                    (Resources.Load("Text") as TextAsset).text
                );
            }

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

        /// <summary>
        /// 설정된 언어값과 기물 객체를 기반으로 Xml로부터 Display name을 불러옴.
        /// </summary>
        /// <param name="piece">이름을 찾을 기물</param>
        /// <returns></returns>
        public string GetClassName(SkillPiece piece)
        {
            if (_textFile != TextFile.Class)
            {
                _xmlDocument.LoadXml(
                    (Resources.Load("Class") as TextAsset).text
                );
            }

            var nodeList = _xmlDocument.SelectNodes("resources/code");
            var text = "";
            var domain = _setting.Country.ToDomain();
            var classCode = piece.ClassCode + "";

            foreach (var node in nodeList)
            {
                var _node = node as XmlElement;

                if (_node.GetAttribute("name") == classCode)
                {
                    foreach (var nameAndExplain in _node.SelectNodes("string"))
                    {
                        var nameAndExplainNode = nameAndExplain as XmlElement;

                        // 기물의 이름 검출
                        if (nameAndExplainNode.GetAttribute("name") == "name")
                        {
                            text = nameAndExplainNode.SelectSingleNode(domain).InnerText;
                            break;
                        }
                    }
                }
            }


            return text;
        }

        /// <summary>
        /// 설정된 언어값과 기술 객체를 기반으로 Xml로부터 Display name과 Display explain을 불러옴.
        /// </summary>
        /// <param name="skill">이름과 설명을 찾을 기술</param>
        /// <returns></returns>
        public Dictionary<string, string> GetSkillInfo(Skill skill)
        {
            if (_textFile != TextFile.Class)
            {
                _xmlDocument.LoadXml(
                    (Resources.Load("Class") as TextAsset).text
                );
            }

            var nodeList = _xmlDocument.SelectNodes("resources/code/skill-array/skill");
            var pair = new Dictionary<string, string>();
            var domain = _setting.Country.ToDomain();
            var skillCode = skill.Code + "";

            foreach (var node in nodeList)
            {
                var _node = node as XmlElement;

                if (_node.GetAttribute("name") == skillCode)
                {
                    foreach (var nameAndExplain in _node.SelectNodes("string"))
                    {
                        var nameAndExplainNode = nameAndExplain as XmlElement;

                        // 기술의 이름 검출
                        if (nameAndExplainNode.GetAttribute("name") == "name")
                        {
                            pair.Add("Name", nameAndExplainNode.SelectSingleNode(domain).InnerText);
                        }

                        // 기술의 설명 검출
                        if (nameAndExplainNode.GetAttribute("name") == "explain")
                        {
                            pair.Add("Explain", nameAndExplainNode.SelectSingleNode(domain).InnerText);
                        }
                    }
                }
            }
            
            return pair;
        }

        private enum TextFile
        {
            Text,
            Class
        } 
    }
}
