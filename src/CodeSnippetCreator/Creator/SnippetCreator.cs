using CodeSnippetCreator.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeSnippetCreator.Creator
{
    /// <summary>
    /// スニペット作成クラス
    /// </summary>
    public class SnippetCreator
    {
        /// <summary>スニペットXML定義</summary>
        private XmlDocument doc = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SnippetCreator()
        {
            // ルート部分を設定する。
            XmlDocument tmpDoc = new XmlDocument();
            // 宣言を設定する。
            XmlDeclaration declaration = tmpDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            tmpDoc.AppendChild(declaration);

            // ルート要素を設定する。
            XmlElement rootElement = tmpDoc.CreateElement(SnippetElements.CODE_SNIPPETS);
            // xmlns属性を設定する。
            XmlAttribute xmlnsAttribute = tmpDoc.CreateAttribute(SnippetAttributes.XMLNS);
            xmlnsAttribute.Value = SnippetAttributes.XMLNS_CONTENTS;
            rootElement.Attributes.Append(xmlnsAttribute);

            // スニペット要素を設定する。
            XmlElement snippetElement = tmpDoc.CreateElement(SnippetElements.CODE_SNIPPET);
            // Format属性を設定する。
            XmlAttribute formatAttribute = tmpDoc.CreateAttribute(SnippetAttributes.FORMAT);
            formatAttribute.Value = "1.0.0";
            snippetElement.Attributes.Append(formatAttribute);

            rootElement.AppendChild(snippetElement);
            tmpDoc.AppendChild(rootElement);
            this.doc = tmpDoc;
        }

        /// <summary>
        /// ヘッダ要素設定
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="author">管理者</param>
        /// <param name="description">説明</param>
        /// <param name="shortcut">ショートカット</param>
        public void SetHeader(String title, String author, String description, String shortcut)
        {
            XmlElement headerElement = this.doc.CreateElement(SnippetElements.HEADER);
            // タイトルを設定する。
            this.AddChildElement(SnippetElements.TITLE, title, ref headerElement);
            // 管理者を設定する。
            this.AddChildElement(SnippetElements.AUTHOR, author, ref headerElement);
            // 説明を設定する。
            this.AddChildElement(SnippetElements.DESCRIPTION, description, ref headerElement);
            // ショートカットを設定する。
            this.AddChildElement(SnippetElements.SHORTCUT, shortcut, ref headerElement);

            // Header要素をCodeSnippet要素に追加する。
            this.AddCodeSnippetNode(headerElement);
        }

        /// <summary>
        /// コード要素設定
        /// </summary>
        /// <param name="code">コード</param>
        /// <param name="language">言語</param>
        public void SetCode(String code, String language)
        {
            XmlElement codeElement = this.doc.CreateElement(SnippetElements.CODE);
            // コード部分を設定する。
            XmlCDataSection codeSection = this.doc.CreateCDataSection(code);
            codeElement.AppendChild(codeSection);
            // 言語要素を設定する。
            XmlAttribute languageAttribute = this.doc.CreateAttribute(SnippetAttributes.LANGUAGE);
            languageAttribute.Value = language;
            codeElement.Attributes.Append(languageAttribute);

            // Code要素をCodeSnippet要素に追加する。
            this.AddCodeSnippetNode(codeElement);
        }

        /// <summary>
        /// スニペットファイル保存
        /// </summary>
        /// <param name="filePath">保存ファイルパス</param>
        public void Save(String filePath)
        {
            // ファイル保存する。
            this.doc.Save(filePath);
        }

        /// <summary>
        /// 子要素追加
        /// </summary>
        /// <param name="elementName">要素名</param>
        /// <param name="value">設定値</param>
        /// <param name="parentElement">親要素</param>
        private void AddChildElement(String elementName, String value, ref XmlElement parentElement)
        {
            XmlElement element = this.doc.CreateElement(elementName);
            element.InnerText = value;
            parentElement.AppendChild(element);
        }

        /// <summary>
        /// CodeSnippetにXML要素を追加
        /// </summary>
        private void AddCodeSnippetNode(XmlElement element)
        {
            XmlNode snippetNode = this.doc.SelectSingleNode(XPaths.CODESNIPPET_XPATH);
            snippetNode.AppendChild(element);
        }
    }
}
