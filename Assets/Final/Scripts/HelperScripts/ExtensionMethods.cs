using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

	public static float SquaredDistance(this Vector3 pos1, Vector3 pos2){
		return
		Mathf.Pow ((pos1.x - pos2.x), 2f) +
		Mathf.Pow ((pos1.y - pos2.y), 2f) + 
		Mathf.Pow ((pos1.z - pos2.z), 2f);
	}

	public static void SetX(this Transform trans, float x){
		Vector3 temp = trans.position;
		temp.x = x;
		trans.position = temp;
	}

	public static void SetY(this Transform trans, float y){
		Vector3 temp = trans.position;
		temp.y = y;
		trans.position = temp;
	}

	public static void SetZ(this Transform trans, float z){
		Vector3 temp = trans.position;
		temp.z = z;
		trans.position = temp;
	}

	//function call, as stated by the UML. Axis not actually used, as method didnt need it.
	//Kept it just for the sake of the UML
	public static void FaceObjectOnAxis (this Transform myObject, Transform otherObject, Vector3 axis){
		//Find position to face.
		Vector3 facing = otherObject.position - myObject.position;
		//Find rotation.
		Quaternion toRotation = Quaternion.LookRotation(facing);
		Vector3 euler = toRotation.eulerAngles;
		toRotation = Quaternion.Euler(euler);
		//Sets rotation of Object
		myObject.rotation = toRotation;
		//Limits rotation to Y axis.
		myObject.eulerAngles = new Vector3(0, myObject.eulerAngles.y, 0);
	}

	public static void FaceObjectOnAxis (this Transform myObject, Vector3 otherObject, Vector3 axis){
		//Find position to face.
		Vector3 facing = otherObject - myObject.position;
		//Find rotation.
		Quaternion toRotation = Quaternion.LookRotation(facing);
		Vector3 euler = toRotation.eulerAngles;
		toRotation = Quaternion.Euler(euler);
		//Sets rotation of Object
		myObject.rotation = toRotation;
		//Limits rotation to Y axis.
		myObject.eulerAngles = new Vector3(0, myObject.eulerAngles.y, 0);
	}
   
	public static Color RGBtoHSI (this Color rgb){
		float R = rgb.r;
		float G = rgb.g;
		float B = rgb.b;
		float I = ((R + G + B)/3f);
		float m = Mathf.Min(R, G, B);
		float S;
		if (I > 0){
			S = 1-(m/I);
		} else {
			S = 0;
		}

		float H;
		if (G >= B){
			H = Mathf.Acos((R-(G/2)-(B/2))/(Mathf.Sqrt(Mathf.Pow(R,2f)+Mathf.Pow(G,2f)+Mathf.Pow(B,2f)-(R*G)-(R*B)-(G*B))));							
		} else {
			H = 360-(Mathf.Acos((R-(G/2)-(B/2))/(Mathf.Sqrt(Mathf.Pow(R,2f)+Mathf.Pow(G,2f)+Mathf.Pow(B,2f)-(R*G)-(R*B)-(G*B)))));
		}
		Vector4 hsi = new Vector4 (H,S,I,rgb.a);
		hsi.Normalize();
		return hsi;
	}

	public static Color HSItoRGB (this Color hsi){
		float H = hsi.r;
		float S = hsi.g;
		float I = hsi.b;
		float R;
		float G;
		float B;
		if (H == 0){
			R = I+(2*I*S);
			G = I-(I*S);
			B = I-(I*S);
		} else if (0 < H && H < 120){
			R = I+((I*S)*(Mathf.Cos(H)/Mathf.Cos(60-H)));
			G = I+((I*S)*(1-(Mathf.Cos(H)/Mathf.Cos(60-H))));
			B = I-(I*S);
		} else if (H == 120){
			R = I-(I*S);
			G = I+(2*I*S);
			B = I-(I*S);
		} else if (120 < H && H < 240){
			R = I-(I*S);
			G = I+((I*S)*(Mathf.Cos(H-120)/Mathf.Cos(180-H)));
			B = I+((I*S)*(1-(Mathf.Cos(H-120)/Mathf.Cos(180-H))));
		} else if (H == 240){
			R = I-(I*S);
			G = I-(I*S);
			B = I+(2*I*S);
		} else {
			R = I+((I*S)*(1-(Mathf.Cos(H-240)/Mathf.Cos(300-H))));
			G = I-(I*S);
			B = I+((I*S)*(Mathf.Cos(H-240)/Mathf.Cos(300-H)));
		}
		Vector4 rgb = new Vector4(R,G,B,hsi.a);
		rgb.Normalize();
		return rgb;
	}
		
}
