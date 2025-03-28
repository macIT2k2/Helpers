using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Watermelon;
using System.Linq;
using System.Text;
using System.Globalization;
using Watermelon.SquadShooter;
using Sirenix.Utilities;
namespace Dev_Tools
{
    /// <summary>
    /// A static class for general helpful methods
    /// </summary> 

    public class Helper : MonoBehaviour
    {
        public static int Fibonacci(int n)
        {
            if (n <= 0)
                return 1;
            if (n == 1)
                return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        public static int RandomByWeight(float[] probs)
        {

            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }

        //random by weight paramter is a list of float
        public static int RandomByWeight(List<float> probs)
        {
            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Count; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Count - 1;
        }


        public static void DestroyAllChilds(Transform go)
        {
            for (int i = go.childCount - 1; i >= 0; i--)
            {
                Destroy(go.GetChild(i).gameObject);
            }
        }

        public static void DestroyImmediateAllChilds(Transform go)
        {
            for (int i = go.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(go.GetChild(i).gameObject);
            }
        }

        //public static void RecycleAllChilds(GameObject go)
        //{
        //    for (int i = go.transform.childCount - 1; i >= 0; i--)
        //    {
        //        ObjectPool.Recycle(go.transform.GetChild(i).gameObject);
        //    }
        //}

        //method switch color to hex
        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        //method switch hex to color
        public static Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }

        //example: ColorUtility.TryParseHtmlString("#FF00FF", out Color myColor);

        public static void SwapPositionInParent(Transform obj1, Transform obj2)
        {
            if (obj1.parent != obj2.parent)
            {
                Debug.LogError("Both objects must have the same parent.");
                return;
            }

            // L?y v? tr� hi?n t?i c?a 2 ??i t??ng
            int index1 = obj1.GetSiblingIndex();
            int index2 = obj2.GetSiblingIndex();

            // ??i v? tr�
            obj1.SetSiblingIndex(index2);
            obj2.SetSiblingIndex(index1);
        }



        public static List<T> GetAllChildsComponent<T>(Transform _parent)
        {
            List<T> _l = new();
            foreach (Transform _child in _parent.GetComponentsInChildren<Transform>(true))
            {
                if (_child.GetComponent<T>() != null)
                    _l.Add(_child.GetComponent<T>());
            }
            return _l;
        }

        public static T GetFirstChildComponent<T>(Transform _parent, string componentName)
        {
            foreach (Transform _child in _parent.GetComponentsInChildren<Transform>(true))
            {
                if (_child.name == componentName)
                {
                    return _child.GetComponent<T>();
                }
            }
            return default(T);
        }

        public static T GetComponentInSelf<T>(GameObject obj) where T : Component
        {
            return obj.GetComponent<T>();
        }

        public static void SaveStringToFile(string _filePath, string _textToSave)
        {
            File.WriteAllText("Assets/Resources/" + _filePath + ".txt", _textToSave);
        }

        public static string LoadFileToString(string _filePath)
        {
            TextAsset _textAss = (TextAsset)Resources.Load(_filePath);
            return _textAss.text;
        }

        public static Vector3 WorldToLocalPointInRectangle(Vector3 _worldPosition, Canvas _canvasParent)
        {
            // Convert the world position to screen space
            Vector2 _screenPosition = Camera.main.WorldToScreenPoint(_worldPosition);

            // Convert the screen position to canvas local position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasParent.transform as RectTransform, _screenPosition, _canvasParent.worldCamera, out Vector2 _localPosition);
            return _localPosition;
        }

        public static string FormatTime(float _time)
        {
            int _minutes = Mathf.FloorToInt(_time / 60f);
            int _seconds = Mathf.FloorToInt(_time % 60f);
            return string.Format("{0:00}:{1:00}", _minutes, _seconds);
        }

        public static bool IsLongScreen()
        {
            return (float)Screen.currentResolution.width / (float)Screen.currentResolution.height < 9f / 16f;
        }

        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> ShuffleList<T>(List<T> list)
        {
            List<T> _list = new();
            foreach (T _item in list)
            {
                _list.Add(_item);
            }
            int n = _list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = _list[k];
                _list[k] = _list[n];
                _list[n] = value;
            }
            return _list;
        }

