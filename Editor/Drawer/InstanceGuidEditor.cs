using GameCreator.Editor.Common;
using GameCreator.Runtime.UniqueGameObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameCreator.Editor.UniqueGameObject
{
    [CustomEditor(typeof(InstanceGuid))]
    public class InstanceGuidEditor : UnityEditor.Editor
    {
        const string ErrDuplicateID = "One ore more Instance Guid components have the same ID";
        const string ErrEmptyGuid = "The Guid of this component should not be empty";
        readonly static Length ErrorMargin = new(10, LengthUnit.Pixel);

        readonly List<GameObject> _duplicateList = new();
        VisualElement _duplicateErrorContainer;
        ListView _duplicateListView;
        ErrorMessage _emptyGuidError;

        public override VisualElement CreateInspectorGUI()
        {
            if (Selection.count > 1)
            {
                return new Label("-");
            }
            _emptyGuidError = new ErrorMessage(ErrEmptyGuid)
            {
                style = { marginTop = ErrorMargin }
            };
            var errorMessage = new ErrorMessage(ErrDuplicateID)
            {
                style = { marginTop = ErrorMargin }
            };

            var guidProperty = serializedObject.FindProperty("_guid");
            var guidPropertyField = new PropertyField(guidProperty);
            guidPropertyField.SetEnabled(!Application.isPlaying);

            _duplicateListView = new ListView(_duplicateList);
            _duplicateListView.makeItem += () =>
            {
                var objectField = new ObjectField
                {
                    objectType = typeof(GameObject),
                    allowSceneObjects = true
                };
                objectField.SetEnabled(false);
                return objectField;
            };
            _duplicateListView.bindItem += (element, i) => ((ObjectField)element).value = _duplicateList[i];

            var root = new VisualElement();

            root.Add(guidPropertyField);
            root.Add(_emptyGuidError);

            _duplicateErrorContainer = new VisualElement();
            _duplicateErrorContainer.Add(errorMessage);
            _duplicateErrorContainer.Add(new SpaceSmaller());

            var duplicatesLabel = new Label("Duplicates");
            duplicatesLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
            _duplicateErrorContainer.Add(duplicatesLabel);

            _duplicateErrorContainer.Add(new SpaceSmaller());
            _duplicateErrorContainer.Add(_duplicateListView);
            root.Add(_duplicateErrorContainer);

            RefreshErrorID();
            guidPropertyField.RegisterValueChangeCallback(_ =>
            {
                RefreshErrorID();
            });

            return root;
        }

        void RefreshErrorID()
        {
            serializedObject.Update();
            _duplicateErrorContainer.style.display = DisplayStyle.None;
            _emptyGuidError.style.display = DisplayStyle.None;
            var guidProperty = serializedObject.FindProperty("_guid");

            if (PrefabUtility.IsPartOfPrefabAsset(target)) return;

            var thisGuidString = guidProperty
                .FindPropertyRelative(UniqueIDDrawer.SERIALIZED_ID)
                .FindPropertyRelative(IdStringDrawer.NAME_STRING)
                .stringValue;

            _emptyGuidError.style.display = thisGuidString == string.Empty ? DisplayStyle.Flex : DisplayStyle.None;

            var instanceGuids = FindObjectsByType<InstanceGuid>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _duplicateList.Clear();
            foreach (var instanceGuid in instanceGuids)
            {
                var guidString = instanceGuid.GuidIdString.String;
                if (thisGuidString != guidString || target == instanceGuid) continue;
                _duplicateList.Add(instanceGuid.gameObject);
            }
            _duplicateErrorContainer.style.display = _duplicateList.Count > 0 ? DisplayStyle.Flex : DisplayStyle.None;
            _duplicateListView.Rebuild();
        }
    }
}
