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

	private Color currentAmbientColor = new Color();
	private Color currentLightColor = new Color();
	private float shaderMadness = 0;
	private int targetMadness = 0;

	void Update() {

		setShaderAmbientColor();
		setLightAmbientColor();
		int day = (DayNightController.isDay()) ? 1 : 0;
		int gameOver = (SceneManager.isGameOver()) ? 1 : 0;
		shaderMaterial.SetInt("_Day", day);
		shaderMaterial.SetInt("_GameOver", gameOver);
		shaderMaterial.SetFloat("_DayRatio", DayNightController.getDayRatio());
		setInsanityRenderSystem();
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

		if (dayRatio < 0.5f) {
			currentAmbientColor.r = MathHelper.Map (dayRatio, 0, 0.5f, begin.r, middle.r);
			currentAmbientColor.g = MathHelper.Map (dayRatio, 0, 0.5f, begin.g, middle.g);
			currentAmbientColor.b = MathHelper.Map (dayRatio, 0, 0.5f, begin.b, middle.b);
			currentAmbientColor.a = 1;
			               
		}
		else {
			currentAmbientColor.r = MathHelper.Map (dayRatio, 0.5f, 1, middle.r, end.r);
			currentAmbientColor.g = MathHelper.Map (dayRatio, 0.5f, 1, middle.g, end.g);
			currentAmbientColor.b = MathHelper.Map (dayRatio, 0.5f, 1, middle.b, end.b);
			currentAmbientColor.a = 1;
			               
		}
		shaderMaterial.SetColor("_AmbientColor", currentAmbientColor);

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
		
			if (dayRatio < 0.5f) {
				currentLightColor.r = MathHelper.Map (dayRatio, 0, 0.5f, begin.r, middle.r);
				currentLightColor.g = MathHelper.Map (dayRatio, 0, 0.5f, begin.r, middle.r);
				currentLightColor.b = MathHelper.Map (dayRatio, 0, 0.5f, begin.r, middle.r);
				currentLightColor.a = 1;

				if(dayRatio < 0.3) {
					currentLightColor.r = MathHelper.Map (dayRatio, 0, 0.3f, currentAmbientColor.r, currentLightColor.r);
					currentLightColor.g = MathHelper.Map (dayRatio, 0, 0.3f, currentAmbientColor.g, currentLightColor.g);
					currentLightColor.b = MathHelper.Map (dayRatio, 0, 0.3f, currentAmbientColor.b, currentLightColor.b);
				}
			}
			else {
				currentLightColor.r = MathHelper.Map (dayRatio, 0.5f, 1, middle.r, end.r);
				currentLightColor.g = MathHelper.Map (dayRatio, 0.5f, 1, middle.g, end.g);
				currentLightColor.b = MathHelper.Map (dayRatio, 0.5f, 1, middle.b, end.b);
				currentLightColor.a = 1;

				if(dayRatio > 0.7) {
					currentLightColor.r = MathHelper.Map (dayRatio, 0.7f, 1.0f, currentLightColor.r, currentAmbientColor.r);
					currentLightColor.g = MathHelper.Map (dayRatio, 0.7f, 1.0f, currentLightColor.g, currentAmbientColor.g);
					currentLightColor.b = MathHelper.Map (dayRatio, 0.7f, 1.0f, currentLightColor.b, currentAmbientColor.b);
				}
			}

			shaderMaterial.SetColor("_LightColor", currentLightColor);
		}

	}

	void setInsanityRenderSystem ()
	{
		targetMadness = Player.currentMadness;
		if (targetMadness > 2) {
			shaderMaterial.SetInt ("_Insane", 1);
			incrementMadnessValueOverTime ();
			float normalizedMadnessRatio = MathHelper.Map (shaderMadness, 2, Player.maxMadness, 0, 1f);
			shaderMaterial.SetFloat ("_MadnessRatio", normalizedMadnessRatio);
			setInsanityVector1 (normalizedMadnessRatio);
			if (shaderMadness > 4) {
				setInsanitVector2 ();
			}
		}
		else {
			shaderMaterial.SetInt ("_Insane", 0);
			shaderMadness = targetMadness;
		}
	}
	
	void incrementMadnessValueOverTime ()
	{
		if (shaderMadness < targetMadness) {
			shaderMadness += Time.deltaTime;
		}
		else
			if (shaderMadness > targetMadness) {
				shaderMadness -= Time.deltaTime;
			}
	}

	void setInsanityVector1 (float normalizedMadnessRatio)
	{
		float insanity1VectorRange = MathHelper.Map (normalizedMadnessRatio, 0, 1, 0.015f, 0.025f);
		float insanity1x = MathHelper.Map (Mathf.PerlinNoise (Time.time * shaderMadness, 0.0F), 0, 1, -insanity1VectorRange, insanity1VectorRange);
		float insanity1y = MathHelper.Map (Mathf.PerlinNoise (Time.time * shaderMadness, 10.0F), 0, 1, -insanity1VectorRange, insanity1VectorRange);
		shaderMaterial.SetVector ("_InsanityVector", new Vector4 (insanity1x, insanity1y, 0, 0));
	}

	void setInsanitVector2 ()
	{
		float insanity2VectorRange = MathHelper.Map ((shaderMadness - 2), 4, Player.maxMadness, 0.01f, 0.02f);
		float insanity2x = MathHelper.Map (Mathf.PerlinNoise (Time.time * (shaderMadness - 2), 20.0F), 0, 1, -insanity2VectorRange, insanity2VectorRange);
		float insanity2y = MathHelper.Map (Mathf.PerlinNoise (Time.time * (shaderMadness - 2), 30.0F), 0, 1, -insanity2VectorRange, insanity2VectorRange);
		shaderMaterial.SetVector ("_InsanityVector2", new Vector4 (insanity2x, insanity2y, 0, 0));
	}
}
