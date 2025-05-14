using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AnomalyDetector
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;

        public Form1(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeComponent(); // Calls InitializeComponent from Form1.Designer.cs
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            var plugins = PluginLoader.LoadPlugins("Plugins");
            foreach (var plugin in plugins)
            {
                var btn = new Button
                {
                    Text = plugin.Name,
                    Width = 200,
                    Height = 30
                };
                btn.Click += (s, e) => plugin.Execute();
                pluginPanel.Controls.Add(btn);
            }
        }

        private async void BtnPredict_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string backendUrl = _configuration["AppSettings:BackendUrl"];

                try
                {
                    var content = new MultipartFormDataContent();
                    var imageContent = new ByteArrayContent(File.ReadAllBytes(openFileDialog.FileName));
                    imageContent.Headers.Add("Content-Type", "application/octet-stream");
                    content.Add(imageContent, "file", Path.GetFileName(openFileDialog.FileName));

                    var response = await client.PostAsync($"{backendUrl}/predict", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var prediction = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Prediction: {prediction}");
                    }
                    else
                    {
                        MessageBox.Show("Error in prediction!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}
