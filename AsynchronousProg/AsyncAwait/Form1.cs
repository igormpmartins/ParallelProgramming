using System.Net;

namespace AsyncAwait
{
    public partial class Form1 : Form
    {

        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(5000);
            return 123;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnCalculate_Click(object sender, EventArgs e)
        {
            var calculate = await CalculateValueAsync();
            lblResult.Text = calculate.ToString();

            await Task.Delay(5000);

            using (var wc = new WebClient())
            {
                string data = await wc.DownloadStringTaskAsync("http://google.com/robots.txt");
                lblResult.Text = data.Split("\n")[0].Trim();
            }

        }
    }
}