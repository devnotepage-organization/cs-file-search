using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace cs_file_search
{
    public partial class FormMain : Form
    {
        private IEnumerable<string> _searchPathList;
        private Dictionary<string, int> _fileList;
        public FormMain()
        {
            InitializeComponent();
            LoadConfig();
            LoadFileList();
        }
        public void LoadConfig()
        {
            // JSONファイル読み込み
            string jsonString = string.Empty;
            string configPath = "config.json";
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    jsonString = System.IO.File.ReadAllText(configPath);
                    // ok
                    break;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("error:" + e.Message);
                    // retry
                    configPath = "../" + configPath;
                }
            }

            // 検索パスリスト取得
            JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
            JsonElement root = jsonDocument.RootElement;
            var searchPathList = root.GetProperty("searchPath").EnumerateArray();
            _searchPathList = searchPathList.Select(x => x.ToString());
        }
        public void LoadFileList()
        {
            string fileListString = string.Empty;
            try
            {
                fileListString = System.IO.File.ReadAllText("fileList.txt");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error:" + e.Message);
            }
            _fileList = fileListString
                .Split(new[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new KeyValuePair<string, int>(x, 0))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
