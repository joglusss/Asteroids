using Cysharp.Threading.Tasks;

namespace Asteroids.Total.Installers
{
    public interface IReadyFlag
    {
        bool IsReady { get; }
    }
}

