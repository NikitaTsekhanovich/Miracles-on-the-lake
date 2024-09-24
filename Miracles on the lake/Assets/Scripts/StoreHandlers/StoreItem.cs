using UnityEngine;
using UnityEngine.UI;

namespace StoreHandlers
{
    public class StoreItem : MonoBehaviour
    {
        [SerializeField] private StoreItemData _storeItemData;
        [SerializeField] private Image _frameChoose;
        [SerializeField] private GameObject _isBoughtImage;

        public StoreItemData StoreItemData => _storeItemData;
        public Image FrameChoose => _frameChoose;
        public GameObject IsBoughtImage => _isBoughtImage;
    }
}

