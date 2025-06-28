Quantum Computing: Ai.Vision. 
Detect face/body/qrcode/allobjects in c#.
Code base with examples.

Internal usage of the package Emugu.CV. (download from nuget or restore all packages).

To Start the app:
QuantumApp -> Operation.

Enable which detection to use:

<pre>
QuantumCube<Bitmap> cube = new QuantumCube<Bitmap>();
cube.OnResultReady += Cube_OnResultReady;
</pre>

<pre>	
cube.AddState(new AllObjects());
//cube.AddState(new FaceState());
//cube.AddState(new BodyState());
//cube.AddState(new QRCodeState());
//cube.AddState(new IDState());
//cube.AddState(new JoloAllObjects());
</pre>

Start the app.
#########################################################################################

Defining your own detection:

Inherit from State<Bitmap>
override Operation method.

<pre>
public class NewDetection: State<Bitmap>
{
	public override void Operation()
	{
		// Detect here. 

		//this.Value

		base.Operation();
	}
}
</pre>

##########################################################################################

Initialize your state in the QuantumApp.

<pre>
public void Operation()
{
	QuantumCube<Bitmap> cube = new QuantumCube<Bitmap>();
	cube.OnResultReady += Cube_OnResultReady;

	//cube.AddState(new AllObjects());
	cube.AddState(new NewDetection());
 }
</pre>
	
 ##########################################################################################

 Handle the detection.

<pre>
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
</pre>
	
 ###########################################################################################

 Run the app.
