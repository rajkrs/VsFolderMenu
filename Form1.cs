using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VsFolderMenu
{
    public partial class Form1 : Form
    {
        StringBuilder sblog;
        public string ProjectName { get; set; }
        public string CurrentPath { get; set; }
        public Form1(string[] args)
        {
            sblog = new StringBuilder();
            InitializeComponent();

            SetCommand(args);
        }

        void SetCommand(string[] args)
        {
            sblog.AppendLine($"args: {string.Join(",", args)}");

            ProjectName = args[0].Trim('"');
            CurrentPath = args[1].Trim('"');
            if (CurrentPath.EndsWith(".csproj"))
            {
                CurrentPath = CurrentPath.Substring(0, CurrentPath.LastIndexOf('\\'));
            }
            sblog.AppendLine($"ProjecName:{ProjectName}");
            sblog.AppendLine($"CurrentPath :{CurrentPath }");

            Text = ProjectName.Substring(0, ProjectName.LastIndexOf('.'));
            sblog.AppendLine($"Text :{Text }");

            var suffix = string.Empty;
            switch (Text)
            {
                case "R6.DataEngine":
                    suffix = "Repository";
                    break;
                case "R6.RuleEngine":
                    suffix = "Service";
                    break;
                case "R6.Service":
                    suffix = "Controller";
                    break;
                case "R6.API.xUnitTest":
                    suffix = "ControllerUnitTest";
                    break;
                case "R6.Model":
                    suffix = "Dto";
                    break;
                default:
                    break;
            }

            lblFileType.Text = suffix;
            txtFIleName.Text = GetProfile();
            sblog.AppendLine($"textBox1.Text :{txtFIleName.Text }");


        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                try
                {
                    sblog.AppendLine($"Enter pressed.");
                    ProcessCommand(txtFIleName.Text);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(sblog.ToString() + "\n" + ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        void ProcessCommand(string inputText)
        {
            SaveProfile(txtFIleName.Text);

            inputText = inputText.Replace("/", "\\");
            string folder = string.Empty;
            string fileName = string.Empty;

            if (inputText.Contains("\\"))
            {
                folder = CurrentPath + "\\" + inputText.Substring(0, inputText.LastIndexOf('\\'));
                fileName = inputText.Substring(inputText.LastIndexOf('\\') + 1, inputText.Length - inputText.LastIndexOf('\\') - 1);
            }
            else
            {
                folder = CurrentPath;
                fileName = inputText;
            }

            sblog.AppendLine($"folder :{folder }");

            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }


            var fileContents = GetTemplates(fileName);
            foreach (var dic in fileContents)
            {
                File.WriteAllText(folder + "\\" + dic.Key, dic.Value);
                sblog.AppendLine($"file :{folder + "\\" + dic.Key }");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(MousePosition.X, MousePosition.Y);
        }

        private Dictionary<string, string> GetTemplates(string fileName)
        {

            var dic = new Dictionary<string, string>();

            sblog.AppendLine($"Application.ExecutablePath :{Path.GetDirectoryName(Application.ExecutablePath) }");

            var suffix = lblFileType.Text;
            DirectoryInfo d = new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\Templates");
            FileInfo[] Files = d.GetFiles($"*{suffix}.cs");
            foreach (var file in Files)
            {
                var fileData = File.ReadAllText(file.FullName);
                dic.Add(file.Name.Replace("Modifier", fileName), ReplaceFileData(fileData, fileName));
            }

            return dic;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var fileData = File.ReadAllText(Path.GetDirectoryName(Application.ExecutablePath) + "\\Templates\\Others.txt");

            string myTempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
            using (StreamWriter sw = new StreamWriter(myTempFile))
            {
                sw.WriteLine(ReplaceFileData(fileData, txtFIleName.Text));
            }
            System.Diagnostics.Process.Start(myTempFile);
        }

        private string ReplaceFileData(string fileData, string fileName)
        {
            var keyPascal = "Modifier";
            var keyCamel = "modifier";
            fileData = Regex.Replace(fileData, keyPascal, fileName);
            fileData = Regex.Replace(fileData, keyCamel, fileName.First().ToString().ToLower() + fileName.Substring(1, fileName.Length - 1));
            return fileData;
        }


        private void SaveProfile(string key) {
            var file = Path.GetDirectoryName(Application.ExecutablePath) + "\\profile.txt";
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(key);
            }
        }


        private string GetProfile()
        {
            var file = Path.GetDirectoryName(Application.ExecutablePath) + "\\profile.txt";
            if (File.Exists(file))
            {
                return File.ReadAllText(file);
            }
            else {
                return "";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                sblog.AppendLine($"Add button clicked.");
                ProcessCommand(txtFIleName.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(sblog.ToString() + "\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}

