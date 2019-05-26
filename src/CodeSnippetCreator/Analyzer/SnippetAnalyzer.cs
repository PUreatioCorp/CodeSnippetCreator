using CodeSnippetCreator.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeSnippetCreator.Analyzer
{
    /// <summary>
    /// スニペット解析クラス
    /// </summary>
    public class SnippetAnalyzer
    {
        /// <summary>namespace管理</summary>
        private XmlNamespaceManager nsManager = null;

        /// <summary>ヘッダ要素</summary>
        private XmlNode headerNode = null;
        /// <summary>コード要素</summary>
        private XmlNode codeNode = null;

        /// <summary>
        /// スニペット読み込み
        /// </summary>
        /// <param name="filePath">読み込みファイルパス</param>
        public void ReadSnippet(String filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            // namespace対応
            XmlNamespaceManager tmpNsManager = new XmlNamespaceManager(doc.NameTable);
            tmpNsManager.AddNamespace(XPaths.NAMESPACE_PREFIX, SnippetAttributes.XMLNS_CONTENTS);

            // 要素を変数に設定する。
            this.headerNode = doc.SelectSingleNode(XPaths.HEADER_XPATH, tmpNsManager);
            this.codeNode = doc.SelectSingleNode(XPaths.CODE_XPATH, tmpNsManager);
            this.nsManager = tmpNsManager;
        }

        /// <summary>
        /// タイトル取得
        /// </summary>
        /// <returns>タイトル</returns>
        public String GetHeaderTitle()
        {
            return this.GetHeaderChildText(XPaths.TITLE_XPATH);
        }

        /// <summary>
        /// 管理者取得
        /// </summary>
        /// <returns>管理者</returns>
        public String GetHeaderAuthor()
        {
            return this.GetHeaderChildText(XPaths.AUTHOR_XPATH);
        }

        /// <summary>
        /// 説明取得
        /// </summary>
        /// <returns>説明</returns>
        public String GetHeaderDescription()
        {
            return this.GetHeaderChildText(XPaths.DESCRIPTION_XPATH);
        }

        /// <summary>
        /// ショートカット取得
        /// </summary>
        /// <returns>ショートカット</returns>
        public String GetHeaderShortcut()
        {
            return this.GetHeaderChildText(XPaths.SHORTCUT_XPATH);
        }

        /// <summary>
        /// 言語取得
        /// </summary>
        /// <returns>言語</returns>
        public String GetCodeLanguage()
        {
            return this.codeNode.Attributes[SnippetAttributes.LANGUAGE].Value as string;
        }

        /// <summary>
        /// コード取得
        /// </summary>
        /// <returns>コード</returns>
        public String GetCode()
        {
            XmlNode codeCdataNode = this.codeNode.ChildNodes.Item(0);
            return codeCdataNode.Value;
        }

        /// <summary>
        /// ヘッダ要素の子要素に設定されたテキスト取得
        /// </summary>
        /// <param name="xpath">検索用XPath</param>
        /// <returns>子要素に設定されたテキスト</returns>
        private String GetHeaderChildText(String xpath)
        {
            XmlNode childNode = this.headerNode.SelectSingleNode(xpath, this.nsManager);
            // 要素が存在しない場合、空文字を返却する。
            if (childNode == null)
            {
                return String.Empty;
            }

            return childNode.InnerText;
        }
    }
}
