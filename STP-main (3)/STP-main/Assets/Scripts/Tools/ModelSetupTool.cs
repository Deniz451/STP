/*using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class ModelSetupTool : EditorWindow
{
    GameObject model;
    private Dictionary<GameObject, GameObject> legBones = new Dictionary<GameObject, GameObject>();
    string savePath = "Assets/Prefabs/";
    float legDistanceCap = 0;
    AnimationCurve legCurve;
    float legSpeedFactor = 0;
    string prefabName;
    bool saveAsPrefab = false;


    [MenuItem("Tools/Model Setup Tool")]
    public static void ShowWindow()
    {
        GetWindow<ModelSetupTool>("Model Setup Tool");
    }

    void OnGUI()
    {
        EditorGUILayout.HelpBox("The first bone of each leg has to be named in format L/R_1-n, depending on which side it is on and its position from the head to the end of the model. Gameobject that you are trying to set up has to have a child named \"Armature\", that contains all of the bones.", MessageType.Info);
        GUILayout.Space(20);

        GUILayout.Label("Model", EditorStyles.boldLabel);
        model = (GameObject)EditorGUILayout.ObjectField("Model", model, typeof(GameObject), true);

        GUILayout.Space(20);

        GUILayout.Label("Animation Settings", EditorStyles.boldLabel);
        legCurve =  EditorGUILayout.CurveField("Leg Curve", legCurve);
        legDistanceCap =  EditorGUILayout.FloatField("Leg Distance Cap", legDistanceCap);
        legSpeedFactor =  EditorGUILayout.FloatField("Leg Speed Factor", legSpeedFactor);

        GUILayout.Space(20);

        GUILayout.Label("Prefab Settings", EditorStyles.boldLabel);
        saveAsPrefab = EditorGUILayout.Toggle("Save As Prefab", saveAsPrefab);
        prefabName = EditorGUILayout.TextField("Prefab Name", prefabName);
        if (GUILayout.Button("Select Save Path"))
        {
            string path = EditorUtility.SaveFolderPanel("Select Save Folder", "Assets/Prefabs", "");
            if (!string.IsNullOrEmpty(path))
            {
                savePath = "Assets" + path.Substring(Application.dataPath.Length);
            }
        }

        GUILayout.Space(40);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Setup model", GUILayout.Width(150), GUILayout.Height(17)))
        {
            if (model != null)
            {
                SetupInsectModel(model);
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private string[] GetNavMeshAgentTypes()
    {
        int agentTypeCount = NavMesh.GetSettingsCount();
        string[] agentTypes = new string[agentTypeCount];

        for (int i = 0; i < agentTypeCount; i++)
        {
            NavMeshBuildSettings settings = NavMesh.GetSettingsByID(i);
            agentTypes[i] = settings.agentTypeID.ToString();
        }

        return agentTypes;
    }

    void SetupInsectModel(GameObject model)
    {

        AplyBoneRenderer();
        AplyRigSetup();

        GameObject armature = model.transform.Find("Armature").gameObject;
        FindRootBones(armature);

        CreateRaycasts();
        CreateLegControllers();

        AssignVariables();

        if (saveAsPrefab) SavePrefab(model);
    }

    private void FindRootBones(GameObject objectToIterate)
    {
        // Iterate through all armature children
        for (int i = 0; i < objectToIterate.transform.childCount; i++)
        {
            GameObject child = objectToIterate.transform.GetChild(i).gameObject;
            char[] charArr = child.name.ToCharArray();

            // If the first char of the child is 'L' or 'R', it is a root bone
            if (charArr[0] == 'L' || charArr[0] == 'R')
            {
                // Add the root bone to the dictionary and try to find the end bone
                GameObject endBone = FindEndBones(child);

                // Add both root and end bone to the dictionary if the end bone is found
                if (endBone != null)
                {
                    legBones.Add(child, endBone);
                }
            }
            // If not root, check its children recursively
            else if (child.transform.childCount > 0)
            {
                FindRootBones(child);
            }
        }
    }

    private GameObject FindEndBones(GameObject objectToIterate)
    {
        // Check the first child of the object to see if it's an end bone
        GameObject child = objectToIterate.transform.GetChild(0).gameObject;

        // If the child contains "end" in its name, return it as the end bone
        if (child.name.Contains("end"))
        {
            return child;
        }
        // Otherwise, continue searching recursively if there are more children
        else if (child.transform.childCount > 0)
        {
            return FindEndBones(child);
        }

        // Return null if no end bone is found
        return null;
    }

    private void AplyRigSetup()
    {
        GameObject rigObject = new("Rig");
        Rig rigComponent = rigObject.AddComponent<Rig>();
        rigObject.transform.parent = model.transform;
        rigObject.transform.position = Vector3.zero;
        model.transform.AddComponent<RigBuilder>();
        RigBuilder rigBuilder = model.GetComponent<RigBuilder>();
        rigBuilder.layers.Add(new RigLayer(rigComponent));
        rigBuilder.Build();
    }

    private void AplyBoneRenderer()
    {
        model.transform.AddComponent<BoneRenderer>();
    }

    private void CreateRaycasts()
    {
        GameObject raycasts = new("Raycasts");
        raycasts.transform.parent = model.transform;
        raycasts.transform.position = Vector3.zero;

        foreach (GameObject key in legBones.Keys)
        {
            string raycastName = key.name.Replace("_root", string.Empty);
            GameObject raycast = new(raycastName);
            raycast.transform.parent = raycasts.transform;
            raycast.transform.position = new Vector3(legBones[key].transform.position.x, 3, legBones[key].transform.position.z);
        }
    }

    private void CreateLegControllers()
    {
        GameObject rig = model.transform.Find("Rig").gameObject;

        foreach (GameObject key in legBones.Keys)
        {
            string controllerName = key.name.Replace("_root", string.Empty);
            GameObject controller = new(controllerName);
            controller.transform.parent = rig.transform;
            controller.transform.position = model.transform.position;
            controller.AddComponent<ChainIKConstraint>();
            ChainIKConstraint chainIKConstraint = controller.GetComponent<ChainIKConstraint>();
            chainIKConstraint.data.root = key.transform;
            chainIKConstraint.data.tip = legBones[key].transform;

            GameObject target = new(controllerName + "_target");
            target.transform.parent = controller.transform;

            // calculating the z offset for the leg targets for smoother animation
            /*float zOffset = 0;
            char[] charArr = controllerName.ToCharArray();
            if (charArr[0] == 'L')
            {
                if ((int)charArr[1] % 2 == 0) zOffset = 0.5f;
                else zOffset = -0.5f;
            }
            else if (charArr[0] == 'R')
            {
                if ((int)charArr[1] % 2 == 0) zOffset = -0.5f;
                else zOffset = 0.5f;
            }

            target.transform.position = new(legBones[key].transform.position.x, legBones[key].transform.position.y, legBones[key].transform.position.z); //+ zOffset);
            chainIKConstraint.data.target = target.transform;

            controller.AddComponent<IKController>();
            controller.GetComponent<IKController>().target = target.transform;
            controller.GetComponent<IKController>().raycast = model.transform.Find("Raycasts").transform.Find(controllerName);
        }
    }

    private void AssignVariables()
    {
        GameObject rig = model.transform.Find("Rig").gameObject;
        for(int i = 0; i < rig.transform.childCount; i++)
        {
            GameObject child = rig.transform.GetChild(i).gameObject;
            IKController ikController = child.GetComponent<IKController>();
            ikController.distanceCap = legDistanceCap;
            if (legCurve == null) legCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            ikController.legMovementCurve = legCurve;
            ikController.speedFactor = legSpeedFactor;
        }
    }

    private void SavePrefab(GameObject model)
    {
        if (!AssetDatabase.IsValidFolder(savePath)) AssetDatabase.CreateFolder("Assets", "Prefabs");

        string prefabPath = $"{savePath}/{prefabName}.prefab";

        PrefabUtility.SaveAsPrefabAsset(model, prefabPath);
        Debug.Log($"Prefab saved at: {prefabPath}");
    }
}*/
