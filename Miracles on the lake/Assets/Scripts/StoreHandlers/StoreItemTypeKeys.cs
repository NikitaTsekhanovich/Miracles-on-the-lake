using System.ComponentModel;

namespace StoreHandlers
{
    public enum StoreItemTypeKeys
    {
        [Description("ShieldIsBought")]
        ShieldSaveKey,
        [Description("OffEaglesIsBought")]
        OffEaglesSaveKey,
        [Description("SmallPlayerIsBought")]
        SmallPlayerSaveKey,
        [Description("GoldenHealthIsBought")]
        GoldenHealthSaveKey
    }
}

