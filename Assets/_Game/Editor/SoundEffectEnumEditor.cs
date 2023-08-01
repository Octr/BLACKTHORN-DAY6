using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundEffectData))]
public class SoundEffectEnumEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Enum"))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum()
    {
        SoundEffectData soundEffectData = (SoundEffectData)target;
        string enumString = "public enum SoundEffect\n{\n";

        if (soundEffectData.audioClips != null && soundEffectData.audioClips.Length > 0)
        {
            foreach (var clip in soundEffectData.audioClips)
            {
                if (clip != null)
                {
                    enumString += "    " + clip.name + ",\n";
                }
            }
        }

        enumString += "}";

        string enumFolderPath = "Assets/_Game/Scripts/Enums";
        string enumFilePath = enumFolderPath + "/SoundEffectEnum.cs";
        System.IO.Directory.CreateDirectory(enumFolderPath);
        System.IO.File.WriteAllText(enumFilePath, enumString);

        AssetDatabase.Refresh();
        Debug.Log("Enum generated and saved to " + enumFilePath);
    }
}
