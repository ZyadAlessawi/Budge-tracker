using Essential_Lib.Localization.CultureResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essential_Lib.Localization
{
    public static class AppResourcesHelper
    {
        public static CultureInfo CurrentCulture { get; set; } = CultureInfo.CurrentCulture;
        public static string Localize(this string? key, CultureInfo? culture = null)
        {

            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;
            if (culture != null)
                return (string?)AppResources.ResourceManager.GetObject(key, culture) ?? $" -{key}- ";
            return LocalizationResourceManager.Instance[key];
            //string res = key==null?"": AppResources.ResourceManager.GetString(key);
            //string? res = key == null ? "" : LocalizationResourceManager.Instance[key]?.ToString();
            //return res??"";
        }
        public static string Localize(this string key, params object?[] BindingValues)
        {
            if (BindingValues != null)
                return string.Format(Localize(key), BindingValues);
            return key.Localize();
        }
    }
    //public class LocalizationBindingValue : BindableObject
    //{
    //    public static readonly BindableProperty BindingValueProperty = BindableProperty.Create(nameof(BindingValue), typeof(object), typeof(LocalizationBindingValue), null, BindingMode.OneWay);
    //    public object BindingValue
    //    {
    //        get => (object)GetValue(BindingValueProperty);
    //        set => SetValue(BindingValueProperty, value);
    //    }

    //}

    [ContentProperty(nameof(Name))]
    [AcceptEmptyServiceProvider]
    public class TranslateExtension : IMarkupExtension<BindingBase>
    {
        public string? Name { get; set; }
        public string? SuffixUnLocalizedValue { get; set; }
        public string? PreffixnLocalizedValue { get; set; }
        public BindingBase ProvideValue(IServiceProvider? serviceProvider)
        {
            try
            {
                return new Binding
                {
                    Mode = BindingMode.OneWay,
                    Path = $"[{Name}]",
                    Source = LocalizationResourceManager.Instance,
                    StringFormat = PreffixnLocalizedValue + "{0}" + SuffixUnLocalizedValue
                };
            }
            catch (Exception)
            {
                return default;
            }

        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
    public partial class LocalizationResourceManager : INotifyPropertyChanged
    {
        public LocalizationResourceManager()
        {
            AppResources.Culture = CultureInfo.CurrentCulture;
        }
        public static LocalizationResourceManager Instance { get; } = new();

        public string this[string resourceKey]
            => (string?)AppResources.ResourceManager.GetObject(resourceKey, AppResources.Culture) ?? $" -{resourceKey}- ";

        //public object this[string resourceKey,object BindingValue]
        //    =>string.Format((string?)AppResources.ResourceManager.GetObject(resourceKey, AppResources.Culture) ?? resourceKey,BindingValue);

        public event PropertyChangedEventHandler? PropertyChanged;

        public void SetCulture(CultureInfo culture)
        {
            AppResources.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }

}
