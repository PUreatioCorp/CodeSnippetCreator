using CodeSnippetCreator.Analyzer;
using CodeSnippetCreator.Creator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CodeSnippetCreator
{
    /// <summary>
    /// コードスニペット作成フォームクラス
    /// </summary>
    public partial class CodeSnippetCreatorForm : Form
    {
        /// <summary>ファイルダイアログフィルタ</summary>
        private const String FILE_DIALOG_FILTER = "スニペットファイル（*.snippet）|*.snippet";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CodeSnippetCreatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void CodeSnippetCreatorForm_Load(object sender, EventArgs e)
        {
            // 入力情報を初期化する。
            this.InitializeInput();
        }

        /// <summary>
        /// ファイルを開く押下イベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // スニペットを読み込む。
            String openFilePath = this.GetOpenPath();

            // キャンセルされた場合、処理をしない。
            if (String.IsNullOrEmpty(openFilePath))
            {
                return;
            }

            // スニペット情報を設定する。
            this.SetSnippetInput(openFilePath);
        }

        /// <summary>
        /// 終了押下イベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void endToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 入力情報クリアボタン押下イベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            // 入力情報を初期化する。
            this.InitializeInput();
        }

        /// <summary>
        /// スニペット作成ボタン押下イベント
        /// </summary>
        /// <param name="sender">イベント発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void createButton_Click(object sender, EventArgs e)
        {
            String saveFilePath = this.GetSavePath();
            // 保存先が指定されていない場合、処理しない。
            if (String.IsNullOrEmpty(saveFilePath))
            {
                return;
            }

            SnippetCreator creator = new SnippetCreator();
            // ヘッダ情報を設定する。
            creator.SetHeader(this.titleTextBox.Text, this.authorTextBox.Text,
                this.descriptionTextBox.Text, this.shortcutTextBox.Text);
            // コード情報を設定する。
            creator.SetCode(this.codeTextBox.Text, this.languageComboBox.SelectedItem as string);
            // スニペットを保存する。
            creator.Save(saveFilePath);

            MessageBox.Show("Complete save file.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 入力情報初期化
        /// </summary>
        private void InitializeInput()
        {
            // ヘッダ要素を初期化する。
            this.titleTextBox.Text = String.Empty;
            this.authorTextBox.Text = String.Empty;
            this.descriptionTextBox.Text = String.Empty;
            this.shortcutTextBox.Text = String.Empty;

            // コード要素を初期化する。
            this.languageComboBox.SelectedIndex = 0;
            this.codeTextBox.Text = String.Empty;
        }

        /// <summary>
        /// スニペット入力情報設定
        /// </summary>
        /// <param name="filePath">読み込みファイルパス</param>
        private void SetSnippetInput(String filePath)
        {
            // スニペットを読み込む。
            SnippetAnalyzer analyzer = new SnippetAnalyzer();
            analyzer.ReadSnippet(filePath);

            // ヘッダ要素を設定する。
            this.titleTextBox.Text = analyzer.GetHeaderTitle();
            this.authorTextBox.Text = analyzer.GetHeaderAuthor();
            this.descriptionTextBox.Text = analyzer.GetHeaderDescription();
            this.shortcutTextBox.Text = analyzer.GetHeaderShortcut();

            // コード要素を設定する。
            this.languageComboBox.SelectedItem = analyzer.GetCodeLanguage();
            this.codeTextBox.Text = analyzer.GetCode();
        }

        /// <summary>
        /// 読み込みファイルパス取得
        /// </summary>
        /// <returns>読み込みファイルパス</returns>
        private String GetOpenPath()
        {
            String openFilePath = null;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = FILE_DIALOG_FILTER;

                DialogResult result = dialog.ShowDialog();
                if (DialogResult.OK == result)
                {
                    openFilePath = dialog.FileName;
                }
            }

            return openFilePath;
        }

        /// <summary>
        /// 保存ファイルパス取得
        /// </summary>
        /// <returns>保存ファイルパス</returns>
        private String GetSavePath()
        {
            String saveFilePath = null;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = FILE_DIALOG_FILTER;

                DialogResult result = dialog.ShowDialog();
                if (DialogResult.OK == result)
                {
                    saveFilePath = dialog.FileName;
                }
            }

            return saveFilePath;
        }
    }
}
