using Ai.Cam.Model;
using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using OpenCvSharp.Extensions;
using QuantumComputing;
using System.Drawing;
using Mat = Emgu.CV.Mat;


namespace Ai.Cam.Lib
{
	public class QuantumApp
	{
		public event CapturedImageHandler FaceDetected;
		public event CapturedImageHandler BodyDetected;
		public event CapturedQRImageHandler CapturedQRCode;
		public event CapturedImageHandler CapturedID;


		public delegate void CapturedImageHandler(CapturedImage image);
		public delegate void CapturedQRImageHandler(string qr);

		public QuantumApp()
		{
			//todo initialize the quantum cube.

		}

		bool run = true;
		async public Task Run()
		{
			this.run = true;

			Thread thread = new Thread(Operation);
			thread.Start();
		}

		public void Operation()
		{
			QuantumCube<Bitmap> cube = new QuantumCube<Bitmap>();
			cube.OnResultReady += Cube_OnResultReady;

			//cube.AddState(new FaceState());
			//cube.AddState(new BodyState());

			//cube.AddState(new QRCodeState());
			//cube.AddState(new IDState());

			cube.AddState(new AllObjects());
			//cube.AddState(new JoloAllObjects());

			OpenCvSharp.VideoCapture video = new OpenCvSharp.VideoCapture();
			video.Open(0);

			if (video.IsOpened())
			{
				while (run)
				{
					OpenCvSharp.Mat frame = new OpenCvSharp.Mat();
					video.Read(frame);

					Bitmap image = BitmapConverter.ToBitmap(frame);
					cube.SetValue(image);

					cube.Run();

					Thread.Sleep(10);
				}
			}

			video.Dispose();
		}

		private void Cube_OnResultReady(State<Bitmap> st)
		{
			if (st is FaceState)
			{
				if (FaceDetected != null)
				{
					this.FaceDetected(new Model.CapturedImage() { ID = Guid.NewGuid().ToString(), Image = st.Value });
				}
			}

			if (st is BodyState)
			{
				if (BodyDetected != null)
				{
					this.BodyDetected(new Model.CapturedImage() { ID = Guid.NewGuid().ToString(), Image = st.Value });
				}
			}

			if (st is QRCodeState)
			{
				if (CapturedQRCode != null)
				{
					this.CapturedQRCode((st as QRCodeState).Text);
				}
			}

			if (st is IDState)
			{
				if (CapturedID != null)
				{
					this.CapturedID(new CapturedImage() { ID = Guid.NewGuid().ToString(), Image = st.Value });
				}
			}

			if(st is AllObjects)
			{
				if (CapturedID != null)
				{
					this.CapturedID(new CapturedImage() { ID = Guid.NewGuid().ToString(), Image = st.Value });
				}
			}

			if (st is JoloAllObjects)
			{
				if (CapturedID != null)
				{
					this.CapturedID(new CapturedImage() { ID = Guid.NewGuid().ToString(), Image = st.Value });
				}
			}
		}

		public void Exit()
		{
			this.run = false;
		}
	}

