using Asteroids.SceneManage;
using Zenject;

public class SceneServiceInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneService>().AsSingle();
    }
}
