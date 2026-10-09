public class ComboTracker
{
    private readonly ComboData data;
    private int progress;

    public ComboTracker(ComboData data)
    {
        this.data = data;
    }

    public ComboData Data => data;
    public int Progress => progress;

    public bool Feed(UnityEngine.KeyCode key)
    {
        if (key == data.Sequence[progress])
        {
            progress++;
        }
        else
        {
            progress = key == data.Sequence[0] ? 1 : 0;
        }

        if (progress < data.Length) return false;

        progress = 0;
        return true;
    }

    public void Reset()
    {
        progress = 0;
    }
}