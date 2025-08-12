using Asteroids.Input;
using Zenject;

public class InputStorageInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<KeyboardInputStorage>().AsSingle();
	}
}