	public class FaceState : State<Bitmap>
	{
		public override void Operation()
		{
			try
			{
				Image<Bgr, Byte> frame = this.Value.ToImage<Bgr, Byte>(); //image that stores your bitmap
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

					this.Value = bmp;

					this.SetResult(this);
				}
			}
			catch (Exception ex)
			{

			}
		}
	}

	public class BodyState : State<Bitmap>
	{
		public override void Operation()
		{
			try
			{
				Image<Bgr, Byte> frame = this.Value.ToImage<Bgr, Byte>(); //image that stores your bitmap
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

					this.Value = bmp;
					this.SetResult(this);
				}
			}
			catch (Exception ex)
			{

			}
		}
	}

	public class QRCodeState : State<Bitmap>
	{
		public string Text { get; set; }

		public override void Operation()
		{
			try
			{
				//TODO: why is this not working?

				// Convert Bitmap to Mat
				Emgu.CV.Mat frame = new Emgu.CV.Mat(); 
				this.Value.ToMat(frame);

				// Create the QR code detector
				var qrCodeDetector = new Emgu.CV.QRCodeDetector();

				// Prepare a Mat to hold the points of the detected QR code
				Mat points = new Mat();

				// Detect the QR code
				bool detected = qrCodeDetector.Detect(frame, points);

				if (detected)
				{
					// Decode the QR code
					Text = qrCodeDetector.Decode(frame, points);
					this.SetResult(this);

				}
			}
			catch (Exception ex)
			{

			}
		}
	}

	public class IDState : State<Bitmap>
	{
		public string Text { get; set; }

		public override void Operation()
		{
			try
			{
				var img = this.Value.Clone() as Bitmap;

				// Load the image
				var image = img.ToImage<Bgr, byte>();

				// Convert to grayscale
				var grayImage = image.Convert<Gray, byte>(); 
				
				// Apply Gaussian blur
				var blurredImage = grayImage.SmoothGaussian(5); 
				
				// Threshold the image
				var thresholdedImage = blurredImage.ThresholdBinary(new Gray(100), new Gray(255)); 
				
				// Initialize Tesseract OCR
				var ocr = new Tesseract(@".\TessData", "nld", OcrEngineMode.TesseractLstmCombined); 
				
				// Set the image for OCR
				ocr.SetImage(thresholdedImage); 
				
				// Recognize the text
				ocr.Recognize(); 
				string text = ocr.GetUTF8Text(); 
				
				// Display the recognized text
				//Console.WriteLine("Recognized Text:"); Console.WriteLine(text); 
				
				// Draw bounding boxes around the detected text (optional)
				var words = ocr.GetWords().ToList();

				if (words.Any())
				{
					foreach (var word in words)
					{
						var rect = new Rectangle(word.Region.X, word.Region.Y, word.Region.Width, word.Region.Height);
						image.Draw(rect, new Bgr(Color.Red), 2);
					}

					this.Value = image.ToBitmap();

					this.SetResult(this);
				}
				 
				
				// Display the result
				//CvInvoke.Imshow("Detected ID Card", image); CvInvoke.WaitKey(0);
			}
			catch (Exception ex)
			{

			}
		}
	}

	public class AllObjects : State<Bitmap>
	{
		public string Text { get; set; }

		public override void Operation()
		{
			try
			{
				Emgu.CV.Mat mat = new Emgu.CV.Mat();
				this.Value.ToMat(mat);

				Emgu.CV.Mat gray = new Emgu.CV.Mat();
				CvInvoke.CvtColor(mat, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
				CvInvoke.GaussianBlur(gray, gray, new System.Drawing.Size(5, 5), 1);
				CvInvoke.Threshold(gray, gray, 100, 255, Emgu.CV.CvEnum.ThresholdType.Binary);

				VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
				CvInvoke.FindContours(gray, contours, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

				for (int i = 0; i < contours.Size; i++)
				{
					Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);
					CvInvoke.Rectangle(mat, rect, new MCvScalar(0, 255, 0), 2);

					//Todo: Learn from the image.
					this.AnalyzeImage(this.Value);

					//Todo: Guess the image.

					// Ai descriptive?
				}

				this.Value = mat.ToBitmap();
				this.SetResult(this);
			}
			catch (Exception ex)
			{

			}

			base.Operation();
		}

		public void AnalyzeImage(Bitmap value)
		{
			////// Create Azure Vision model
			////var visionModel = AzureVisionCaptioningModel.Create("https://your-vision.cognitiveservices.azure.com", "your-vision-api-key");

			////// Analyze image from URL
			////var captionResult = await visionModel.GenerateCaptionAsync("https://example.com/image.jpg");

			////Console.WriteLine($"Caption: {captionResult.Caption.Text}");
			////Console.WriteLine($"Confidence: {captionResult.Caption.Confidence:P2}");

			////// Generate detailed dense captions
			////var denseCaptions = await visionModel.GenerateDenseCaptionsAsync(imageStream);
			////foreach (var caption in denseCaptions.Captions)
			////{
			////	Console.WriteLine($"Region: {caption.Text} (Confidence: {caption.Confidence:P2})");
			////	Console.WriteLine($"Location: X={caption.BoundingBox.X}, Y={caption.BoundingBox.Y}");
			////}
		}
	}

	public class JoloAllObjects : State<Bitmap>
	{
		public string Text { get; set; }

		public Net Net { get; set; }

		public string[] Labels { get; set; }

		public override void Run()
		{
			string cfg = "C:\\yolov3\\yolov3.cfg";
			string weights = "C:\\yolov3\\yolov3.weights";
			string names = "C:\\yolov3\\coco.names";

			this.Net = DnnInvoke.ReadNetFromDarknet(cfg, weights);
			this.Net.SetPreferableBackend(Emgu.CV.Dnn.Backend.OpenCV);
			this.Net.SetPreferableTarget(Target.Cpu);

			this.Labels = System.IO.File.ReadAllLines(names);
			//var capture = new VideoCapture();

			base.Run();
		}

		public override void Operation()
		{
			try
			{
				
				Emgu.CV.Mat frame = new Emgu.CV.Mat();
				this.Value.ToMat(frame);

				var blob = DnnInvoke.BlobFromImage(frame, 1 / 255.0, new System.Drawing.Size(416, 416), new MCvScalar(), true, false);
				this.Net.SetInput(blob);

				VectorOfMat output = new VectorOfMat();
				this.Net.Forward(output, this.Net.UnconnectedOutLayersNames);

				for (int k = 0; k < output.Size; k++)
				{
					float[] data = new float[output[k].Total.ToInt32()];
					output[k].CopyTo(data);

					for (int i = 0; i < data.Length; i += 85)
					{
						float confidence = data[i + 4];
						if (confidence > 0.5)
						{
							float x = data[i] * frame.Width;
							float y = data[i + 1] * frame.Height;
							float w = data[i + 2] * frame.Width;
							float h = data[i + 3] * frame.Height;

							int left = (int)(x - w / 2);
							int top = (int)(y - h / 2);

							CvInvoke.Rectangle(frame, new Rectangle(left, top, (int)w, (int)h), new MCvScalar(0, 255, 0), 2);
							CvInvoke.PutText(frame, this.Labels[0], new System.Drawing.Point(left, top - 10), Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 255, 0));
						}
					}
				}

				CvInvoke.Imshow("YOLOv3 Detection", frame);
				
				this.Value = frame.ToBitmap();
				this.SetResult(this);
			}
			catch (Exception ex)
			{

			}

			base.Operation();
		}
	}
}
