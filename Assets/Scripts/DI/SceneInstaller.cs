using System;
using RPG.Character;
using RPG.UI;
using RPG.UI.Inventories;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity playerEntity;
        [SerializeField] private InventoryCanvasView inventoryCanvasView;
        [SerializeField] private SceneViewConfig sceneViewConfig;
        public override void InstallBindings()
        {
            Container.Bind<PlayerEntity>().FromInstance(playerEntity);
            Container.Bind<EquipmentSlotUI>().FromComponentInHierarchy().AsTransient();
            Container.Bind<ActionSlotUI>().FromComponentInHierarchy().AsTransient();

            Container.Bind<InventoryCanvasView>().FromInstance(inventoryCanvasView);
            Container.BindInterfacesAndSelfTo<InventoryCanvasController>().AsSingle();
            Container.Bind<SceneViewConfig>().FromInstance(sceneViewConfig);
            Container.BindInterfacesAndSelfTo<MouseInputController>().AsSingle();
        }
    }
}