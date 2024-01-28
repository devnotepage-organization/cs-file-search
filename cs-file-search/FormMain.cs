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
        public FormMain()
        {
            InitializeComponent();
            LoadConfig();
        }
        public void LoadConfig()
        {
            // JSONファイル読み込み
            string jsonString = System.IO.File.ReadAllText("../../config.json");

            JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
            JsonElement root = jsonDocument.RootElement;
            var searchPath = root.GetProperty("searchPath").EnumerateArray();
            System.Console.WriteLine(searchPath.ToString());
        }
    }
}
