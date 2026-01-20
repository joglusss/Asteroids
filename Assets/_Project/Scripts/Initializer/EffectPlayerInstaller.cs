using Asteroids.Effect;
using Zenject;

namespace Asteroids.Installers
{
    class EffectPlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EffectPlayer>().AsSingle();
        }
    }
}
