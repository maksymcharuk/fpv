namespace FPVDrone
{
    public interface IDroneRotor
    {
        #region Methods
        // dps = degrees per second
        void UpdateRotor(float dps);
        #endregion
    }
}
