namespace JobControl.Bll
{
    public interface IGlobalSettingsRepository
    {
        GlobalSettingsEditor Fetch();

        ConcurrencyResult<GlobalSettingsEditor> Save(GlobalSettingsEditor data);
    }
}