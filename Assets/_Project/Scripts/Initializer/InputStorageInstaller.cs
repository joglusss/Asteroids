using System;
using Asteroids.Input;
using UnityEditor.Rendering;
using UnityEngine;
using Zenject;

public class InputStorageInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<KeyboardInputStorage>().AsSingle();
	}
}
