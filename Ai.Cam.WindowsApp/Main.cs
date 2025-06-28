using _Ai.Cam.WindowsApp;
using Ai.Cam.Lib;
using Ai.Cam.Model;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ComponentModel;

namespace Ai.Cam.WindowsApp
{
    public partial class Main : Form
    {
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public _App App { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public QuantumApp QuantumApp { get; set; }

		// Todo: how many camera's?
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

		// Todo: how many camera's?
		public Camera Cam { get; set; }

        public Main()
        {
            InitializeComponent();

   //         this.App = new App();
   //         this.App.Captured += App_Captured;
   //         this.App.FaceDetected += App_FaceDetected;
			//this.App.BodyDetected += App_BodyDetected;
			//this.App.CapturedQRCode += App_CapturedQRCode;

            this.QuantumApp = new QuantumApp();
			this.QuantumApp.FaceDetected += App_FaceDetected;
			this.QuantumApp.BodyDetected += App_BodyDetected;
			this.QuantumApp.CapturedQRCode += App_CapturedQRCode;
			this.QuantumApp.CapturedID += QuantumApp_CapturedID;
		}

		private void QuantumApp_CapturedID(CapturedImage image)
		{
			this.idBox.Image = image.Image;
		}

		private void App_CapturedQRCode(string qr)
        {
            MessageBox.Show(qr);
        }

        private void App_FaceDetected(CapturedImage image)
        {
			//Camera camera = new Camera();
			//camera.Show(image);

			//camera.ShowDialog();

			this.faceBox.Image = image.Image;

            //MessageBox.Show("Face Detected");
        }

		private void App_BodyDetected(CapturedImage image)
		{
			//Camera camera = new Camera();
			//camera.Show(image);

			//camera.ShowDialog();

			this.bodyPic.Image = image.Image;

			//MessageBox.Show("Body Detected");
		}

		private void App_Captured(CapturedImage image)
        {
            if(image.ID == this.Cam.ID)
            {
                this.Cam.Show(image);
            }
        }

        async private void btnStart_Click(object sender, EventArgs e)
        {
            //this.Cam = new Camera();
            //this.Cam.Show();

            //this.Cam.ID = this.App.Start();

			////if (capture.IsOpened())
			////{
			////    capture.Read(frame);
			////    image = BitmapConverter.ToBitmap(frame);
			////    if (pictureBox1.Image != null)
			////    {
			////        pictureBox1.Image.Dispose();
			////    }
			////    pictureBox1.Image = image;
			////}

			await this.QuantumApp.Run();
		}

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.QuantumApp.Exit();
            //this.App.Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			//this.App.Stop();
			this.QuantumApp.Exit();

			//this.Cam.Close();
            this.Close();
        }
    }
}
