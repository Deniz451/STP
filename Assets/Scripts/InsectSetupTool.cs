using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class InsectSetupTool : EditorWindow
{
    GameObject model;
    List<GameObject> legBoneRoot;
    List<GameObject> legBoneEnd;

    string savePath = "Assets/Prefabs/";

    // Adjustable values for window
    string[] navMeshAgentTypes;
    int selectedNavMeshAgentIndex = 0;
    float navMeshAgentBaseOffset = 0;
    float legDistanceCap = 0;
    float legCurveHeight = 0;
    float legSpeedFactor = 0;
    string prefabName;

    [MenuItem("Tools/Model Setup Tool")]
    public static void ShowWindow()
    {
        GetWindow<InsectSetupTool>("Model Setup Tool");
    }

    void OnEnable()
    {
        navMeshAgentTypes = GetNavMeshAgentTypes();
    }

    void OnGUI()
    {
        GUILayout.Space(20);

        GUILayout.Label("Model", EditorStyles.boldLabel);
        model = (GameObject)EditorGUILayout.ObjectField("Model", model, typeof(GameObject), true);

        GUILayout.Space(20);

        GUILayout.Label("Navmesh Settings", EditorStyles.boldLabel);
        selectedNavMeshAgentIndex = EditorGUILayout.Popup("NavMeshAgent Index", selectedNavMeshAgentIndex, navMeshAgentTypes);
        navMeshAgentBaseOffset = EditorGUILayout.FloatField("NavMeshAgent Base Ofsset", navMeshAgentBaseOffset);

        GUILayout.Space(20);

        GUILayout.Label("Animation Settings", EditorStyles.boldLabel);
        legDistanceCap =  EditorGUILayout.FloatField("Leg Distance Cap", legDistanceCap);
        legCurveHeight =  EditorGUILayout.FloatField("Leg Curve Height", legCurveHeight);
        legSpeedFactor =  EditorGUILayout.FloatField("Leg Speed Factor", legSpeedFactor);

        GUILayout.Space(20);

        GUILayout.Label("Prefab Settings", EditorStyles.boldLabel);
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
        legBoneRoot = new List<GameObject>();
        legBoneEnd = new List<GameObject>();

        AplyBoneRenderer();
        AplyRigSetup();

        GameObject armature = model.transform.Find("Armature").gameObject;
        FindRootBones(armature);

        CreateRaycasts();
        CreateLegControllers();

        model.AddComponent<NavMeshAgent>();
        model.AddComponent<Movement>();

        AssignVariables();

        SavePrefab(model);
    }

    private void FindRootBones(GameObject objectToIterate)
    {

        // Itirate through all armature children
        for (int i = 0; i < objectToIterate.transform.childCount; i++)
        {
            // If the first char of the child is L or R, child must be root bone, therefor add to list
            GameObject child = objectToIterate.transform.GetChild(i).gameObject;
            char[] charArr = child.name.ToCharArray();
            if (charArr[0] == 'L' || charArr[0] == 'R')
            {
                legBoneRoot.Add(child);
                // If we found the root bone, find its end bone
                string targetName = child.name.Replace("root", "end");
                FindEndBones(child, targetName);
            }
            // Else if the child doesn have L or R as first char and has children, iterate through its children
            else if (child.transform.childCount > 0) FindRootBones(child);
        }
    }

    // Itirates throgu all the children of the target gameobject and compares them to target name
    private void FindEndBones(GameObject objectToIterate, string targetName)
    {
        GameObject child = objectToIterate.transform.GetChild(0).gameObject;
        if (child.name == targetName)
        {
            legBoneEnd.Add(child);
        }
        else if (child.transform.childCount > 0) FindEndBones(child, targetName);
    }

    private void AplyRigSetup()
    {
        GameObject rigObject = new("Rig");
        Rig rigComponent = rigObject.AddComponent<Rig>();
        rigObject.transform.parent = model.transform;
        rigObject.transform.position = Vector3.zero;
        model.transform.AddComponent<RigBuilder>();
        RigBuilder rigBuilder = model.GetComponent<RigBuilder>();
        // add rig to ribuilder
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

        // Creating individual raycast points depending on end bone position
        foreach (GameObject bone in legBoneEnd)
        {
            // Creating the name for the controller
            string raycastName = bone.name.Replace("_end", string.Empty);
            // Creating gameobject
            GameObject raycast = new(raycastName);
            raycast.transform.parent = raycasts.transform;
            raycast.transform.position = new Vector3(bone.transform.position.x, 3, bone.transform.position.z);
        }
    }

    private void CreateLegControllers()
    {
        // Finding the Rig gameobject
        GameObject rig = model.transform.Find("Rig").gameObject;
        // Itirating through each end bone and making a controller for it
        foreach (GameObject bone in legBoneEnd)
        {
            // Creating the name for the controller
            string controllerName = bone.name.Replace("_end", string.Empty);
            // Creating gameobject
            GameObject controller = new(controllerName);
            // Assigning parent to teh controller
            controller.transform.parent = rig.transform;
            // Assigning position to the controller equal to the position of the current end bone
            controller.transform.position = model.transform.position;
            // Adding the Chain IK Constraint to the gameobject
            controller.AddComponent<ChainIKConstraint>();
            ChainIKConstraint chainIKConstraint = controller.GetComponent<ChainIKConstraint>();
            // Assigning the root bone to the Chain IK Constraint
            foreach(GameObject rootBone in legBoneRoot) if (rootBone.name.Replace("root", "end") == bone.name) chainIKConstraint.data.root = rootBone.transform;
            // Assigning the tip bone to the Chain IK Constraint
            chainIKConstraint.data.tip = bone.transform;
            // Assigning the target to the Chain IK Constraint
            GameObject target = new(controllerName + "_target");
            target.transform.parent = controller.transform;
            target.transform.position = bone.transform.position;
            chainIKConstraint.data.target = target.transform;
            // Adding IKController script
            controller.AddComponent<IKController>();
            controller.GetComponent<IKController>().target = target.transform;
            controller.GetComponent<IKController>().raycast = model.transform.Find("Raycasts").transform.Find(controllerName);
        }
    }

    private void AssignVariables()
    {
        model.GetComponent<Movement>().agent = model.GetComponent<NavMeshAgent>();
        model.GetComponent<NavMeshAgent>().baseOffset = navMeshAgentBaseOffset;
        model.GetComponent<NavMeshAgent>().agentTypeID = selectedNavMeshAgentIndex;

        GameObject rig = model.transform.Find("Rig").gameObject;
        for(int i = 0; i < rig.transform.childCount; i++)
        {
            GameObject child = rig.transform.GetChild(i).gameObject;
            IKController ikController = child.GetComponent<IKController>();
            ikController.distanceCap = legDistanceCap;
            ikController.curveHeight = legCurveHeight;
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
}
