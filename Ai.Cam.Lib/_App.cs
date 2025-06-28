using Ai.Cam.Model;
using OpenCvSharp.Extensions;
using System.Drawing;
using ZXing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;

namespace Ai.Cam.Lib
{
    public static class ImageExtentions
    {
        public static byte[] ToByteArray(this Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }
    }

    public class _App
    {
        public event CapturedImageHandler Captured;
        public event CapturedImageHandler FaceDetected;
		public event CapturedImageHandler BodyDetected;
		public event CapturedQRImageHandler CapturedQRCode;

        public delegate void CapturedImageHandler(CapturedImage image);
        public delegate void CapturedQRImageHandler(string qr);


        public bool RunThreads { get; set; }

        // TODO: Number of camera's. 4, 5 ,6 (from settings).

        public _App()
        {
            this.RunThreads = true;

            
        }

        public string Start()
        {
            // Todo: Get the hardware webcams, create camera's and recognize images.

            // Todo: this is only 1 camera.
            OpenCvSharp.VideoCapture videoCapture = new OpenCvSharp.VideoCapture();
            videoCapture.Open(0);

            Thread thread = new Thread(i => this.Run(videoCapture));
            thread.Start();

            return videoCapture.Guid.ToString();
        }

        public void Stop()
        {
            this.RunThreads = false;
        }

        public void Run(OpenCvSharp.VideoCapture video)
        {
            if (video == null) { return; }

            while (this.RunThreads)
            {
                if (video.IsOpened())
                {
                    OpenCvSharp.Mat frame = new OpenCvSharp.Mat();
                    video.Read(frame);
                    
                    Bitmap image = BitmapConverter.ToBitmap(frame);

                    if(this.Captured != null)
                    {
                        //var cloneImage1 = (Bitmap)image.Clone();
                        var cloneImage1 = this.DetectPerson((Bitmap)image.Clone());

						//var cloneImage1 = (Bitmap)image.Clone();
						var cloneImage2 = this.DetectFullBodyPerson((Bitmap)image.Clone());

						// Read the QR code.
						this.QRCode((Bitmap)image.Clone());

						//this.BodyDetected(
						//	new CapturedImage()
						//	{
						//		ID = video.Guid.ToString(),
						//		Image = cloneImage2
						//	});

						this.Captured(
                            new CapturedImage()
                            {
                                ID = video.Guid.ToString(),
                                Image = cloneImage1
                            });
                    }
                }

                // Fire a refresh with the new image.

                Thread.Sleep(10);
            }
        }

        private Bitmap DetectPerson(Bitmap bitmap)
        {
            try
            {
                Image<Bgr, Byte> frame = bitmap.ToImage<Bgr, Byte>(); //image that stores your bitmap
                Image<Gray, Byte> grayFrame = frame.Convert<Gray, Byte>(); //grayscale of your image

                Emgu.CV.CascadeClassifier haar = new Emgu.CV.CascadeClassifier("C:\\TFS\\Ai.Cam.App\\emu\\haarcascade_frontalface_default.xml"); //the object used for detection

                var faces = haar.DetectMultiScale(grayFrame, 1.1, 3, new System.Drawing.Size(25, 25), new System.Drawing.Size(130, 130)); //variable that stores detection information

                foreach (var face in faces)
                {
                    //CvInvoke.Ellipse(grayFrame, new System.Drawing.Point(5, 5), new System.Drawing.Size(10, 20), 0, 0, 360, new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LineType.AntiAlias, 0);
                    //frame = frame.And(frame, grayFrame);

                    Bitmap bmp;
                    using (var ms = new MemoryStream(frame.ToJpegData()))
                    {
                        bmp = new Bitmap(ms);
                    }

                    if (FaceDetected != null)
                    {
                        this.FaceDetected(new CapturedImage() { ID = Guid.NewGuid().ToString(), Image = bmp });
                    }

                    return bmp;
                }
            }
            catch (Exception ex)
            {

            }

            return bitmap;
        }

        private Bitmap DetectFullBodyPerson(Bitmap bitmap)
        {
			try
			{
				Image<Bgr, Byte> frame = bitmap.ToImage<Bgr, Byte>(); //image that stores your bitmap
				Image<Gray, Byte> grayFrame = frame.Convert<Gray, Byte>(); //grayscale of your image

				Emgu.CV.CascadeClassifier haar = new Emgu.CV.CascadeClassifier("C:\\TFS\\Ai.Cam.App\\emu\\haarcascade_fullbody.xml"); //the object used for detection

				var faces = haar.DetectMultiScale(grayFrame, 1.1, 3, new System.Drawing.Size(25, 25), new System.Drawing.Size(130, 130)); //variable that stores detection information

				foreach (var face in faces)
				{
					//CvInvoke.Ellipse(grayFrame, new System.Drawing.Point(5, 5), new System.Drawing.Size(10, 20), 0, 0, 360, new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LineType.AntiAlias, 0);
					//frame = frame.And(frame, grayFrame);

					Bitmap bmp;
					using (var ms = new MemoryStream(frame.ToJpegData()))
					{
						bmp = new Bitmap(ms);
					}

					if (BodyDetected != null)
					{
						this.BodyDetected(new CapturedImage() { ID = Guid.NewGuid().ToString(), Image = bmp });
					}

					return bmp;
				}
			}
			catch (Exception ex)
			{

			}

			return bitmap;
		}

        public void QRCode(System.Drawing.Bitmap image)
        {
            try
            {
                ////var barcodeReader = new BarcodeReader();

                ////var result = barcodeReader.Decode(image);
                ////if (result != null)
                ////{
                ////    //MessageBox.Show(result.Text);

                ////    if (this.CapturedQRCode != null)
                ////    {
                ////        this.CapturedQRCode(result.Text);
                ////    }
                ////}
            }
            catch (Exception ex)
            {

            }
            
        }
    }

    
}
