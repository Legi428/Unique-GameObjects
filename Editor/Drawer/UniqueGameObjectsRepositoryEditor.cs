using GameCreator.Editor.Common;
using GameCreator.Runtime.UniqueGameObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameCreator.Editor.UniqueGameObject
{
    [CustomPropertyDrawer(typeof(UniqueGameObjectsRepository))]
    public class UniqueGameObjectsRepositoryEditor : TTitleDrawer
    {
        ToolbarSearchField _searchField;
        ListView _listView;
        List<InstanceGuid> _allUniqueGameObjects;
        List<InstanceGuid> _filteredUniqueGameObjects;

        protected override string Title => "Unique Game Objects";

        protected override void CreateContent(VisualElement body, SerializedProperty property)
        {
            var refreshButton = new Button(Refresh)
            {
                text = "Refresh",
                style = { width = 200 }
            };

            _allUniqueGameObjects = new List<InstanceGuid>();
            _filteredUniqueGameObjects = new List<InstanceGuid>();

            _searchField = new ToolbarSearchField();
            _searchField.RegisterValueChangedCallback(_ => Refresh());

            _listView = new ListView(_filteredUniqueGameObjects);
            _listView.style.maxHeight = 500;
            _listView.makeItem += () =>
            {
                var objectField = new ObjectField
                {
                    objectType = typeof(InstanceGuid),
                    allowSceneObjects = true,
                    labelElement = { style = { minWidth = 200 } }
                };
                objectField.SetEnabled(false);
                return objectField;
            };
            _listView.bindItem += (element, i) =>
            {
                var objectField = (ObjectField)element;
                objectField.label = _filteredUniqueGameObjects[i].GuidIdString.String;
                objectField.value = _filteredUniqueGameObjects[i];
            };
            body.Add(refreshButton);
            body.Add(new SpaceSmall());
            body.Add(_searchField);
            body.Add(new SpaceSmall());
            body.Add(_listView);

            Refresh();
        }

        void UpdateFilter(string searchFieldValue)
        {
            _filteredUniqueGameObjects.Clear();
            _filteredUniqueGameObjects
                .AddRange(_allUniqueGameObjects.Where(x => x.name.Contains(searchFieldValue)
                                                           || x.GuidIdString.String.Contains(searchFieldValue)));
            _listView.Rebuild();
        }

        void Refresh()
        {
            _allUniqueGameObjects.Clear();
            var sceneObjects = Object.FindObjectsByType<InstanceGuid>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _allUniqueGameObjects.AddRange(sceneObjects);
            UpdateFilter(_searchField.value);
        }
    }
}
