using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippetCreator.Constants
{
    /// <summary>
    /// 健作用XPath定数
    /// </summary>
    public static class XPaths
    {
        /// <summary>namespace</summary>
        public const String NAMESPACE_PREFIX = "ns";

        /// <summary>CodeSnippet検索用XPath</summary>
        public const String CODESNIPPET_XPATH = "/CodeSnippets/CodeSnippet";

        /// <summary>Header検索用XPath</summary>
        public const String HEADER_XPATH = "/ns:CodeSnippets/ns:CodeSnippet/ns:Header";
        /// <summary>Code検索用XPath</summary>
        public const String CODE_XPATH = "/ns:CodeSnippets/ns:CodeSnippet/ns:Code";
        /// <summary>Title検索用XPath</summary>
        public const String TITLE_XPATH = "ns:Title";
        /// <summary>Author検索用XPath</summary>
        public const String AUTHOR_XPATH = "ns:Author";
        /// <summary>Description検索用XPath</summary>
        public const String DESCRIPTION_XPATH = "ns:Description";
        /// <summary>Shortcut検索用XPath</summary>
        public const String SHORTCUT_XPATH = "ns:Shortcut";
    }
}
