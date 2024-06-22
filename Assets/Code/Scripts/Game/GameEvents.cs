using UniRx;

namespace FPVDrone
{
    public class GameEvents : Singletone<GameEvents>
    {
        public BoolReactiveProperty droneDestroyed { get; private set; } =
            new BoolReactiveProperty(false);

        public IntReactiveProperty dronesLeft { get; private set; } = new IntReactiveProperty(3);
        public FloatReactiveProperty groundSpeed { get; private set; } =
            new FloatReactiveProperty(0);
        public FloatReactiveProperty altitude { get; private set; } = new FloatReactiveProperty(0);
        public QuaternionReactiveProperty altitudeIndicatorRotation { get; private set; } =
            new QuaternionReactiveProperty(new UnityEngine.Quaternion(0f, 0f, 0f, 1f));
    }
}