        public static List<T> ShuffleList<T>(List<T> list, int n)
        {
            if (n <= list.Count)
            {
                return ShuffleList(list);
            }
            List<T> _list = ShuffleList(list);
            List<T> shuffledList = new List<T>(_list); // add all elements from the original list
            for (int i = _list.Count; i < n; i++)
            {
                T lastElement = shuffledList[shuffledList.Count - 1];
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, _list.Count);
                } while (_list[randomIndex].Equals(lastElement));
                shuffledList.Add(_list[randomIndex]);
            }
            return shuffledList;
        }

        public static T GetRandomEnum<T>(T _min, T _max)
        {
            System.Array A = System.Enum.GetValues(typeof(T));
            T V = (T)A.GetValue(UnityEngine.Random.Range((int)(object)_min, (int)(object)_max));
            return V;
        }

        public static List<T> GetAllEnum<T>()
        {

            List<T> enumValues = new();
            foreach (T value in System.Enum.GetValues(typeof(T)))
            {
                enumValues.Add(value);
            }
            return enumValues;
        }

        public static Vector2 PerpendicularVector(Vector2 direction, Vector2 point)
        {
            // Calculate the perpendicular vector
            Vector2 perpVector = new Vector2(-direction.y, direction.x);

            // Calculate the vector that passes through the point
            Vector2 resultVector = point + perpVector;

            return resultVector;
        }

        public static bool GetMousePostionOnPlane(Ray _ray, Plane _plane, out Vector3 _hitPoint)
        {
            if (_plane.Raycast(_ray, out float enter))
            {
                _hitPoint = _ray.GetPoint(enter);
                return true;
            }
            else
            {
                _hitPoint = Vector3.zero;
                return false;
            }
        }

        public static bool GetMousePostionOnCollider(Ray _ray, LayerMask _mask, out Vector3 _hitPoint)
        {
            if (Physics.Raycast(_ray, out RaycastHit _raycastHitPoint, Mathf.Infinity, _mask))
            {
                _hitPoint = _raycastHitPoint.point;
                return true;
            }
            else
            {
                _hitPoint = Vector3.zero;
                return false;
            }
        }

        public static bool GetMousePostionOnCollider(Ray _ray, LayerMask _mask, out RaycastHit _raycastHit)
        {
            if (Physics.Raycast(_ray, out RaycastHit _raycastHitPoint, Mathf.Infinity, _mask))
            {
                _raycastHit = _raycastHitPoint;
                return true;
            }
            else
            {
                _raycastHit = new RaycastHit();
                return false;
            }
        }

        public static bool PhysicRaycast2D(Camera _cam, out RaycastHit2D _hit)
        {
            // Cast a ray from the mouse position
            _hit = Physics2D.Raycast(_cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (_hit.collider != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static float ParseFloatWithPoint(string _value)
        {
            if (_value.Contains(","))
                _value = _value.Replace(",", ".");

            if (float.TryParse(_value, out float _result))
            {
                return _result;
            }
            else
            {
                Debug.LogError("Fail to parse: " + _value);
                return 0;
            }
        }

        //ham truyen vao 1 list , 1 phan tu trong list , tra ve 1 phan tu khac trong list
        public static T GetRandomElement<T>(List<T> list, T _exclude)
        {
            var random = new System.Random();
            var index = random.Next(list.Count);
            var element = list[index];
            while (element.Equals(_exclude))
            {
                index = random.Next(list.Count);
                element = list[index];
            }
            return element;
        }

        //ham truyen vao list , 1 phan tu trong list , tra ve phan tu tiep theo , neu phan tu cuoi cung thi tra ve phan tu dau tien
        public static T GetNextElement<T>(List<T> list, int currentIndex)
        {
            if (currentIndex == list.Count - 1)
            {
                return list[0];
            }
            else
            {
                return list[currentIndex + 1];
            }
        }

        public static List<T> GetDistinctElements<T>(List<T> list, int n, T _exclude = default)
        {
            var distinctElements = new HashSet<T>();
            var random = new System.Random();

            // Filter out the excluded element if provided and get distinct elements
            var filteredList = new List<T>(list);
            if (!EqualityComparer<T>.Default.Equals(_exclude, default))
            {
                filteredList.RemoveAll(item => item.Equals(_exclude));
            }

            while (distinctElements.Count < n && filteredList.Count > 0)
            {
                var index = random.Next(filteredList.Count);
                var element = filteredList[index];
                distinctElements.Add(element);
            }

            return new List<T>(distinctElements);
        }

        public static List<T> GetNewListWithResult<T>(List<T> list, T result, int loops)
        {
            if (loops < 1)
            {
                Debug.LogError("Loops must be greater than 0");
                return null;
            }
            List<T> _list = new();
            for (int i = 0; i < loops; i++)
            {
                if (i < loops - 1)
                {
                    _list.AddRange(list);
                }
                else if (i == loops - 1)
                {
                    foreach (var item in list)
                    {
                        _list.Add(item);
                        if (item.Equals(result)) break;
                    }
                }

            }
            return _list;
        }

        public static void LogCaller(string _message = " ",
            [System.Runtime.CompilerServices.CallerLineNumber] int line = 0
            , [System.Runtime.CompilerServices.CallerMemberName] string membername = ""
            , [System.Runtime.CompilerServices.CallerFilePath] string filePath = ""
            )
        {
            Debug.Log($"<color=green>Log with _message {_message}: </color> {line} :: {membername} :: {filePath}");
        }

        public static int GetGoodNumber(int currentNumber, int min, int max)
        {
            // (max - min )/4 and  return 3,5,7,9
            int _range = (max - min) / 4;
            if (currentNumber >= min && currentNumber <= min + _range)
            {
                return 5;
            }
            else if (currentNumber >= min + _range && currentNumber <= min + _range * 2)
            {
                return 7;
            }
            else if (currentNumber >= min + _range * 2 && currentNumber <= min + _range * 3)
            {
                return 9;
            }
            else
            {
                return 11;
            }

        }

        //find generic object
        public static T FindObject<T>(bool includeInactive = false) where T : UnityEngine.Object
        {
            T _objects = GameObject.FindObjectOfType<T>(includeInactive);
            return _objects;
        }

        public static void SetIconBySpriteSize(Image _image, Sprite _sprite, float _ratio = 1)
        {
            _image.sprite = _sprite;
            //Debug.Log($"<color=#2884FB>{_image}</color>");
            //Debug.Log($"<color=#2884FB>{_sprite}</color>");
            _image.GetComponent<RectTransform>().sizeDelta = new Vector2(_sprite.rect.width * _ratio, _sprite.rect.height * _ratio);
        }

        public static void PreSizeRawImage(RawImage _image, Texture _texture, float _ratio = 1)
        {
            _image.texture = _texture;
            _image.GetComponent<RectTransform>().sizeDelta = new Vector2(_texture.width * _ratio, _texture.height * _ratio);
        }

        public static void ResetSprite(Image _image, float _ratio = 1)
        {
            Sprite _sprite = _image.sprite;
            _image.GetComponent<RectTransform>().sizeDelta = new Vector2(_sprite.rect.width * _ratio, _sprite.rect.height * _ratio);
        }


        #region [Number]
        public static int UpperBound(float value)
        {
            return Mathf.CeilToInt(value);
        }

        public static int LowerBound(float value)
        {
            return Mathf.FloorToInt(value);
        }
        #endregion

        #region [Scroll]
        public static float GetTargetScrollPosition(ScrollRect scrollRect, RectTransform contentRect, RectTransform targetRect)
        {
            // Tính toán vị trí scroll dựa trên vị trí của container trong content
            float contentHeight = contentRect.rect.height;
            float viewportHeight = scrollRect.viewport.rect.height;
            float totalAnchorredPosition = 0;
            RectTransform currentRect = targetRect;
            int i = 0;
            while (currentRect != contentRect)
            {
                i++;
                totalAnchorredPosition += currentRect.anchoredPosition.y + currentRect.rect.height * (1 - currentRect.pivot.y);
                currentRect = currentRect.parent as RectTransform;
                if (i > 10)
                {
                    Debug.LogError("Loop too much");
                    break;
                }
            }

            // Normalize vị trí (0 = bottom, 1 = top)
            float normalizedPosition = (totalAnchorredPosition + contentHeight - viewportHeight) / (contentHeight - viewportHeight);
            return Mathf.Clamp01(normalizedPosition);
        }
        #endregion

        #region [Slice Tween]
        // public static void RunFillAnimation(SlicedFilledImage slice, float newFillAmount)
        // {
        //     if(slice.fillAmount == newFillAmount)
        //     {
        //         return;
        //     }
        //     float distance = Mathf.Abs(slice.fillAmount - newFillAmount) * 10;
        //     float _time = 0.15f + 0.08f * distance;
        //     Tween.DelayedCall(0.15f, () =>
        //     {
        //         slice.DOFillAmount(newFillAmount, _time).SetEasing(Watermelon.Ease.Type.CubicOut).OnComplete(() =>
        //         {
        //         });
        //     });
        // }
        #endregion

        #region [ReSize Sprite]
        public static void ResizeSprite_ByNative(Image targetImg, Sprite sprite, AspectRatioFitter.AspectMode aspectMode, float targetValue = 0)
        {
            targetImg.sprite = sprite;
            targetImg.SetNativeSize();
            AspectRatioFitter aspectRatioFitter = targetImg.GetComponent<AspectRatioFitter>();
            if (aspectRatioFitter == null)
            {
                aspectRatioFitter = targetImg.gameObject.AddComponent<AspectRatioFitter>();
            }
            aspectRatioFitter.aspectMode = aspectMode;
            float aspectRatio = (float)targetImg.sprite.texture.width / targetImg.sprite.texture.height;
            aspectRatioFitter.aspectRatio = aspectRatio;
            if (aspectMode == AspectRatioFitter.AspectMode.FitInParent) return;
            RectTransform iconRectTransform = targetImg.rectTransform;
            iconRectTransform.SetSizeWithCurrentAnchors(aspectMode == AspectRatioFitter.AspectMode.HeightControlsWidth ?
                                                            RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal, targetValue);
        }
        public static void ResizeSprite_ByCustomRatio(Image targetImg, float ratio, AspectRatioFitter.AspectMode aspectMode, float targetValue = 0)
        {
            //targetImg.SetNativeSize();
            AspectRatioFitter aspectRatioFitter = targetImg.GetComponent<AspectRatioFitter>();
            if (aspectRatioFitter == null)
            {
                aspectRatioFitter = targetImg.gameObject.AddComponent<AspectRatioFitter>();
            }
            aspectRatioFitter.aspectMode = aspectMode;
            aspectRatioFitter.aspectRatio = ratio;
            if (aspectMode == AspectRatioFitter.AspectMode.FitInParent) return;
            RectTransform iconRectTransform = targetImg.rectTransform;
            iconRectTransform.SetSizeWithCurrentAnchors(aspectMode == AspectRatioFitter.AspectMode.HeightControlsWidth ?
                                                            RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal, targetValue);
        }

        #endregion

        #region [TextField]
        public static string RemoveSpecialCharactersAndAccents(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            input = input.Replace("Đ", "D").Replace("đ", "d");
            // Bước 1: Chuẩn hóa thành FormD để tách dấu
            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);

                // Bỏ qua dấu (NonSpacingMark) nhưng giữ lại chữ cái
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    // Giữ lại các chữ cái (kể cả chữ nước ngoài) và số
                    if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                    {
                        sb.Append(c);
                    }
                }
            }

            // Bước 2: Chuẩn hóa lại về FormC để tránh lỗi Unicode
            string targetText = sb.ToString().Normalize(NormalizationForm.FormC);

            // Bước 3: Giới hạn độ dài
            return targetText.Length > maxLength ? targetText.Substring(0, maxLength) : targetText;
        }
        #endregion
    }
}
public static class CanvasExtensions
{
    public static Matrix4x4 GetCanvasMatrix(this Canvas _Canvas)
    {
        RectTransform rectTr = _Canvas.transform as RectTransform;
        Matrix4x4 canvasMatrix = rectTr.localToWorldMatrix;
        canvasMatrix *= Matrix4x4.Translate(-rectTr.sizeDelta / 2);
        return canvasMatrix;
    }
}

