using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour
{

    private string jsonData;
    public Dropdown TextureQualityDropdown;
    public Dropdown vSyncDropdown;
    public Dropdown AntiAliasingDropdown;
    public Button applyButton;

	public Toggle effectsToggle;

    public GameSetting gamesettings;

	public static bool effects = true;

    void OnEnable()
    {
        gamesettings = new GameSetting();
        TextureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        AntiAliasingDropdown.onValueChanged.AddListener(delegate { OnAntiAliasingChange(); });
        applyButton.onClick.AddListener(delegate { ApplyButtonClick(); });

        LoadSettings();
    }

	public void EffectsToggleClick()
	{
		effects = effectsToggle.isOn;
		Debug.Log (effects);
	}

    public void OnTextureQualityChange()
    {


        QualitySettings.masterTextureLimit = gamesettings.TextureQuality = TextureQualityDropdown.value;
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gamesettings.vSync = vSyncDropdown.value;
    }

    public void OnAntiAliasingChange()
    {
        QualitySettings.antiAliasing = gamesettings.antiAliasing = (int)Mathf.Pow(2f, AntiAliasingDropdown.value);
    }

    public void ApplyButtonClick()
    {
        SaveSettings();
        Debug.Log(jsonData);
    }

    public void SaveSettings()
    {
        jsonData = JsonUtility.ToJson(gamesettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }
    public void LoadSettings()
    {
        gamesettings = JsonUtility.FromJson<GameSetting>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        AntiAliasingDropdown.value = gamesettings.antiAliasing / 2;
        TextureQualityDropdown.value = gamesettings.TextureQuality;
        vSyncDropdown.value = gamesettings.vSync;
    }
}
