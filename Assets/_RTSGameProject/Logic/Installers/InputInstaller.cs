﻿using _RTSGameProject.Logic.Common.Selection;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _clickable;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private LayerMask _buildingMask;
        [SerializeField] private SelectionBox _selectionBox;

        public override void InstallBindings()
        {
            BindInputCatchClick();
            BindInput();
        }

        private void BindInputCatchClick()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_camera)
                .AsSingle();
        }

        private void BindInput()
        {
            Container
                .BindInterfacesAndSelfTo<InputController>()
                .AsSingle();
            Container
                .BindInterfacesAndSelfTo<InputCatchKeyClick>()
                .AsSingle();
            Container
                .Bind<SelectionBox>()
                .FromInstance(_selectionBox)
                .AsSingle();
            Container
                .Bind<LayerMask>()
                .WithId("Clickable")
                .FromInstance(_clickable);
            Container
                .Bind<LayerMask>()
                .WithId("Ground")
                .FromInstance(_ground);
            Container
                .Bind<LayerMask>()
                .WithId("Building")
                .FromInstance(_buildingMask);
        } 
    }
}