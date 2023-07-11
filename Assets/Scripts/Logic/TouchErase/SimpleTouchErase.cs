namespace Logic.TouchErase
{
    public class SimpleTouchErase : TouchErase
    {
        protected override void CheckWin() =>
            ResetRenderTexture();
    }
}