using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ExampleProject;

[CustomEditor(typeof(PlayerSystem))]
public class PlayerEditor :Editor
{
    PlayerSystem player;

    bool foldMovemnetData;
    bool foldMailBoxData;
    private void OnEnable()
    {
        player = (PlayerSystem)target;
    }

    public override void OnInspectorGUI()
    {
        foldMovemnetData = EditorGUILayout.Foldout(foldMovemnetData, "移动数据");
        if (foldMovemnetData)
        {
            EditorGUILayout.Vector2Field("玩家速度", Data.playerMovement.velocity.Value);
            Data.playerMovement.speed = EditorGUILayout.FloatField("速率", Data.playerMovement.speed);
            Data.playerMovement.groundDetectSensitivity =
                EditorGUILayout.Slider("落地检测灵敏度", Data.playerMovement.groundDetectSensitivity, 0, 0.75f);

            Data.playerMovement.jumpForce =
                EditorGUILayout.IntField("跳跃力", Data.playerMovement.jumpForce);
        }

        foldMailBoxData = EditorGUILayout.Foldout(foldMailBoxData, "收到Event");
        if (foldMailBoxData)
        {
            EditorGUILayout.EnumFlagsField("The 1st indicator", Data.playerInput.eventReceiver[0]);
            EditorGUILayout.EnumFlagsField("The 2st indicator", Data.playerInput.eventReceiver[1]);
            EditorGUILayout.EnumFlagsField("The 3st indicator", Data.playerInput.eventReceiver[2]);

            EditorGUILayout.EnumFlagsField("Behavior", Data.playerInput.behavior.Value);

            EditorGUILayout.TagField(Data.playerInput.grounded.Value.ToString(), "isGround");
            EditorGUILayout.TextField("isGround",Data.playerInput.grounded.Value.ToString());
        }
    }
}
