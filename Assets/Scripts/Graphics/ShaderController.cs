using UnityEngine;
using System.Collections;

public class ShaderController : MonoBehaviour {

	public Material shaderMaterial;

	public Color AmbientNightBeginColor;
	public Color AmbientNightMiddleColor;
	public Color AmbientNightEndColor;
	public Color AmbientDayBeginColor;
	public Color AmbientDayMiddleColor;
	public Color AmbientDayEndColor;


	public Color LightDayBeginColor;
	public Color LightDayMiddleColor;
	public Color LightDayEndColor;

	void Update() {

		setShaderAmbientColor();
		setLightAmbientColor();
		//shaderMaterial.SetVector("_InsanityVector", new Vector4(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), 0,0 ));
		//shaderMaterial.SetVector("_InsanityVector2", new Vector4(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0,0 ));
	}

	private void setShaderAmbientColor ()
	{
		float dayRatio = DayNightController.getDayRatio();
		Color begin;
		Color middle;
		Color end;
		if (DayNightController.isDay()) {
			begin = AmbientDayBeginColor;
			middle = AmbientDayMiddleColor;
			end = AmbientDayEndColor;
		}
		else {
			begin = AmbientNightBeginColor;
			middle = AmbientNightMiddleColor;
			end = AmbientNightEndColor;
		}
		Color c;
		if (dayRatio < 0.5f) {
			c = new Color (MathHelper.Map (dayRatio, 0, 0.5f, begin.r, middle.r), 
			               MathHelper.Map (dayRatio, 0, 0.5f, begin.g, middle.g), 
			               MathHelper.Map (dayRatio, 0, 0.5f, begin.b, middle.b), 1);
		}
		else {
			c = new Color (MathHelper.Map (dayRatio, 0.5f, 1, middle.r, end.r), 
			               MathHelper.Map (dayRatio, 0.5f, 1, middle.g, end.g), 
			               MathHelper.Map (dayRatio, 0.5f, 1, middle.b, end.b), 1);
		}
		shaderMaterial.SetColor("_AmbientColor", c);
		int day = (DayNightController.isDay()) ? 1 : 0;
		shaderMaterial.SetInt("_Day", day);
	}

	private void setLightAmbientColor ()
	{
		float dayRatio = DayNightController.getDayRatio();
		Color begin;
		Color middle;
		Color end;

		if (DayNightController.isDay()) {
			begin = LightDayBeginColor;
			middle = LightDayMiddleColor;
			end = LightDayEndColor;
		
			Color c;
			if (dayRatio < 0.5f) {
				c = new Color (MathHelper.Map (dayRatio, 0, 0.5f, begin.r, middle.r), 
				               MathHelper.Map (dayRatio, 0, 0.5f, begin.g, middle.g), 
				               MathHelper.Map (dayRatio, 0, 0.5f, begin.b, middle.b), 1);
			}
			else {
				c = new Color (MathHelper.Map (dayRatio, 0.5f, 1, middle.r, end.r), 
				               MathHelper.Map (dayRatio, 0.5f, 1, middle.g, end.g), 
				               MathHelper.Map (dayRatio, 0.5f, 1, middle.b, end.b), 1);
			}

			shaderMaterial.SetColor("_LightColor", c);

		}


		
	}

}
