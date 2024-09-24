using UnityEngine;

namespace StoreHandlers
{
    [CreateAssetMenu(fileName = "StoreItemData", menuName = "Store item/ Item")]
    public class StoreItemData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _price;
        [SerializeField] private string _description;
        [SerializeField] private StoreItemTypeKeys _isBoughtKey;

        public string Name => _name;
        public int Price => _price;
        public string Description => _description;
        public int IsBought => PlayerPrefs.GetInt(EnumExtensions.GetDescription(_isBoughtKey));
        public string IsBoughtKey => EnumExtensions.GetDescription(_isBoughtKey);
    }
}