[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField]
    public DictionaryItem[] items;

    [System.Serializable]
    public class DictionaryItem
    {
        public TKey key;
        public TValue value;
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
        foreach (var item in items)
        {
            dict.Add(item.key, item.value);
        }
        return dict;
    }

    public SerializableDictionary(Dictionary<TKey, TValue> dict)
    {
        items = new DictionaryItem[dict.Count];
        int i = 0;
        foreach (var pair in dict)
        {
            items[i] = new DictionaryItem();
            items[i].key = pair.Key;
            items[i].value = pair.Value;
            i++;
        }
    }

    public SerializableDictionary()
    {
        items = new DictionaryItem[0];
    }

    public void Add(TKey key, TValue value)
    {
        items.AddRange(new DictionaryItem[] { new DictionaryItem() { key = key, value = value } });
    }

    public bool Contains(TKey key)
    {
        return items.Any(x => EqualityComparer<TKey>.Default.Equals(x.key, key));
    }
}

// Extension method để dễ sử dụng
public static class DictionaryExtensions
{
    public static SerializableDictionary<TKey, TValue> ToSerializable<TKey, TValue>(this Dictionary<TKey, TValue> dict)
    {
        return new SerializableDictionary<TKey, TValue>(dict);
    }
}

public class AnimationTriggerName
{
    public const string UNLOCK_MODE_NORMAL = "UnlockNormal";
    public const string UNLOCK_MODE_HARD = "UnlockHard";

    public const string LOCKED = "Locked";
}
