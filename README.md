Quantum Computing: Ai.Vision. 
Detect face/body/qrcode/allobjects in c#.

Internal usage of the package Emugu.CV. (download from nuget or restore all packages).

To Start the app:
QuantumApp -> Operation.

Enable which detection to use:

QuantumCube<Bitmap> cube = new QuantumCube<Bitmap>();
cube.OnResultReady += Cube_OnResultReady;

cube.AddState(new AllObjects());
//cube.AddState(new FaceState());
//cube.AddState(new BodyState());
//cube.AddState(new QRCodeState());
//cube.AddState(new IDState());
//cube.AddState(new JoloAllObjects());

Start the app.
#########################################################################################

Defining your own detection:

Inherit from State<Bitmap>
override Operation method.

public class NewDetection: State<Bitmap>
{
	public override void Operation()
	{
		// Detect here. 

		//this.Value

		base.Operation();
	}
}

##########################################################################################

Initialize your state in the QuantumApp.
public void Operation()
{
	QuantumCube<Bitmap> cube = new QuantumCube<Bitmap>();
	cube.OnResultReady += Cube_OnResultReady;

	//cube.AddState(new AllObjects());
	cube.AddState(new NewDetection());
 }

 ##########################################################################################

 Handle the detection.

 private void Cube_OnResultReady(State<Bitmap> st)
 {
  	if (st is NewDetection)
  	{
  		if (CapturedID != null)
  		{
  			this.CapturedID(new CapturedImage() { ID = Guid.NewGuid().ToString(), Image = st.Value });
  		}
  	}
 }

 ###########################################################################################

 Run the app.
