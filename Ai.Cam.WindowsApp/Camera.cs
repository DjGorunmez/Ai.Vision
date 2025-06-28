using Ai.Cam.Lib;
using Ai.Cam.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Ai.Cam.WindowsApp
{
    public partial class Camera : Form
    {
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ID { get; set; }

        public Camera()
        {
            InitializeComponent();
        }

        internal void Show(CapturedImage image)
        {
            this.pbImage.Image = image.Image;
        }
    }
}
